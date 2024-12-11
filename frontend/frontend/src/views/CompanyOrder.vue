<template>
    <div class="company-order-page">
      <h1> </h1>
      <OrderForm />
    </div>
  </template>
  
  <script>
  import OrderForm from '../components/OrderForm.vue';
  
  export default {
    name: 'CompanyOrder',
    components: {
      OrderForm,
    },
  };
  </script>
  
  <style scoped>
  .company-order-page {
    padding: 20px;
    max-width: 1200px;
    margin: 0 auto;
  }
  </style>
  