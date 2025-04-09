<template>
    <div class="card p-3">
      <div class="card-div">      
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
.card-div {
  background-color: #2D3445;
  border-radius: 5px;
  color: #E0E0E0;
  padding: 10px;
  border: 1px #E0E0E0 solid;
}


.card-div .btn {
  background-color: #00b5c5;
  color: white;
}
.card-div .btn:hover {
  background-color: #037485;
  color: white;
}
  </style>
  