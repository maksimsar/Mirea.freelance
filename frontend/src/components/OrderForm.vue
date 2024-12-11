<template>
  <div class="order-form">
    <h1>Оформление заказа</h1>
    <transition name="fade-slide">
      <RepresentativesForm v-model="representatives" />
    </transition>
    <transition name="fade-slide">
      <CompanyForm v-model="company" />
    </transition>
    <transition name="fade-slide">
      <OrderDetailsForm v-model="orderDetails" />
    </transition>
    <button class="btn-submit" @click="submitOrder">Отправить заказ</button>
  </div>
</template>

<script>
import RepresentativesForm from "./RepresentativesForm.vue";
import CompanyForm from "./CompanyForm.vue";
import OrderDetailsForm from "./OrderDetailsForm.vue";

export default {
  components: { RepresentativesForm, CompanyForm, OrderDetailsForm },
  data() {
    return {
      representatives: [],
      company: {},
      orderDetails: "",
    };
  },
  methods: {
    submitOrder() {
      console.log("Заказ отправлен:", {
        representatives: this.representatives,
        company: this.company,
        orderDetails: this.orderDetails,
      });
      alert("Форма успешно отправлена!");
    },
  },
};
</script>

<style scoped>
/* Общий стиль формы */
.order-form {
  max-width: 900px;
  margin: 0 auto;
  padding: 20px;
  border: 2px solid #007bff;
  border-radius: 10px;
  background-color: white;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.order-form:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 12px rgba(0, 0, 0, 0.2);
}

/* Заголовок */
h1 {
  color: #007bff;
  margin-bottom: 20px;
  text-align: center;
}

/* Кнопка */
.btn-submit {
  background-color: #007bff;
  color: white;
  padding: 12px 24px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  display: block;
  margin: 20px auto;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.btn-submit:hover {
  background-color: #0056b3;
  transform: scale(1.05);
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
