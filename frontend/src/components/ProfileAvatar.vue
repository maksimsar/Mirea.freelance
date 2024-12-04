<template>
    <div class="text-center">
      <!-- Аватар -->
      <img
        :src="currentAvatar"
        @error="onImageError"
        alt="Avatar"
        class="rounded-circle img-thumbnail"
        width="130"
        height="130"
      />
      <!-- Кнопка изменения аватара -->
      <button class="btn btn-primary mt-3" @click="changeAvatar">
        Изменить аватарку
      </button>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      avatar: {
        type: String,
        default: 'https://via.placeholder.com/150',
      },
    },
    data() {
      return {
        currentAvatar: this.avatar, // Локальное состояние для аватара
      };
    },
    methods: {
      onImageError() {
        // Замена на заглушку, если аватар не загрузился
        this.currentAvatar = 'https://via.placeholder.com/150';
      },
      changeAvatar() {
        // Заглушка для функции изменения аватара
        alert('Функция изменения аватарки пока не реализована.');
      },
    },
  };
  </script>
  
  <style scoped>
  .text-center {
    text-align: center;
  }
  
  img {
    object-fit: cover; /* Адаптация изображения */
    border: 2px solid #007bff; /* Обводка в корпоративном стиле */
  }
  
  .btn-primary {
    background-color: #007bff;
    border-color: #007bff;
  }
  
  .btn-primary:hover {
    background-color: #0056b3;
    border-color: #004085;
  }
  </style>
  