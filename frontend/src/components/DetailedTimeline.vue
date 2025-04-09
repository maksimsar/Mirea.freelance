<template>
    <div class="detailed-timeline animate__animated animate__fadeIn">
      <h3>Этапы заказа</h3>
      <ul>
        <li v-for="(entry, index) in order.history" :key="index">
          <span class="date">{{ entry.date }}</span>
          <span class="status">{{ entry.status }}</span>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    name: "DetailedTimeline",
    props: {
      order: { type: Object, required: true },
    },
  };
  </script>
  
  <style scoped>
  .detailed-timeline {
    margin-top: 10px;
  }
  .detailed-timeline h3 {
    font-size: 1rem;
    margin-bottom: 5px;
  }
  .detailed-timeline ul {
    list-style: none;
    padding: 0;
    margin: 0;
  }
  .detailed-timeline li {
    display: flex;
    justify-content: space-between;
    padding: 5px 0;
    font-size: 0.85rem;
  }
  .status {
    color: #00FF9F;
  }
  </style>
  