<template>
  <header>
    <nav class="navbar navbar-expand-lg navbar-light bg-info">
      <div class="container">
        <!-- Логотип в левой части -->
        <a class="navbar-brand" href="#">Mirea Freelance</a>

        <!-- Бургер-меню для маленьких экранов -->
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        
        <!-- Меню навигации -->
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav mx-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/" @mouseover="handleMouseOver" @mouseleave="handleMouseLeave">Главная</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/orders" @mouseover="handleMouseOver" @mouseleave="handleMouseLeave">Заказы</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/profile" @mouseover="handleMouseOver" @mouseleave="handleMouseLeave">Профиль</router-link>
            </li>
              <li class="nav-item">
              <router-link class="nav-link" to="/order_processing" @mouseover="handleMouseOver" @mouseleave="handleMouseLeave">Обработка заказов</router-link>
            
            </li>
          </ul>
        </div>

        <!-- Кнопки Войти/Регистрация справа -->
        <div class="navbar-nav ml-auto">
          <router-link class="nav-link" to="/login" @mouseover="handleMouseOver" @mouseleave="handleMouseLeave">Войти</router-link>
        </div>
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  name: 'AppHeader',
  methods: {
    handleMouseOver(event) {
      event.target.style.transition = 'transform 0.3s ease';
      event.target.style.transform = 'scale(1.1)';
    },
    handleMouseLeave(event) {
      event.target.style.transform = 'scale(1)';
    }
  }
};
</script>

<style scoped>
.navbar {
  border-bottom: 2px solid #007bff; /* Добавим границу снизу для красоты */
}

/* Цвет фона navbar изменён на голубой */
.bg-info {
  background-color: #007bff !important;
}

/* Эффект при наведении на ссылки */
.nav-link {
  transition: color 0.3s ease, transform 0.3s ease;
  color: white !important;
}

.nav-link:hover {
  color: #acacac !important; /* Изменение цвета текста при наведении */
  transform: scale(1.1);
}

/* Убираем подчеркивание у ссылок */
.navbar-nav .nav-item .nav-link {
  text-decoration: none;
}

/* Кастомизация навигации для правой части */
.navbar-nav.ml-auto {
  margin-left: auto;
}

/* Убираем отступы на маленьких экранах */
.navbar-collapse {
  flex-grow: 0;
}
</style>
