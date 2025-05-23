import { api } from './api';

// 1) Список задач по проекту
export function getTasksByProject(projectId) {
  return api.get(`/projects/${projectId}/tasks`);
}

// 2) Создать новую задачу
export function createTask({ orderId, assigneeStudentId, title, description }) {
  return api.post('/tasks', { orderId, assigneeStudentId, title, description });
}

// 3) Сменить статус задачи
//    newStatus: 1 = AwaitingReview, 2 = Done, 3 = Rejected
export function changeTaskStatus(taskId, newStatus) {
  return api.patch(`/tasks/${taskId}/status`, { newStatus });
}

// 4) Опционально: история статусов
export function getTaskHistory(taskId) {
  return api.get(`/tasks/${taskId}/history`);
}
