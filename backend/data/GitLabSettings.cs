namespace Mirea.freelance.backend.data
{
    public class GitLabSettings
    {
        public string  BaseUrl      { get; set; } = null!;
        public string  PrivateToken { get; set; } = null!;
        public long    ProjectId    { get; set; }
    }
}
