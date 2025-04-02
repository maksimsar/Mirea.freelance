<template>
    <div class="order-history animate__animated animate__fadeIn">
      <h3>История заказа</h3>
      <ul>
        <li
          v-for="(entry, index) in history"
          :key="index"
          :style="{ color: entry.color }"
        >
          {{ entry.date }} — {{ entry.status }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    name: "OrderHistory",
    props: {
      history: { type: Array, required: true },
    },
  };
  </script>
  
  <style scoped>
  .order-history {
    margin-top: 10px;
  }
  .order-history h3 {
    font-size: 1rem;
    margin-bottom: 5px;
  }
  .order-history ul {
    list-style: none;
    padding: 0;
    margin: 0;
  }
  .order-history li {
    font-size: 0.8rem;
  }
  </style>
  