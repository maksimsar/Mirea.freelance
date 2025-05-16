using Mirea.freelance.backend.services.Models;

namespace Mirea.freelance.backend.services
{
    public class DocumentTreeBuilder
    {
        private readonly List<DocumentNode> _nodes = new List<DocumentNode>();

        public DocumentTreeBuilder AddFolder(string path)
        {
            _nodes.Add(new DocumentNode { Path = path, Type = NodeType.Folder });
            return this;
        }

        public DocumentTreeBuilder AddFile(string path, string? template = null)
        {
            _nodes.Add(new DocumentNode { Path = path, Type = NodeType.File, Template = template });
            return this;
        }

        public DocumentTree Build()
            => new DocumentTree { Nodes = _nodes };
    }
}
