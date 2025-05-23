using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.dto;
using Mirea.freelance.backend.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mirea.freelance.backend.services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    // Получить заказ по Id
        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                Title = order.Title,
                Description = order.Description,
                Status = order.Status,
                Budget = order.Budget,
                CompanyProfileId = order.CompanyProfileId,
                CreatedDate = order.CreatedDate,
                Deadline = order.Deadline
            };
        }

        // Получить все заказы
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Status = o.Status,
                Budget = o.Budget,
                CompanyProfileId = o.CompanyProfileId,
                CreatedDate = o.CreatedDate,
                Deadline = o.Deadline
            });
        }

        // Создать заказ
        public async Task<(bool success, string message, OrderResponseDto? order)> CreateOrderAsync(CreateOrderDto dto)
        {
            // Примеры проверок
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return (false, "Название заказа не может быть пустым.", null);
            }
            if (dto.Budget <= 0)
            {
                return (false, "Бюджет должен быть больше 0.", null);
            }

            // Создаём новый объект Order из DTO
            var newOrder = new Order
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = "Open",
                Budget = dto.Budget,
                CompanyProfileId = dto.CompanyProfileId,
                CreatedDate = DateTime.UtcNow,
                Deadline = dto.Deadline
            };

            await _orderRepository.AddAsync(newOrder);

            var response = new OrderResponseDto
            {
                Id = newOrder.Id,
                Title = newOrder.Title,
                Description = newOrder.Description,
                Status = newOrder.Status,
                Budget = newOrder.Budget,
                CompanyProfileId = newOrder.CompanyProfileId,
                CreatedDate = newOrder.CreatedDate,
                Deadline = newOrder.Deadline
            };

            return (true, "Заказ успешно создан.", response);
        }

        // Обновить заказ
        public async Task<(bool success, string message, OrderResponseDto? order)> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return (false, "Заказ не найден.", null);
            }

            // Меняем те поля, которые разрешено обновлять
            existingOrder.Title = dto.NewTitle;
            existingOrder.Description = dto.NewDescription;
            existingOrder.Budget = dto.NewBudget;
            existingOrder.Deadline = dto.NewDeadline;

            await _orderRepository.UpdateAsync(existingOrder);

            // Формируем ответ
            var response = new OrderResponseDto
            {
                Id = existingOrder.Id,
                Title = existingOrder.Title,
                Description = existingOrder.Description,
                Status = existingOrder.Status,
                Budget = existingOrder.Budget,
                CompanyProfileId = existingOrder.CompanyProfileId,
                CreatedDate = existingOrder.CreatedDate,
                Deadline = existingOrder.Deadline
            };

            return (true, "Заказ успешно обновлен.", response);
        }

        // Удалить заказ
        public async Task<(bool success, string message)> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return (false, "Заказ не найден.");
            }

            await _orderRepository.DeleteAsync(id);
            return (true, "Заказ успешно удален.");
        }
        
        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByCompanyIdAsync(int companyProfileId)
        {
            var orders = await _orderRepository.GetOrdersByCompanyIdAsync(companyProfileId);
            return orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Status = o.Status,
                Budget = o.Budget,
                CompanyProfileId = o.CompanyProfileId,
                CreatedDate = o.CreatedDate,
                Deadline = o.Deadline
            });
        }

        // Получить заказы, где указан конкретный фрилансер
        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByFreelancerIdAsync(int freelancerProfileId)
        {
            var orders = await _orderRepository.GetOrdersByFreelancerIdAsync(freelancerProfileId);
            return orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Status = o.Status,
                Budget = o.Budget,
                CompanyProfileId = o.CompanyProfileId,
                CreatedDate = o.CreatedDate,
                Deadline = o.Deadline
            });
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOpenOrdersAsync()
        {
            // Вызываем метод репозитория, который возвращает все заказы со статусом "Open"
            var openOrders = await _orderRepository.GetOrdersAsync();

            // Преобразуем каждый Order в OrderResponseDto
            return openOrders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Status = o.Status,
                Budget = o.Budget,
                CompanyProfileId = o.CompanyProfileId,
                CreatedDate = o.CreatedDate,
                Deadline = o.Deadline
            });
        }

        // Assign mentor to existing order
        public async Task<(bool success, string message)> AssignMentorAsync(int orderId, int mentorProfileId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return (false, "Order not found.");
            if (order.MentorProfileId != null)
                return (false, "Order already has a mentor.");

            order.MentorProfileId = mentorProfileId;
            await _orderRepository.UpdateAsync(order);
            return (true, "Mentor assigned successfully.");
        }

        // Attach student to existing order
        public async Task<(bool success, string message)> AttachStudentAsync(int orderId, int studentProfileId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return (false, "Order not found.");

            if (order.ProjectStudents.Any(ps => ps.StudentId == studentProfileId))
                return (false, "Student already attached.");

            order.ProjectStudents.Add(new ProjectStudent {
                OrderId   = orderId,
                StudentId = studentProfileId
            });
            await _orderRepository.UpdateAsync(order);
            return (true, "Student attached successfully.");
        }

        // Get orders by mentor
        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByMentorIdAsync(int mentorProfileId)
        {
            var orders = await _orderRepository.GetOrdersByMentorIdAsync(mentorProfileId);
            return orders.Select(o => new OrderResponseDto {
                Id               = o.Id,
                Title            = o.Title,
                Description      = o.Description,
                Status           = o.Status,
                Budget           = o.Budget,
                CompanyProfileId = o.CompanyProfileId,
                CreatedDate      = o.CreatedDate,
                Deadline         = o.Deadline
            });
        }

}   