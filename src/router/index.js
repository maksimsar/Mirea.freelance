import { createRouter, createWebHistory } from 'vue-router';

// Импорт страниц
import HomePage from '../views/HomePage.vue';
import OrdersPage from '../views/OrdersPage.vue';
import ProfilePage from '../views/ProfilePage.vue';
import ProfileTeacher from '../views/ProfileTeacher.vue';
import ProfileCompany from '../views/ProfileCompany.vue';
import CompanyOrder from '../views/CompanyOrder.vue';
import ProcessingOrder from '../views/ProcessingOrder.vue'; // Импорт страницы обработки заказов

const routes = [
  {
    path: '/profile/teacher',
    name: 'TeacherProfile',
    component: ProfileTeacher,
  },
  {
    path: '/profile/company',
    name: 'CompanyProfile',
    component: ProfileCompany,
  },
  {
    path: '/profile/company/create_order',
    name: 'CreateOrder',
    component: CompanyOrder,
  },
  {
    path: '/',
    name: 'Home',
    component: HomePage,
  },
  {
    path: '/orders',
    name: 'Orders',
    component: OrdersPage,
  },
  {
    path: '/profile',
    name: 'Profile',
    component: ProfilePage,
  },
  {
    path: '/order_processing', // Добавленный маршрут
    name: 'OrderProcessing',
    component: ProcessingOrder,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
