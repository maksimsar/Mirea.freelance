// Builder для фронтенда
export class DocumentTreeBuilder {
    constructor() {
      this.nodes = [];
    }
  
    addFolder(path) {
      this.nodes.push({ path, type: 'folder' });
      return this;
    }
  
    addFile(path, template = null) {
      this.nodes.push({ path, type: 'file', template });
      return this;
    }
  
    build() {
      return { nodes: this.nodes };
    }
  }