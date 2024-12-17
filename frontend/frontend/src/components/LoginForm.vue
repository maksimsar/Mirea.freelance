<template>
    <div class="login-form-container d-flex justify-content-center align-items-center">
      <div class="login-form card shadow-lg">
        <div class="card-body">
          <h3 class="text-center mb-4">Регистрация</h3>
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
            <div class="mb-3">
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
            <button
              type="submit"
              class="btn btn-primary w-100"
              :disabled="loading"
            >
              <span v-if="loading" class="spinner-border spinner-border-sm me-2"></span>
              Зарегистрироваться
            </button>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        form: {
          email: "",
          password: "",
          confirmPassword: "",
        },
        loading: false,
      };
    },
    methods: {
      async handleSubmit() {
        if (this.form.password !== this.form.confirmPassword) {
          alert("Пароли не совпадают!");
          return;
        }
        this.loading = true;
        try {
          // Здесь вы можете добавить логику для отправки данных на сервер
          console.log("Регистрация успешна", this.form);
          alert("Вы успешно зарегистрировались!");
        } catch (error) {
          alert("Произошла ошибка при регистрации");
        } finally {
          this.loading = false;
        }
      },
    },
  };
  </script>
  
  <style scoped>
  /* Контейнер формы - центрируем содержимое */
  .login-form-container {
    height: 100vh;
    background: linear-gradient(to right, #6a11cb, #2575fc);
  }
  
  /* Анимация появления формы */
  .login-form {
    width: 100%;
    max-width: 400px;
    animation: fadeIn 0.8s ease-out;
  }
  
  /* Анимация */
  @keyframes fadeIn {
    from {
      opacity: 0;
      transform: scale(0.9);
    }
    to {
      opacity: 1;
      transform: scale(1);
    }
  }
  
  /* Кнопка при наведении */
  .btn-primary:hover {
    background-color: #1d4ed8;
    transition: background-color 0.3s ease-in-out;
  }
  </style>
  
  