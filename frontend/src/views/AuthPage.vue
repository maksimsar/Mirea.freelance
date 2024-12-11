<template>
    <div class="auth-page">
      <div class="card auth-card shadow-lg">
        <div class="card-header text-center">
          <h3>{{ isLoginMode ? "Вход" : "Регистрация" }}</h3>
        </div>
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label for="email" class="form-label">Email</label>
              <input
                type="email"
                id="email"
                class="form-control"
                v-model="form.email"
                placeholder="Введите ваш email"
                required
              />
            </div>
            <div class="mb-3">
              <label for="password" class="form-label">Пароль</label>
              <input
                type="password"
                id="password"
                class="form-control"
                v-model="form.password"
                placeholder="Введите ваш пароль"
                required
              />
            </div>
            <div v-if="!isLoginMode" class="mb-3">
              <label for="confirmPassword" class="form-label">Подтвердите пароль</label>
              <input
                type="password"
                id="confirmPassword"
                class="form-control"
                v-model="form.confirmPassword"
                placeholder="Повторите пароль"
                required
              />
            </div>
            <button type="submit" class="btn btn-primary w-100" :disabled="loading">
              <span v-if="loading" class="spinner-border spinner-border-sm me-2"></span>
              {{ isLoginMode ? "Войти" : "Зарегистрироваться" }}
            </button>
          </form>
        </div>
        <div class="card-footer text-center">
          <p class="switch-mode-text">
            {{ isLoginMode ? "Нет аккаунта?" : "Уже есть аккаунт?" }}
            <button class="btn btn-link p-0 ms-1" @click="toggleMode">
              {{ isLoginMode ? "Регистрация" : "Войти" }}
            </button>
          </p>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        isLoginMode: true, // Режим: true = Вход, false = Регистрация
        form: {
          email: "",
          password: "",
          confirmPassword: "",
        },
        loading: false,
      };
    },
    methods: {
      toggleMode() {
        this.isLoginMode = !this.isLoginMode;
        this.form.password = "";
        this.form.confirmPassword = "";
      },
      async handleSubmit() {
        if (!this.isLoginMode && this.form.password !== this.form.confirmPassword) {
          alert("Пароли не совпадают!");
          return;
        }
        this.loading = true;
        try {
          // Логика для входа или регистрации
          console.log(this.isLoginMode ? "Вход успешен" : "Регистрация успешна", this.form);
          alert(this.isLoginMode ? "Вы вошли в систему!" : "Вы зарегистрировались!");
        } catch (error) {
          alert("Произошла ошибка, попробуйте ещё раз.");
        } finally {
          this.loading = false;
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .auth-page {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: #f8f9fa;
    animation: fadeIn 1s ease-out;
  }
  
  .auth-card {
    width: 100%;
    max-width: 400px;
    animation: slideIn 0.8s ease-out;
    border-radius: 15px;
    overflow: hidden;
  }
  
  /* Анимации */
  @keyframes fadeIn {
    from {
      opacity: 0;
    }
    to {
      opacity: 1;
    }
  }
  
  @keyframes slideIn {
    from {
      transform: translateY(-20%);
      opacity: 0;
    }
    to {
      transform: translateY(0);
      opacity: 1;
    }
  }
  
  /* Визуальная эстетика */
  .card-header {
    background-color: #007bff;
    color: white;
    padding: 1rem;
    font-size: 1.25rem;
    font-weight: bold;
  }
  
  .card-footer {
    background-color: #f8f9fa;
  }
  
  .switch-mode-text {
    margin: 0;
    font-size: 0.875rem;
  }
  
  .btn-link {
    color: #007bff;
    text-decoration: none;
  }
  
  .btn-link:hover {
    text-decoration: underline;
  }
  </style>
  