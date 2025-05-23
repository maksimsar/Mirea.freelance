import { api } from './api';

// 1) Доступные (открытые) проекты
export function getAvailableProjects() {
  return api.get('/orders/open');
}

// 2) Мои проекты как ментор
export function getMyProjectsAsMentor(mentorId) {
  return api.get(`/orders/mentor/${mentorId}`);
}

// 3) Мои проекты как студент
export function getMyProjectsAsStudent(studentId) {
  return api.get(`/orders/freelancer/${studentId}`);
}

// 4) Прикрепить студента к проекту
export function attachStudentToProject(orderId, studentId) {
  return api.post(`/orders/${orderId}/students`, { studentProfileId: studentId });
}

// 5) Взять проект в кураторство (назначить ментора)
export function assignMentorToProject(orderId, mentorId) {
  return api.patch(`/orders/${orderId}/assign-mentor`, { mentorProfileId: mentorId });
}
