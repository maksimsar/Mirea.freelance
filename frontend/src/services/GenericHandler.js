/* eslint-disable no-unused-vars */
export default {
    /**
     * Никакой валидации не требуется – любые файлы пропускаем
     * @param {string} path — путь к файлу
     * @param {File|Blob} file — содержимое
     */
    validate(path, file) {
      return Promise.resolve()
    }
  }
  