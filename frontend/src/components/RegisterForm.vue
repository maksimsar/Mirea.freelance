<template>
    <div class="form-container">
      <form @submit.prevent="onSubmit">
        <input v-model="companyName" type="text" placeholder="Название компании" required />
        <input v-model="email" type="email" placeholder="Email" required />
        <input v-model="password" type="password" placeholder="Пароль" required />
        <button type="submit">Зарегистрироваться</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        companyName: '',
        email: '',
        password: ''
      };
    },
    methods: {
      onSubmit() {
        console.log('Регистрация компании:', this.companyName, this.email, this.password);
      },
    },
  };
  </script>
  
  <style scoped>
  /* Стиль аналогичен LoginForm.vue */
  @import './LoginForm.vue';
  </style>
  