<template>
  <header>
    <nav class="navbar navbar-expand-lg navbar-dark bg-custom">
      <div class="container">
        <a class="navbar-brand" href="#">Mirea Freelance</a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav mx-auto">
            <!-- Всегда доступно -->
            <li class="nav-item">
              <router-link class="nav-link" to="/">Главная</router-link>
            </li>
            <!-- Если пользователь не авторизован -->
            <template v-if="!userRole">
              <li class="nav-item">
                <router-link class="nav-link" to="/login">Вход/Регистрация</router-link>
              </li>
            </template>
            <!-- Для авторизованных пользователей -->
            <template v-else>
              <!-- Меню для студента -->
              <template v-if="userRole === 'student'">
                <li class="nav-item">
                  <router-link class="nav-link" to="/orders">Заказы</router-link>
                </li>
              </template>
              <!-- Меню для админа -->
              <template v-if="userRole === 'admin'">
                <li class="nav-item">
                  <router-link class="nav-link" to="/order_processing">Обработка заказов</router-link>
                </li>
              </template>
              <!-- Меню для компании -->
              <template v-if="userRole === 'company'">
                <li class="nav-item">
                  <router-link class="nav-link" to="/create_order">Создать заявку</router-link>
                </li>
              </template>
              <!-- Общая страница профиля -->
              <li class="nav-item">
                <router-link class="nav-link" to="/profile">Профиль</router-link>
              </li>
              <!-- Кнопка Logout -->
              <li class="nav-item">
                <button class="nav-link btn-logout" @click="logout">Logout</button>
              </li>
            </template>
          </ul>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  name: "AppHeader",
  data() {
    return {
      userRole: localStorage.getItem("userRole"),
    };
  },
  methods: {
    logout() {
      localStorage.removeItem("userRole");
      this.userRole = null;
      this.$router.push({ name: "Home" });
    },
    syncUserRole() {
      this.userRole = localStorage.getItem("userRole");
    },
  },
  mounted() {
    window.addEventListener("storage", this.syncUserRole);
  },
  beforeUnmount() {
    window.removeEventListener("storage", this.syncUserRole);
  },
};
</script>

<style scoped>
.navbar {
  border-bottom: 2px solid #007bff;
}
.bg-custom {
  background-color: #007bff !important;
}
.nav-link {
  transition: color 0.3s ease, transform 0.3s ease;
  color: white !important;
}
.nav-link:hover {
  color: #acacac !important;
  transform: scale(1.1);
}
.btn-logout {
  background: none;
  border: none;
  color: white;
  cursor: pointer;
}
</style>
