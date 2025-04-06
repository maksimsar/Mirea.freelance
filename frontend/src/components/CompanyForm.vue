<template>
  <div class="company-form">
    <h3 class="text-center">O компании</h3>
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

.company-form {
  margin-bottom: 20px;
  padding: 20px;
  background-color:  #2D3445;
  border-radius: 5px;
}

h2 {
  color: #e0e0e0;
  margin-bottom: 20px;
  font-family: "BezierSans-Regular";
  font-weight: bold;
}

.form-group {
  margin-bottom: 15px;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 5px;
  color: #e0e0e0;
}

input {
  padding: 10px;
  font-size: 16px;
  border: 2px solid #00b5c5;
  border-radius: 5px;
  outline: none;
}
</style>
