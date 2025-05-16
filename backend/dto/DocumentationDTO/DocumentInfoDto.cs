using System;

namespace Mirea.freelance.backend.dto
{
    public class DocumentInfoDto
    {
        public string   Path         { get; set; }
        public string   FileName     { get; set; }
        public string Type { get; set; } = null!; 
        public int      VersionCount { get; set; }
        public DateTime LastModified { get; set; }
        public string   LastCommitId { get; set; }
    }
}
