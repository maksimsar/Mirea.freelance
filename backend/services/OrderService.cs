using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;

namespace Mirea.freelance.backend.services
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderResponseDto>> GetOrdersByMentorIdAsync(int mentorProfileId);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByStatusAsync(OrderStatus status);
        Task<(bool success, string message, OrderResponseDto? order)> CreateOrderAsync(CreateOrderDto dto);
        Task<(bool success, string message, OrderResponseDto? order)> UpdateOrderAsync(int id, UpdateOrderDto dto);
        Task<(bool success, string message)> ChangeStatusAsync(int id, ChangeStatusDto dto);
        Task<(bool success, string message)> DeleteOrderAsync(int id);
        Task<(bool success, string message)> AssignMentorAsync(int orderId, int mentorProfileId);
        Task<(bool success, string message)> AttachStudentAsync(int orderId, int studentProfileId);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusStrategy _statusStrategy;

        public OrderService(IOrderRepository orderRepository, IOrderStatusStrategy statusStrategy)
        {
            _orderRepository = orderRepository;
            _statusStrategy = statusStrategy;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : MapToResponseDto(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByMentorIdAsync(int mentorProfileId)
        {
            var orders = await _orderRepository.GetOrdersByMentorWithDetailsAsync(mentorProfileId);
            return orders.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return orders.Select(MapToResponseDto);
        }

        public async Task<(bool success, string message, OrderResponseDto? order)> CreateOrderAsync(CreateOrderDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return (false, "Название заказа не может быть пустым.", null);
            if (dto.Budget <= 0)
                return (false, "Бюджет должен быть больше 0.", null);

            var factory = new OrderFactory();
            var order = factory.CreateOrder(OrderStatus.Open);
            var builder = new OrderBuilder(order)
                .SetTitle(dto.Title)
                .SetDescription(dto.Description)
                .SetBudget(dto.Budget)
                .SetCompanyProfileId(dto.CompanyProfileId)
                .SetDeadline(dto.Deadline)
                .SetRequiredRoles(dto.RequiredRoles)
                .SetPreferredContactMethods(dto.PreferredContactMethods);

            await _orderRepository.AddAsync(order);

            return (true, "Заказ успешно создан.", MapToResponseDto(order));
        }

        public async Task<(bool success, string message, OrderResponseDto? order)> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return (false, "Заказ не найден.", null);

            if (order.Status != OrderStatus.Unprocessed && order.Status != OrderStatus.Processing)
                return (false, "Редактирование доступно только для заказов в статусе Unprocessed или Processing.", null);

            if (string.IsNullOrWhiteSpace(dto.NewTitle))
                return (false, "Название заказа не может быть пустым.", null);
            if (dto.NewBudget < 0)
                return (false, "Бюджет не может быть отрицательным.", null);

            var builder = new OrderBuilder(order)
                .SetTitle(dto.NewTitle)
                .SetDescription(dto.NewDescription)
                .SetBudget(dto.NewBudget)
                .SetDeadline(dto.NewDeadline)
                .SetRequiredRoles(dto.NewRequiredRoles);

            await _orderRepository.UpdateAsync(order);

            return (true, "Заказ успешно обновлен.", MapToResponseDto(order));
        }

        public async Task<(bool success, string message)> ChangeStatusAsync(int id, ChangeStatusDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return (false, "Заказ не найден.");

            try
            {
                _statusStrategy.Process(order, dto.NewOrderStatus);
                await _orderRepository.UpdateAsync(order);
                return (true, "Статус заказа успешно изменён.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool success, string message)> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return (false, "Заказ не найден.");

            await _orderRepository.DeleteAsync(id);
            return (true, "Заказ успешно удален.");
        }

        public async Task<(bool success, string message)> AssignMentorAsync(int orderId, int mentorProfileId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return (false, "Заказ не найден.");
            if (order.MentorProfileId != null)
                return (false, "Заказ уже имеет наставника.");

            order.MentorProfileId = mentorProfileId;
            await _orderRepository.UpdateAsync(order);
            return (true, "Наставник успешно назначен.");
        }

        public async Task<(bool success, string message)> AttachStudentAsync(int orderId, int studentProfileId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return (false, "Заказ не найден.");

            if (order.ProjectStudents.Any(ps => ps.StudentId == studentProfileId))
                return (false, "Студент уже прикреплён.");

            order.ProjectStudents.Add(new ProjectStudent
            {
                OrderId = orderId,
                StudentId = studentProfileId
            });
            await _orderRepository.UpdateAsync(order);
            return (true, "Студент успешно прикреплён.");
        }

        private OrderResponseDto MapToResponseDto(Order order)
{
    return new OrderResponseDto
    {
        Id = order.Id,
        Title = order.Title,
        Description = order.Description,
        Status = order.Status,
        Budget = order.Budget,
        CompanyProfileId = order.CompanyProfileId,
        CompanyProfile = order.CompanyProfile == null ? null : new CompanyProfileResponseDto
        {
            UserId = order.CompanyProfile.UserId,
            CompanyName = order.CompanyProfile.CompanyName,
            CompanyAddress = order.CompanyProfile.CompanyAddress,
            TaxId = order.CompanyProfile.TaxId,
            Website = order.CompanyProfile.Website,
            Contacts = order.CompanyProfile.Contacts?.Select(c => new CompanyContactResponseDto
            {
                Id = c.Id,
                CompanyProfileId = c.CompanyProfileId,
                Name = c.Name,
                Phone = c.Phone,
                Telegram = c.Telegram,
                Email = c.Email
            }).ToList() ?? new List<CompanyContactResponseDto>()
        },
        CreatedDate = order.CreatedDate,
        Deadline = order.Deadline,
        RequiredRoles = order.RequiredRoles,
        PreferredContactMethods = order.PreferredContactMethods,
        MentorProfileId = order.MentorProfileId
    };
}

    }

    // Factory Method
    public class OrderFactory
    {
        public Order CreateOrder(OrderStatus status)
        {
            return new Order
            {
                Status = status,
                CreatedDate = DateTime.UtcNow
            };
        }
    }

    // Builder
    public class OrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder(Order order)
        {
            _order = order;
        }

        public OrderBuilder SetTitle(string title) { _order.Title = title; return this; }
        public OrderBuilder SetDescription(string description) { _order.Description = description; return this; }
        public OrderBuilder SetBudget(decimal budget) { _order.Budget = budget; return this; }
        public OrderBuilder SetCompanyProfileId(int companyProfileId) { _order.CompanyProfileId = companyProfileId; return this; }
        public OrderBuilder SetDeadline(DateTime? deadline)
{
        _order.Deadline = deadline?.ToUniversalTime();
    return this;
}
        public OrderBuilder SetRequiredRoles(string roles) { _order.RequiredRoles = roles; return this; }
        public OrderBuilder SetPreferredContactMethods(string methods) { _order.PreferredContactMethods = methods; return this; }
        public Order Build() => _order;
    }

    // Strategy
    public interface IOrderStatusStrategy
    {
        void Process(Order order, OrderStatus newOrderStatus);
    }

    public class DefaultOrderStatusStrategy : IOrderStatusStrategy
    {
        public void Process(Order order, OrderStatus newOrderStatus)
        {
            var validTransitions = new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.Open, new[] { OrderStatus.Unprocessed } },
                { OrderStatus.Unprocessed, new[] { OrderStatus.Processing } },
                { OrderStatus.Processing, new[] { OrderStatus.ProcessedPositive, OrderStatus.ProcessedNegative } }
            };

            if (!validTransitions.ContainsKey(order.Status) || !validTransitions[order.Status].Contains(newOrderStatus))
                throw new Exception($"Недопустимый переход статуса из {order.Status} в {newOrderStatus}");

            order.Status = newOrderStatus;
        }
    }
}