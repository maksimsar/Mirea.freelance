
namespace Mirea.freelance.backend.models;

public abstract class Profile
{
    // Будет использоваться как первичный ключ, а также внешний ключ к User (если у вас связь 1:1).
    public int UserId { get; set; }

    // Общий рейтинг для любого типа профиля
    public decimal Rating { get; set; } = 0;

    // Навигационное свойство на пользователя
    public User User { get; set; }
}

// Профиль для студента
public class StudentProfile : Profile
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    

    public int Age { get; set; }
    public string Gender { get; set; }
    

    public string Phone { get; set; }
    public string Telegram { get; set; }

    // Сфера разработки или специальность
    public string SphereOfDevelopment { get; set; }
}

// Профиль для преподавателя
public class TeacherProfile : Profile
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    

    public int Age { get; set; }
    public string Gender { get; set; }

    
    public string Phone { get; set; }
    public string Telegram { get; set; }

    // Здесь может быть предмет, который ведёт преподаватель, или его специализация
    public string SphereOfDevelopment { get; set; }

    // Пример: кабинет, адрес вуза и т.п.
    public string OfficeAddress { get; set; }
}

// Профиль для компании
public class CompanyProfile : Profile
{
    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    

    // Коллекция контактов компании
    public ICollection<CompanyContact> Contacts { get; set; } = new List<CompanyContact>();
    
    
    public string TaxId { get; set; }
    public string Website { get; set; }
}
