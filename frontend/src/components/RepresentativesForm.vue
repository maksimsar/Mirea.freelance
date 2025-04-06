<template>
  <div class="representatives-form">
    <h3 class="text-center">Представители компании</h3>
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
        <button class="btn btn-delete" @click="removeRepresentative(index)">Удалить</button>
      </div>
    </transition-group>
    <div class="button-wrapper">
  <button class="btn" @click="addRepresentative">Добавить представителя</button>
</div>

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
  border-radius: 5px;
  background-color: #2d3445;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
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
  border-radius: 5px;
  outline: none;
  flex: 1;
  border: 2px solid #00b5c5;
}
.button-wrapper {
  display: flex;
  justify-content: center;
  margin-top: 1rem;
}
.btn {
  background-color: #00b5c5;
  color: white;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}
.btn-delete {
  background-color: #FF007A;
  color: white;

}
.btn-delete:hover {
  background-color: #a1014f;
}

</style>
