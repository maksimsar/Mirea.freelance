<template>
  <div class="order-form mt-5">
      <h2 class="page-title text-center mb-5">Оформление заказа</h2>
    <transition name="fade-slide">
      <CompanyForm v-model="company" />
    </transition>
    <transition name="fade-slide">
      <RepresentativesForm v-model="representatives" />
    </transition>
    <transition name="fade-slide">
      <OrderDetailsForm v-model="orderDetails" />
    </transition>
    <div class="button-wrapper">
    <button class="btn px-8 py-3" @click="submitOrder">Оформить заказ</button>
  </div>
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
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
  border-radius: 10px;
  background-color: #0D0F1A;
}
.btn {
  background-color: #00b5c5;
  color: white;
  font-size: 120%;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}
.button-wrapper {
  display: flex;
  justify-content: center;
  margin-top: 1rem;
}
</style>
