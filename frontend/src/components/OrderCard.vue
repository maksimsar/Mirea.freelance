<template>
    <div class="card order-card mb-4">
      <div class="card-body">
        <h5 class="card-title">{{ order.title }}</h5>
        <p class="card-text">{{ order.description }}</p>
        <button class="btn btn-primary" @click="$emit('respond', order)">Откликнуться</button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'OrderCard',
    props: {
      order: {
        type: Object,
        required: true,
      },
    },
  };
  </script>
  
  <style scoped>
  .order-card {
    min-height: 200px; /* Минимальная высота карточки */
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 20px;
    border: 1px solid #ddd; /* Легкая граница */
    border-radius: 8px; /* Закругленные углы */
  }
  
  .card-title {
    font-size: 1.25rem;
    font-weight: bold;
  }
  
  .card-text {
    flex-grow: 1;
    margin-bottom: 15px; /* Отступ от кнопки */
  }
  </style>
  