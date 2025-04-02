<!-- eslint-disable vue/no-mutating-props -->
<template>
    <div class="modal-overlay">
      <div class="modal-window animate__animated animate__zoomIn">
        <h4>Редактирование заказа</h4>
        <div class="form-group">
          <label>Название компании:</label>
          <!-- Изменения сразу влияют на order -->
          <input v-model="order.company.name" type="text" />
        </div>
        <div class="form-group">
          <label>Адрес:</label>
          <input v-model="order.company.address" type="text" />
        </div>
        <div class="buttons">
          <button class="btn-cancel" @click="cancelChanges">Отмена</button>
          <button class="btn-save" @click="saveChanges">Сохранить</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "EditOrderModal",
    props: {
      order: {
        type: Object,
        required: true,
      },
    },
    data() {
      return {
        // Создаём резервную копию заказа при открытии модалки
        backupOrder: JSON.stringify(this.order),
      };
    },
    methods: {
      saveChanges() {
        // Изменения уже внесены в объект order, просто закрываем модалку
        this.$emit("save");
      },
      cancelChanges() {
        // Откатываем изменения, используя резервную копию
        const backup = JSON.parse(this.backupOrder);
        Object.assign(this.order, backup);
        this.$emit("close");
      },
    },
  };
  </script>
  
  <style scoped>
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 9999;
  }
  .modal-window {
    background: #fff;
    padding: 1.5em;
    border-radius: 8px;
    min-width: 300px;
    max-width: 500px;
  }
  .form-group {
    margin-bottom: 1em;
  }
  .form-group label {
    font-weight: bold;
  }
  .form-group input {
    width: 100%;
    padding: 0.5em;
    margin-top: 0.3em;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  .buttons {
    display: flex;
    justify-content: flex-end;
    gap: 0.5em;
  }
  .btn-cancel,
  .btn-save {
    border: none;
    padding: 0.5em 1em;
    border-radius: 5px;
    cursor: pointer;
  }
  .btn-cancel {
    background-color: #6c757d;
    color: #fff;
  }
  .btn-cancel:hover {
    background-color: #5a6268;
  }
  .btn-save {
    background-color: #007bff;
    color: #fff;
  }
  .btn-save:hover {
    background-color: #0056b3;
  }
  </style>
  