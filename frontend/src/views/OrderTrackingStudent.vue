<template>
    <div class="order-tracking-student container mt-5">
      <h2 class="text-center mb-4">Отслеживание заказов </h2>
      <transition-group name="list" tag="div">
        <div v-for="order in orders" :key="order.id" class="order-card mt-5">
          <OrderSummary :order="order" />
          <ProgressBar :progress="order.progress" />
          <OrderHistory :history="order.history" />
          <div class="tasks">
            <h3>Задачи:</h3>
            <ul>
              <li
                v-for="task in order.tasks"
                :key="task.id"
                :class="{
                  completed: task.completed,
                  pending: !task.completed,
                }"
              >
                {{ task.title }} <span v-if="task.completed">(Выполнено)</span>
                <span v-else>(В работе)</span>
              </li>
            </ul>
          </div>
        </div>
      </transition-group>
      </div>
  </template>
  
  <script>
  import OrderSummary from "@/components/OrderSummary.vue";
  import ProgressBar from "@/components/ProgressBar.vue";
  import OrderHistory from "@/components/OrderHistory.vue";
  
  export default {
    name: "OrderTrackingStudent",
    components: { OrderSummary, ProgressBar, OrderHistory },
    data() {
      return {
        orders: [
          {
            id: 1,
            name: "Заказ №1",
            summary: "Общая суть заказа №1",
            progress: 45,
            history: [
              { status: "в работе", date: "2025-03-01", color: "#00b5c5" },
              { status: "выполнено", date: "2025-03-05", color: "#00FF9F" },
            ],
            tasks: [
              { id: 101, title: "Общая задача", completed: true },
              { id: 102, title: "Индивидуальная задача", completed: false },
            ],
          },
          {
            id: 2,
            name: "Заказ №2",
            summary: "Общая суть заказа №2",
            progress: 80,
            history: [
              { status: "в работе", date: "2025-03-10", color: "#00b5c5" },
              { status: "выполнено", date: "2025-03-15", color: "#00FF9F" },
            ],
            tasks: [
              { id: 201, title: "Задача 1", completed: true },
              { id: 202, title: "Задача 2", completed: true },
            ],
          },
        ],
      };
    },
  };
  </script>
  
  <style scoped>
  .order-tracking-student {
    background-color: #0D0F1A;
    max-width: 1080px;
  }
  .order-card {
    max-width: 100%;
    padding: 2%;
    border-radius: 8px;
    background-color:  #2D3445;
    margin-bottom: 5%;
  }
  .tasks ul {
    list-style: none;
    padding: 0;
    margin-left: 4%;
  }
  .tasks li {
    font-size: 1rem;
    padding: 3px 0;
  }
  .tasks li.completed {
    color: #00FF9F;
  }
  .tasks li.pending {
    color: #00b5c5;
  }
  
  h2 {
  font-size: 2rem;
  font-family: 'BezierSans-Regular';
  text-shadow: #FF007A 1px 1px 1px;
}

h3 {
  margin-bottom: 1%;
  margin-top: 3%;
}
  </style>
  