<template>
    <div class="card p-3">
      <div class="card-div">   
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
  