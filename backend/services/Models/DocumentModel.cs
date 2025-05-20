namespace Mirea.freelance.backend.services.Models
{
    public enum NodeType
    {
        Folder,
        File
    }

    public class DocumentNode
    {
        public string   Path     { get; set; } = null!;
        public NodeType Type     { get; set; }
        public string?  Template { get; set; }
    }

    public class DocumentTree
    {
        public IList<DocumentNode> Nodes { get; set; } = new List<DocumentNode>();
    }
}
