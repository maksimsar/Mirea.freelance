<template>
    <div class="order-summary animate__animated animate__fadeIn">
      <h2>{{ order.name }}</h2>
      <p>{{ order.summary }}</p>
    </div>
  </template>
  
  <script>
  export default {
    name: "OrderSummary",
    props: {
      order: { type: Object, required: true },
    },
  };
  </script>
  
  <style scoped>
  .order-summary h2 {
    margin-left: 1%;
    font-size: 1.8rem;
    color: #E0E0E0;
    text-shadow: #037485 1px 1px 1px;
    font-family: 'BezierSans-Regular';
    
  }
  .order-summary p {
    font-size: 1rem;
    color: #E0E0E0;
    margin-left: 4%;
  }
  </style>
  