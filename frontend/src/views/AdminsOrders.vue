<template>
    <div class="admin-orders mt-5">
      <h2 class="page-title text-center mb-5">Доступные проекты для менторства</h2>
      <transition-group name="list" tag="div" class="orders-list">
        <div
          v-for="order in orders"
          :key="order.id"
          class="order-card animate__animated animate__fadeInUp"
        >
          <div class="order-info">
            <h3>{{ order.name }}</h3>
            <p>{{ order.summary }}</p>
            <p>
              <strong>Компания:</strong>
              {{ order.company.name }} – {{ order.company.address }}
            </p>
            <p v-if="order.mentor">
              <strong>Ментор: </strong>
              <span class="mentor-assigned">{{ order.mentor }}</span>
            </p>
            <p v-else>
              <strong>Ментор:</strong> <span class="not-assigned">Не назначен</span>
            </p>
          </div>
          <div class="order-actions" v-if="!order.mentor">
            <button class="btn" @click="takeMentorship(order.id)">
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
  max-width: 1200px;
  margin: 0 auto; /* Центрируем контейнер */
  padding: 0 20px;
}
    h2 {
    font-size: 2rem;
    font-family: 'BezierSans-Regular';
    text-shadow: #FF007A 1px 1px 1px;
  }
  .order-card {
    min-height: 200px; 
    max-width: 100%;
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
  
  h3{
    font-size: 1.3rem;
    font-family: 'BezierSans-Regular';
    text-shadow: #037485 1px 1px 1px;
  }
  
  .card-text {
    flex-grow: 1;
    margin-bottom: 15px;
  }

  .btn {
  background-color: #00b5c5;
  color: white;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}
.not-assigned {
  color:#FF007A;
}

.mentor-assigned {
  color: #00FF9F;
}
  </style>
  