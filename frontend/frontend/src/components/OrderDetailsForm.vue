<template>
  <div class="order-details-form">
    <h2>Описание заказа</h2>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="order-details">Подробное описание заказа</label>
        <textarea
          id="order-details"
          v-model="orderDetails"
          placeholder="Опишите ваш заказ..."
        ></textarea>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  props: {
    modelValue: {
      type: String,
      required: true,
    },
  },
  computed: {
    orderDetails: {
      get() {
        return this.modelValue;
      },
      set(value) {
        this.$emit("update:modelValue", value);
      },
    },
  },
};
</script>

<style scoped>
/* Общий стиль контейнера формы */
.order-details-form {
  margin-bottom: 20px;
  padding: 20px;
  border: 2px solid #007bff;
  border-radius: 10px;
  background-color: white;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.order-details-form:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 12px rgba(0, 0, 0, 0.2);
}

/* Заголовок формы */
h2 {
  color: #007bff;
  margin-bottom: 20px;
}

/* Группы полей */
.form-group {
  margin-bottom: 15px;
  display: flex;
  flex-direction: column;
}

label {
  font-weight: bold;
  margin-bottom: 5px;
  color: #007bff;
}

/* Поле ввода (textarea) */
textarea {
  padding: 10px;
  font-size: 16px;
  border: 1px solid #007bff;
  border-radius: 5px;
  outline: none;
  resize: none;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
  width: 100%;
  height: 120px;
  font-family: 'Arial', sans-serif;
  line-height: 1.5;
}

textarea:focus {
  border-color: #0056b3;
  box-shadow: 0 0 8px rgba(0, 123, 255, 0.5);
}

/* Анимации */
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
