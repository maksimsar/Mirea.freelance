<template>
    <div class="admin-orders">
      <h1 class="page-title">Доступные заказы для менторства</h1>
      <transition-group name="list" tag="div" class="orders-list">
        <div
          v-for="order in orders"
          :key="order.id"
          class="order-card animate__animated animate__fadeInUp"
        >
          <div class="order-info">
            <h2>{{ order.name }}</h2>
            <p>{{ order.summary }}</p>
            <p>
              <strong>Компания:</strong>
              {{ order.company.name }} – {{ order.company.address }}
            </p>
            <p v-if="order.mentor">
              <strong>Ментор:</strong> {{ order.mentor }}
            </p>
            <p v-else>
              <strong>Ментор:</strong> <span class="not-assigned">Не назначен</span>
            </p>
          </div>
          <div class="order-actions" v-if="!order.mentor">
            <button @click="takeMentorship(order.id)">
              Взять в менторство
            </button>
          </div>
        </div>
      </transition-group>
    </div>
  </template>
  
  <script>
  export default {
    name: "AdminOrders",
    data() {
      return {
        orders: [
          {
            id: 1,
            name: "Заказ №1",
            summary: "Описание заказа №1. Общая суть и задачи.",
            company: {
              name: "Компания 1",
              address: "ул. Примерная, 1",
            },
            mentor: null, // Заказ пока свободен
          },
          {
            id: 2,
            name: "Заказ №2",
            summary: "Описание заказа №2. Детали и этапы выполнения.",
            company: {
              name: "Компания 2",
              address: "ул. Примерная, 2",
            },
            mentor: null,
          },
          {
            id: 3,
            name: "Заказ №3",
            summary: "Описание заказа №3. Требуется опыт в определённой области.",
            company: {
              name: "Компания 3",
              address: "ул. Примерная, 3",
            },
            mentor: "admin", // Уже взят в менторство
          },
        ],
      };
    },
    methods: {
      takeMentorship(orderId) {
        const order = this.orders.find((o) => o.id === orderId);
        if (order && !order.mentor) {
          // В реальном приложении можно взять данные о текущем администраторе, здесь упрощенно
          order.mentor = "admin";
          alert(`Вы успешно взяли заказ "${order.name}" в менторство!`);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .admin-orders {
    padding: 1.5em;
    background-color: #f9f9f9;
    min-height: 100vh;
  }
  .page-title {
    color: #007bff;
    margin-bottom: 1em;
    text-align: center;
  }
  .orders-list {
    display: flex;
    flex-direction: column;
    gap: 1em;
  }
  .order-card {
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 1em;
    display: flex;
    justify-content: space-between;
    align-items: center;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }
  .order-info h2 {
    margin: 0 0 5px;
    font-size: 1.2rem;
    color: #007bff;
  }
  .order-info p {
    margin: 3px 0;
    font-size: 0.9rem;
    color: #555;
  }
  .not-assigned {
    color: #dc3545;
    font-weight: bold;
  }
  .order-actions button {
    background-color: #28a745;
    border: none;
    color: #fff;
    padding: 0.5em 1em;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  .order-actions button:hover {
    background-color: #218838;
  }
  /* Плавная анимация появления карточек */
  .list-enter-active,
  .list-leave-active {
    transition: all 0.3s ease;
  }
  .list-enter-from,
  .list-leave-to {
    opacity: 0;
    transform: translateY(-10px);
  }
  </style>
  