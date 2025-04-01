import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import OrdersPage from '../views/OrdersPage.vue';
import ProfilePage from '../views/ProfilePage.vue';
import ProcessingOrder from '../views/ProcessingOrder.vue';
import CompanyOrder from '../views/CompanyOrder.vue';
import AuthPage from '../views/AuthPage.vue';

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
  // Страницы для студентов
  {
    path: '/orders',
    name: 'Orders',
    component: OrdersPage,
    meta: { requiresAuth: true, roles: ['student'] },
  },
  // Общая страница профиля доступна всем авторизованным пользователям
  {
    path: '/profile',
    name: 'Profile',
    component: ProfilePage,
    meta: { requiresAuth: true, roles: ['student', 'admin', 'company'] },
  },
  // Страница обработки заказов для админа
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
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Глобальный navigation guard для проверки авторизации и роли
router.beforeEach((to, from, next) => {
  // Получаем роль пользователя (например, из localStorage)
  const userRole = localStorage.getItem('userRole');

  if (to.meta.requiresAuth && !userRole) {
    // Если страница требует авторизации, а пользователь не вошёл, перенаправляем на страницу входа
    next({ name: 'AuthPage' });
  } else if (to.meta.roles && userRole && !to.meta.roles.includes(userRole)) {
    // Если роль пользователя не соответствует разрешённым для маршрута, перенаправляем на главную
    next({ name: 'Home' });
  } else {
    next();
  }
});

export default router;
