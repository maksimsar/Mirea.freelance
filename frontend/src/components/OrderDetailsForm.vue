<template>
  <div class="order-details-form">
    <h3 class="text-center">Описание заказа</h3>
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
  border-radius: 5px;
  background-color: #2d3445;
}


/* Группы полей */
.form-group {
  margin-bottom: 15px;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 5px;
  color: #e0e0e0;
}

/* Поле ввода (textarea) */
textarea {
  padding: 10px;
  font-size: 16px;
  border: 2px solid #00b5c5;
  border-radius: 5px;
  outline: none;
  resize: none;
  width: 100%;
  height: 120px;
}
</style>
