<template>
    <div class="card order-card mb-4">
      <div class="card-body">
        <h5 class="card-title">{{ order.title }}</h5>
        <p class="card-text">{{ order.description }}</p>
        <button class="btn" @click="$emit('respond', order)">Откликнуться</button>
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
    min-height: 200px; 
    max-width: 90%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 20px;
    border-radius: 8px;
    background-color: #2D3445;
    color:  #E0E0E0;
    transition: color 0.3s ease, transform 0.3s ease;
    margin-bottom: 5%;
  }
  .card-body {
    background-color: #2D3445;
  }

  .order-card:hover {
    transform: scale(1.05);
  }
  
  .card-title {
    font-size: 1.3rem;
    font-family: 'BezierSans-Regular';
  }
  
  .card-text {
    flex-grow: 1;
    margin-bottom: 15px; /* Отступ от кнопки */
  }

  .btn {
  background-color: #00b5c5;
  color: white;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}
  </style>

  