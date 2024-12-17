<template>
    <div class="card p-3">
      <h5>Общая информация</h5>
      <p><strong>Фамилия:</strong> {{ user.surname }}</p>
      <p><strong>Имя:</strong> {{ user.name }}</p>
      <p><strong>Отчество:</strong> {{ user.patronymic }}</p>
      <p>
        <strong>Номер телефона:</strong> {{ user.phone }}
        <button class="btn btn-edit" @click="editField('phone')">Изменить</button>
      </p>
      <p>
        <strong>Telegram:</strong> {{ user.telegram }}
        <button class="btn btn-edit" @click="editField('telegram')">Изменить</button>
      </p>
      <p><strong>Средний балл:</strong> {{ user.grade }}</p>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      user: Object,
    },
    methods: {
      editField(field) {
        this.$emit('edit', field);
      },
    },
  };
  </script>
  
  <style>
  .btn-edit {
    color: #fff; /* Белый текст */
    background-color: #007bff; /* Синий фон */
    border: none; /* Убираем рамку */
    padding: 5px 10px; /* Пространство внутри кнопки */
    font-size: 14px; /* Размер текста */
    border-radius: 5px; /* Скруглённые углы */
    cursor: pointer; /* Указатель при наведении */
    transition: background-color 0.2s ease; /* Плавный переход цвета */
  }
  
  .btn-edit:hover {
    background-color: #0056b3; /* Более тёмный синий при наведении */
  }
  </style>
  