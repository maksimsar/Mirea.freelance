<template>
    <div class="order-tracking-teacher container mt-5">
      <h2  class="text-center mb-4">Проекты в работе</h2>
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
          <!-- Форма создания новой задачи -->
          <CreateTaskForm :orderId="order.id" @add-task="addTask" />
        </div>
      </transition-group>
    </div>
  </template>
  
  <script>
  import OrderSummary from "@/components/OrderSummary.vue";
  import ProgressBar from "@/components/ProgressBar.vue";
  import OrderHistory from "@/components/OrderHistory.vue";
  import CreateTaskForm from "@/components/CreateTaskForm.vue";
  
  export default {
    name: "OrderTrackingTeacher",
    components: { OrderSummary, ProgressBar, OrderHistory, CreateTaskForm },
    data() {
      return {
        orders: [
          {
            id: 1,
            name: "Заказ №1",
            summary: "Общая суть заказа №1",
            progress: 30,
            history: [
              { status: "в работе", date: "2025-03-01", color: "yellow" },
            ],
            tasks: [
              { id: 101, title: "Общая задача", completed: false },
            ],
          },
          {
            id: 2,
            name: "Заказ №2",
            summary: "Общая суть заказа №2",
            progress: 60,
            history: [
              { status: "в работе", date: "2025-03-10", color: "yellow" },
            ],
            tasks: [
              { id: 201, title: "Задача 1", completed: false },
            ],
          },
        ],
      };
    },
    methods: {
      addTask({ orderId, title }) {
        const order = this.orders.find(o => o.id === orderId);
        if (order) {
          // Генерируем уникальный id для задачи (например, на основе timestamp)
          const newTask = {
            id: Date.now(),
            title,
            completed: false,
          };
          order.tasks.push(newTask);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .order-tracking-teacher {
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
  