import { createRouter, createWebHistory } from 'vue-router';

// Импорт страниц
import HomePage from '../views/HomePage.vue';
import OrdersPage from '../views/OrdersPage.vue';

const routes = [
  { path: '/', name: 'Home', component: HomePage },
  { path: '/orders', name: 'Orders', component: OrdersPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
