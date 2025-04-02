<template>
    <div class="order-tracking-student">
      <h1>Отслеживание заказов для студента</h1>
      <transition-group name="list" tag="div">
        <div v-for="order in orders" :key="order.id" class="order-card">
          <OrderSummary :order="order" />
          <ProgressBar :progress="order.progress" />
          <OrderHistory :history="order.history" />
          <div class="tasks">
            <h4>Задачи:</h4>
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
              { status: "в работе", date: "2025-03-01", color: "yellow" },
              { status: "выполнено", date: "2025-03-05", color: "green" },
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
              { status: "в работе", date: "2025-03-10", color: "yellow" },
              { status: "выполнено", date: "2025-03-15", color: "green" },
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
    padding: 1em;
    background-color: #f1f1f1;
  }
  .order-card {
    background-color: #fff;
    border: 1px solid #ddd;
    border-radius: 5px;
    padding: 1em;
    margin-bottom: 1em;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
  }
  .tasks ul {
    list-style: none;
    padding: 0;
  }
  .tasks li {
    font-size: 0.85rem;
    padding: 3px 0;
  }
  .tasks li.completed {
    color: green;
  }
  .tasks li.pending {
    color: orange;
  }
  .list-enter-active, .list-leave-active {
    transition: all 0.3s ease;
  }
  .list-enter-from, .list-leave-to {
    opacity: 0;
    transform: translateY(-10px);
  }
  </style>
  