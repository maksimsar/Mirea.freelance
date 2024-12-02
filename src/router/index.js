import { createRouter, createWebHistory } from 'vue-router';

// Импорт страниц
import HomePage from '../views/HomePage.vue';
import OrdersPage from '../views/OrdersPage.vue';
import ProfilePage from '../views/ProfilePage';
import ProfileTeacher from '../views/ProfileTeacher.vue';
import ProfileCompany from '../views/ProfileCompany.vue';

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
  { path: '/', name: 'Home', component: HomePage },
  { path: '/orders', name: 'Orders', component: OrdersPage },
  { path: '/profile', name: 'Profile', component: ProfilePage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
