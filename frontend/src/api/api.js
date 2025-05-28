import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5083/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

//  добавления токена
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// обработка 401
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('userRole');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export const loginUser = (data) => api.post('/auth/login', data);
export const createUser = (data) => api.post('/Users', data);
export const createStudentProfile = (data) => api.post('/Profiles/student', data);
export const createMentorProfile = (data) => api.post('/Profiles/mentor', data);
export const createCompanyProfile = (data) => api.post('/Profiles/company', data);

// Получить данные пользователя (для роли)
export const getUserProfile = () => api.get('/Users/profile');

export default api;