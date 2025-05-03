import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import OrdersPage from '../views/OrdersPage.vue';
import ProfilePage from '../views/ProfilePage.vue';
import ProfileTeacher from '../views/ProfileTeacher.vue';
import ProfileCompany from '../views/ProfileCompany.vue';
import ProcessingOrder from '../views/ProcessingOrder.vue';
import CompanyOrder from '../views/CompanyOrder.vue';
import AuthPage from '../views/AuthPage.vue';
import OrderTrackingStudent from '../views/OrderTrackingStudent.vue';
import OrderTrackingTeacher from '../views/OrderTrackingTeacher.vue'; // Можно оставить, если в дальнейшем понадобится
import OrderTrackingCompany from '../views/OrderTrackingCompany.vue';
import AdminOrders from '../views/AdminsOrders.vue';    // Уже есть
import AdminDocs from '../views/AdminDocs.vue';        // Новый компонент для документации

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomePage,
    meta: { requiresAuth: false },
  },
  {
    path: '/login',
    name: 'AuthPage',
    component: AuthPage,
    meta: { requiresAuth: false },
  },
  // Страница заказов для студентов
  {
    path: '/orders',
    name: 'Orders',
    component: OrdersPage,
    meta: { requiresAuth: true, roles: ['student'] },
  },
  // Общая страница профиля для всех авторизованных пользователей
  {
    path: '/profile',
    name: 'Profile',
    component: () => {
      const userRole = localStorage.getItem('userRole');
      if (userRole === 'student') {
        return ProfilePage;
      } else if (userRole === 'admin') {
        return ProfileTeacher;
      } else if (userRole === 'company') {
        return ProfileCompany;
      } else {
        return ProfilePage;
      }
    },
    meta: { requiresAuth: true, roles: ['student', 'admin', 'company'] },
  },
  // Страница обработки заказов для админа (преподаватель = админ)
  {
    path: '/order_processing',
    name: 'OrderProcessing',
    component: ProcessingOrder,
    meta: { requiresAuth: true, roles: ['admin'] },
  },
  // Страница создания заявки для компании
  {
    path: '/create_order',
    name: 'CreateOrder',
    component: CompanyOrder,
    meta: { requiresAuth: true, roles: ['company'] },
  },
  // Страницы отслеживания заказов
  {
    path: '/tracking/student',
    name: 'StudentTracking',
    component: OrderTrackingStudent,
    meta: { requiresAuth: true, roles: ['student'] },
  },
  {
    path: '/tracking/teacher',
    name: 'TeacherTracking',
    component: OrderTrackingTeacher,
    meta: { requiresAuth: true, roles: ['admin'] },
  },
  {
    path: '/tracking/company',
    name: 'CompanyTracking',
    component: OrderTrackingCompany,
    meta: { requiresAuth: true, roles: ['company'] },
  },
  // Новый маршрут для страницы администраторов заказов
  {
    path: '/admin/orders',
    name: 'AdminsOrders',
    component: AdminOrders,
    meta: { requiresAuth: true, roles: ['admin'] },
  },
  // Новый маршрут для модуля документации (только для админа)
  {
    path: '/admin/docs',
    name: 'AdminDocs',
    component: AdminDocs,
    meta: { requiresAuth: true, roles: ['admin'] },
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Глобальный navigation guard для проверки авторизации и роли
router.beforeEach((to, from, next) => {
  const userRole = localStorage.getItem('userRole');
  if (to.meta.requiresAuth && !userRole) {
    return next({ name: 'AuthPage' });
  }
  if (to.meta.roles && userRole && !to.meta.roles.includes(userRole)) {
    return next({ name: 'Home' });
  }
  next();
});

export default router;
