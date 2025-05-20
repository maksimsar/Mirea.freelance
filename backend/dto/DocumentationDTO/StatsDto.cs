// backend/dto/StatsDto.cs
namespace Mirea.freelance.backend.dto
{
    public class StatsDto
    {
        public int TotalFiles    { get; set; }
        public int TotalVersions { get; set; }
        public long TotalBytes   { get; set; }
        public int RecentCommits { get; set; }
    }
}
