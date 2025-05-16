using System;
using System.Collections.Generic;

namespace Mirea.freelance.backend.services.filehandlers
{
    public static class FileHandlerFactory
    {
        // Словарь «расширение → функция-создатель обработчика»
        private static readonly Dictionary<string, Func<IFileHandler>> _handlers
            = new Dictionary<string, Func<IFileHandler>>(StringComparer.OrdinalIgnoreCase)
        {
            [".md"]   = () => new MarkdownHandler(),
            [".json"] = () => new JsonHandler(),
            [".png"]  = () => new ImageHandler(),
            [".jpg"]  = () => new ImageHandler(),
            [".jpeg"] = () => new ImageHandler(),
            // можно здесь же по умолчанию добавить:
            [".txt"]  = () => new TextHandler(),
            [".pdf"]  = () => new BinaryHandler(),
            [".docx"] = () => new BinaryHandler(),
            [".xlsx"] = () => new BinaryHandler(),
        };

        /// <summary>
        /// Регистрирует новый обработчик для указанного расширения.
        /// </summary>
        public static void Register(string extension, Func<IFileHandler> creator)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));
            if (!extension.StartsWith(".")) extension = "." + extension;
            _handlers[extension] = creator;
        }

        /// <summary>
        /// Возвращает обработчик для данного расширения.
        /// Если расширение не зарегистрировано — бросает NotSupportedException.
        /// </summary>
        public static IFileHandler Create(string extension)
        {
            if (extension == null) throw new ArgumentNullException(nameof(extension));
            if (!extension.StartsWith(".")) extension = "." + extension;

            if (_handlers.TryGetValue(extension, out var creator))
                return creator();

            throw new NotSupportedException($"Unsupported file type: {extension}");
        }
    }
}
