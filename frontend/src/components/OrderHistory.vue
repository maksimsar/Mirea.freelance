<template>
    <div class="order-history animate__animated animate__fadeIn">
      <h3>История заказа: </h3>
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
    margin-top: 1%;
  }
  .order-history h3 {
  margin-bottom: 1%;
}
  .order-history ul {
    list-style: none;
    padding: 0;
    margin-left: 4%;
  }
  .order-history li {
    font-size: 1rem;
  }
  </style>
  