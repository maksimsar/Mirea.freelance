<template>
    <div class="detailed-timeline animate__animated animate__fadeIn">
      <h3>Этапы заказа</h3>
      <ul>
        <li v-for="(entry, index) in order.history" :key="index">
          <span class="date">{{ entry.date }}</span>
          <span class="status" :style="{ color: entry.color }">{{ entry.status }}</span>
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
    color: #007bff;
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
    border-bottom: 1px solid #eee;
    font-size: 0.85rem;
  }
  .date {
    color: #555;
  }
  .status {
    font-weight: bold;
  }
  </style>
  