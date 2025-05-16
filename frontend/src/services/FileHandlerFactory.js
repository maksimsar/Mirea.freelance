// Файл: frontend/src/services/filehandlers/FileHandlerFactory.js
import MarkdownHandler from './MarkdownHandler'
import JsonHandler     from './JsonHandler'
import ImageHandler    from './ImageHandler'
import GenericHandler  from './GenericHandler'

export default {
  /**
   * @param {string} ext — расширение, включая точку, например ".txt"
   * @returns {{ validate: function }}
   */
  create(ext) {
    switch (ext.toLowerCase()) {
      case '.md':    return MarkdownHandler
      case '.json':  return JsonHandler
      case '.png':
      case '.jpg':
      case '.jpeg':  return ImageHandler

      // Добавляем все остальные текстовые/документные форматы сюда:
      case '.txt':
      case '.pdf':
      case '.doc':
      case '.docx':
      case '.xls':
      case '.xlsx':
      case '.csv':
        return GenericHandler

      // По умолчанию — тоже Generic
      default:
        return GenericHandler
    }
  }
}