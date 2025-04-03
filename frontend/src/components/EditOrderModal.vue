<!-- eslint-disable vue/no-mutating-props -->
<template>
    <div class="modal-overlay">
      <div class="modal-window animate__animated animate__zoomIn">
        <h4>Редактирование заказа</h4>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Название заказа:</label>
            <input v-model="order.Title" type="text" />
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Описание:</label>
            <textarea v-model="order.Description"></textarea>
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Бюджет:</label>
            <input v-model.number="order.Budget" type="number" />
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Дедлайн:</label>
            <input v-model="order.Deadline" type="date" />
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Статус:</label>
            <select v-model="order.Status">
              <option value="Open">Open</option>
              <option value="processing">Processing</option>
              <option value="processed_positive">Processed (Positive)</option>
              <option value="processed_negative">Processed (Negative)</option>
            </select>
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Компания – Название:</label>
            <input v-model="order.CompanyProfile.name" type="text" />
          </div>
        </transition>
        <transition name="fade-slide">
          <div class="form-group">
            <label>Компания – Адрес:</label>
            <input v-model="order.CompanyProfile.address" type="text" />
          </div>
        </transition>
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
        // Резервная копия для отката изменений
        backupOrder: JSON.stringify(this.order),
      };
    },
    methods: {
      saveChanges() {
        // Изменения уже внесены в объект order – просто закрываем модалку
        this.$emit("save");
      },
      cancelChanges() {
        // Восстанавливаем исходное состояние заказа
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
    max-width: 600px;
  }
  .form-group {
    margin-bottom: 1em;
  }
  .form-group label {
    font-weight: bold;
    display: block;
    margin-bottom: 5px;
    color: #007bff;
  }
  .form-group input,
  .form-group textarea,
  .form-group select {
    width: 100%;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 1rem;
    box-sizing: border-box;
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
    font-size: 1rem;
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
  .fade-slide-enter-active,
  .fade-slide-leave-active {
    transition: opacity 0.5s ease, transform 0.5s ease;
  }
  .fade-slide-enter-from,
  .fade-slide-leave-to {
    opacity: 0;
    transform: translateY(20px);
  }
  </style>
  