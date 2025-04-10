<!-- eslint-disable vue/no-mutating-props -->
<template>
  <div class="modal-overlay">
    <div class="modal-window animate__animated animate__zoomIn">
      <h4 class="text-center">Редактирование заказа</h4>
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
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}
.modal-window {
  background: #2D3445;
  padding: 1.5em;
  border-radius: 8px;
  min-width: 500px;
  max-width: 600px;
  max-height: 90vh; 
overflow-y: auto; /* Прокрутка, если контента слишком много */
}

.modal-window h4 {
color: #00b5c5; /* Яркий цвет, соответствующий твоему дизайну */
font-size: 1.5rem; /* Можно увеличить размер шрифта, если нужно */
font-weight: bold; /* Жирный шрифт для выделения */
}

.form-group {
  margin-bottom: 1em;
}
.form-group label {
  font-weight: bold;
  display: block;
  margin-bottom: 5px;
  color:  #E0E0E0;
}
.form-group input,
.form-group textarea,
.form-group select {
  width: 100%;
  padding: 8px;
  background-color:  #e0e0e0;
  border-radius: 5px;
  font-size: 1rem;
  box-sizing: border-box;
  color:#2D3445;
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
  background-color: #FF007A;
  color: #fff;
}
.btn-cancel:hover {
  background-color: #a1014f;
}
.btn-save {
  background-color: #00b5c5;
  color: #fff;
}
.btn-save:hover {
  background-color: #037485;
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
  