public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}