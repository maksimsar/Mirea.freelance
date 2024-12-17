<template>
    <div class="card p-3">
      <h5>Общая информация о компании</h5>
      <p><strong>Название компании:</strong> {{ user.companyName }}</p>
      
      <div v-for="(contact, index) in user.contacts" :key="index">
        <h6>Представитель {{ index + 1 }}</h6>
        <p><strong>Имя:</strong> {{ contact.name }}</p>
        <p>
          <strong>Номер телефона:</strong> {{ contact.phone }}
          <button class="btn btn-edit" @click="editField(`contacts[${index}].phone`)">Изменить</button>
        </p>
        <p>
          <strong>Telegram:</strong> {{ contact.telegram }}
          <button class="btn btn-edit" @click="editField(`contacts[${index}].telegram`)">Изменить</button>
        </p>
      </div>
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
  
  <style scoped>
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
  