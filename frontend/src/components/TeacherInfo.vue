<template>
    <div class="card">
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
      <p><strong>Рейтинг:</strong> {{ user.rating }}</p>
      <p><strong>Сфера разработки:</strong> {{ user.developmentArea }}</p>
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
.card {
  background-color:  #0D0F1A;
  color:#E0E0E0;
}
.btn {
  background-color: #00b5c5;
  color: white;
}
.btn:hover {
  background-color: #037485;
  color: white;
}
  </style>
  