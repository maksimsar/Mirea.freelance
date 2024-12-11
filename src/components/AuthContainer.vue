<template>
    <div class="auth-container">
      <transition name="fade">
        <component :is="isLogin ? 'LoginForm' : 'RegisterForm'" />
      </transition>
    </div>
  </template>
  
  <script>
  import LoginForm from './LoginForm.vue';
  import RegisterForm from './RegisterForm.vue';
  
  export default {
    components: { LoginForm, RegisterForm },
    props: {
      isLogin: {
        type: Boolean,
        required: true,
      },
    },
  };
  </script>
  
  <style scoped>
  .auth-container {
    width: 300px;
    margin: 0 auto;
  }
  
  .fade-enter-active,
  .fade-leave-active {
    transition: opacity 0.3s;
  }
  
  .fade-enter, .fade-leave-to {
    opacity: 0;
  }
  </style>
  