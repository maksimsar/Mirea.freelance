<template>
  <div class="representatives-form">
    <h2>Представители компании</h2>
    <transition-group name="fade-slide" tag="div">
      <div
        v-for="(rep, index) in representatives"
        :key="index"
        class="representative"
      >
        <input
          type="text"
          v-model="rep.name"
          placeholder="ФИО"
          required
        />
        <input
          type="tel"
          v-model="rep.phone"
          placeholder="Телефон"
          required
        />
        <input
          type="text"
          v-model="rep.telegram"
          placeholder="Telegram"
          required
        />
        <button class="btn-remove" @click="removeRepresentative(index)">Удалить</button>
      </div>
    </transition-group>
    <button class="btn-add" @click="addRepresentative">Добавить представителя</button>
  </div>
</template>

<script>
export default {
  props: {
    modelValue: {
      type: Array,
      required: true,
    },
  },
  computed: {
    representatives: {
      get() {
        return this.modelValue;
      },
      set(value) {
        this.$emit("update:modelValue", value);
      },
    },
  },
  methods: {
    addRepresentative() {
      this.representatives.push({ name: "", phone: "", telegram: "" });
    },
    removeRepresentative(index) {
      this.representatives.splice(index, 1);
    },
  },
};
</script>

<style scoped>
/* Общий стиль формы */
.representatives-form {
  margin-bottom: 20px;
  padding: 20px;
  border: 2px solid #007bff;
  border-radius: 10px;
  background-color: white;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* Заголовок */
h2 {
  color: #007bff;
  margin-bottom: 20px;
}

/* Элементы представителя */
.representative {
  display: flex;
  gap: 10px;
  align-items: center;
  margin-bottom: 15px;
}

input {
  padding: 10px;
  font-size: 14px;
  border: 1px solid #007bff;
  border-radius: 5px;
  outline: none;
  flex: 1;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

input:focus {
  border-color: #0056b3;
  box-shadow: 0 0 8px rgba(0, 123, 255, 0.5);
}

/* Кнопки */
.btn-remove {
  background-color: #ff4d4d;
  color: white;
  border: none;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-remove:hover {
  background-color: #cc0000;
}

.btn-add {
  background-color: #007bff;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.3s ease;
  display: block;
  margin: 10px auto 0;
}

.btn-add:hover {
  background-color: #0056b3;
  transform: scale(1.05);
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
