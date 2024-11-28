<template>
    <div id="app">
      <AppHeader />
      <router-view />
      <AppFooter />
    </div>
  </template>
  
  <script>
  import AppHeader from './components/AppHeader.vue';
  import AppFooter from './components/AppFooter.vue';
  
  export default {
    name: 'App',
    components: {
      AppHeader,
      AppFooter,
    },
  };
  </script>
  
  <style>
  /* Глобальные стили можно добавлять здесь или в main.css */
  </style>
  