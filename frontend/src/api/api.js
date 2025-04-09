import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5083/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const loginUser = (data) => api.post('/auth/login', data);
export const createUser = (data) => api.post('/Users', data);
export const createStudentProfile = (data) => api.post('/Profiles/student', data);
export const createMentorProfile = (data) => api.post('/Profiles/mentor', data);
export const createCompanyProfile = (data) => api.post('/Profiles/company', data);

export default api;