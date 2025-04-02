
namespace Mirea.freelance.backend.models;

public abstract class Profile
{
    // Будет использоваться как первичный ключ, а также внешний ключ к User (если связь 1:1)
    public int UserId { get; set; }

    // Общий рейтинг для любого типа профиля
    public decimal Rating { get; set; } = 0;

    // Навигационное свойство на пользователя
    public User User { get; set; } = null!;
}

// Профиль для студента
public class StudentProfile : Profile
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;

    // Сфера разработки или специальность
    public string SphereOfDevelopment { get; set; } = string.Empty;
}

// Профиль для преподавателя
public class TeacherProfile : Profile
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    public string Telegram { get; set; } = string.Empty;

    // Здесь может быть предмет, который ведёт преподаватель, или его специализация
    public string SphereOfDevelopment { get; set; } = string.Empty;

    // Пример: кабинет, адрес вуза и т.п.
    public string OfficeAddress { get; set; } = string.Empty;
}

// Профиль для компании
public class CompanyProfile : Profile
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    
    // Коллекция контактов компании
    public ICollection<CompanyContact> Contacts { get; set; } = new List<CompanyContact>();
    
    public string TaxId { get; set; } = string.Empty; //ИНН
    public string Website { get; set; } = string.Empty;
}