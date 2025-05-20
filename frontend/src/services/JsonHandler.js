/* eslint-disable no-unused-vars */
export default {
    validate(path, file) {
        // проверка JSON-консистентности
        try {
            JSON.parse(file);
            return Promise.resolve();
        } catch (e) {
            return Promise.reject(e);
        }
    }
}
