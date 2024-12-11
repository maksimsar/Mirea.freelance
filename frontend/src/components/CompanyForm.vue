<template>
  <div class="company-form">
    <h2>Информация о компании</h2>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="name">Название компании</label>
        <input id="name" v-model="company.name" type="text" placeholder="Введите название компании" />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="ogrn">ОГРН/ОГРНИП</label>
        <input id="ogrn" v-model="company.ogrn" type="text" placeholder="Введите ОГРН/ОГРНИП" />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="inn">ИНН</label>
        <input id="inn" v-model="company.inn" type="text" placeholder="Введите ИНН" />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="address">Адрес</label>
        <input id="address" v-model="company.address" type="text" placeholder="Введите адрес" />
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  props: {
    modelValue: {
      type: Object,
      required: true,
    },
  },
  computed: {
    company: {
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
/* Основной стиль формы */
.company-form {
  margin-bottom: 20px;
  padding: 20px;
  border: 2px solid #007bff;
  border-radius: 10px;
  background-color: white;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.company-form:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 12px rgba(0, 0, 0, 0.2);
}

/* Заголовок */
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

input {
  padding: 10px;
  font-size: 16px;
  border: 1px solid #007bff;
  border-radius: 5px;
  outline: none;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

input:focus {
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
