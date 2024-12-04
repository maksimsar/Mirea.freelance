<template>
    <div class="profile-switcher">
      <h5>Выберите профиль:</h5>
      <ul class="nav nav-pills">
        <li class="nav-item">
          <router-link
            to="/profile"
            class="nav-link"
            :class="{ active: isActive('/profile') }"
          >
            Студент
          </router-link>
        </li>
        <li class="nav-item">
          <router-link
            to="/profile/teacher"
            class="nav-link"
            :class="{ active: isActive('/profile/teacher') }"
          >
            Преподаватель
          </router-link>
        </li>
        <li class="nav-item">
          <router-link
            to="/profile/company"
            class="nav-link"
            :class="{ active: isActive('/profile/company') }"
          >
            Компания
          </router-link>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    methods: {
      isActive(route) {
        return this.$route.path === route;
      },
    },
  };
  </script>
  
  <style scoped>
  .nav-pills .nav-link {
    color: #007bff;
    cursor: pointer;
  }
  
  .nav-pills .nav-link.active {
    background-color: #007bff;
    color: #fff;
  }
  </style>
  