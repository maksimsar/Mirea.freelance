import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.min.css'; // Подключение стилей
import 'bootstrap'; // Подключение JS


createApp(App).use(router).mount('#app');
