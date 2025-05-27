```vue
<template>
  <div class="auth-page">
    <div class="card auth-card shadow-lg">
      <div class="card-header text-center">
        <h3>{{ isLoginMode ? "Вход" : "Регистрация" }}</h3>
      </div>
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="mb-3">
            <label for="login" class="form-label">Логин</label>
            <input
              type="text"
              id="login"
              class="form-control"
              v-model="form.login"
              placeholder="Введите ваш логин"
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
          <div v-if="!isLoginMode" class="mb-3">
            <label for="role" class="form-label">Выберите роль</label>
            <select id="role" class="form-select" v-model="selectedRole" required>
              <option disabled value="">Выберите роль</option>
              <option value="Student">Студент</option>
              <option value="Mentor">Преподаватель</option>
              <option value="Company">Компания</option>
            </select>
          </div>
          <button type="submit" class="btn btn-primary w-100" :disabled="loading">
            <span v-if="loading" class="spinner-border spinner-border-sm me-2"></span>
            {{ isLoginMode ? "Войти" : "Зарегистрироваться" }}
          </button>
          <p v-if="error" class="error">{{ error }}</p>
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
import { loginUser, createUser } from '../api/api';

export default {
  name: 'AuthPage',
  data() {
    return {
      isLoginMode: true,
      form: {
        login: '',
        password: '',
        confirmPassword: '',
      },
      selectedRole: '',
      loading: false,
      error: '',
    };
  },
  methods: {
    toggleMode() {
      this.isLoginMode = !this.isLoginMode;
      this.form.login = '';
      this.form.password = '';
      this.form.confirmPassword = '';
      this.selectedRole = '';
      this.error = '';
    },
    async handleSubmit() {
      this.error = '';
      if (!this.isLoginMode && !this.selectedRole) {
        this.error = 'Пожалуйста, выберите роль';
        return;
      }
      if (!this.isLoginMode && this.form.password !== this.form.confirmPassword) {
        this.error = 'Пароли не совпадают!';
        return;
      }
      this.loading = true;
      try {
        if (this.isLoginMode) {
          // Логин
          console.log('Отправка на /auth/login:', {
            login: this.form.login,
            password: this.form.password,
          });
          const response = await loginUser({
            login: this.form.login,
            password: this.form.password,
          });
          console.log('Полный ответ /auth/login:', JSON.stringify(response, null, 2));
          const token = response.data.token;
          if (!token) {
            throw new Error('Токен не получен в ответе');
          }
          localStorage.setItem('token', token);

          // Извлекаем роль из JWT
          let userRole;
          try {
            const { jwtDecode } = await import('jwt-decode');
            const decoded = jwtDecode(token);
            console.log('Декодированный токен:', JSON.stringify(decoded, null, 2));
            userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            if (!userRole) {
              throw new Error('Роль не найдена в токене');
            }
            console.log('Роль до нормализации:', userRole);
            userRole = userRole.toLowerCase();
            const validRoles = ['student', 'mentor', 'company', 'admin'];
            if (!validRoles.includes(userRole)) {
              throw new Error(`Недопустимая роль: ${userRole}`);
            }
            console.log('Роль из токена:', userRole);
            localStorage.setItem('userRole', userRole);
            this.$router.push('/');
          } catch (err) {
            this.error = 'Ошибка получения роли: ' + err.message;
            console.error('Ошибка декодирования токена:', err);
            return;
          }
        } else {
          // Регистрация
          console.log('Отправка на /Users:', {
            login: this.form.login,
            password: this.form.password,
            role: this.selectedRole,
          });
          await createUser({
            login: this.form.login,
            password: this.form.password,
            role: this.selectedRole,
          });
          this.error = 'Регистрация успешна! Пожалуйста, войдите.';
          this.toggleMode();
        }
      } catch (error) {
        console.error('Ошибка запроса:', error.response || error);
        this.error = `Ошибка ${this.isLoginMode ? 'входа' : 'регистрации'}: ${
          error.response?.data?.message || error.message || 'Попробуйте снова'
        }`;
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
  background-color: #0D0F1A;
  animation: fadeIn 1s ease-out;
}

h3,
label {
  font-family: 'BezierSans-Regular';
}

.auth-card {
  width: 100%;
  max-width: 400px;
  animation: slideIn 0.8s ease-out;
  border-radius: 15px;
  overflow: hidden;
  color: #E0E0E0;
  background-color: #181B29;
}

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

.card-header {
  background-color: #FF007A;
  color: white;
  padding: 1rem;
  font-size: 1.25rem;
  font-weight: bold;
}

.card-footer {
  background-color: #FF007A;
}

.switch-mode-text {
  margin: 0;
  font-size: 0.875rem;
}

.btn {
  background-color: #00b5c5;
  color: white;
}

.btn:hover {
  background-color: #037485;
  color: white;
}

.btn-link {
  color: #007bff;
  text-decoration: none;
  background-color: #FF007A;
}

.btn-link:hover {
  text-decoration: underline;
  background-color: #FF007A;
}

.error {
  color: red;
  text-align: center;
  margin-top: 10px;
}
</style>
```