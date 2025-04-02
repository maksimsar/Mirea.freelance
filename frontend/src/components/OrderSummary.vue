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
    margin: 0 0 5px;
    font-size: 1.2rem;
    color: #007bff;
  }
  .order-summary p {
    font-size: 0.9rem;
    color: #555;
  }
  </style>
  