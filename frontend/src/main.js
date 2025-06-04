/* eslint-disable */
import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.min.css'; // Подключение стилей
import 'bootstrap'; // Подключение JS
import DocTreeNode from '@/components/DocTreeNode.vue';


const app = createApp(App);

app.component('DocTreeNode', DocTreeNode);
app.use(router);
app.mount('#app');
