<template>
    <div class="order-tracking-company">
      <h1>Отслеживание заказов для компании</h1>
      <transition-group name="list" tag="div">
        <div v-for="order in orders" :key="order.id" class="order-card">
          <OrderSummary :order="order" />
          <ProgressBar :progress="order.progress" />
          <DetailedTimeline :order="order" />
        </div>
      </transition-group>
    </div>
  </template>
  
  <script>
  import OrderSummary from "@/components/OrderSummary.vue";
  import ProgressBar from "@/components/ProgressBar.vue";
  import DetailedTimeline from "@/components/DetailedTimeline.vue";
  
  export default {
    name: "OrderTrackingCompany",
    components: { OrderSummary, ProgressBar, DetailedTimeline },
    data() {
      return {
        orders: [
          {
            id: 1,
            name: "Заказ №1",
            summary: "Общая суть заказа №1",
            progress: 75,
            history: [
              { status: "начато", date: "2025-03-01", color: "blue" },
              { status: "в работе", date: "2025-03-05", color: "yellow" },
              { status: "выполнено", date: "2025-03-10", color: "green" },
            ],
            tasks: [],
          },
          {
            id: 2,
            name: "Заказ №2",
            summary: "Общая суть заказа №2",
            progress: 40,
            history: [
              { status: "начато", date: "2025-03-08", color: "blue" },
              { status: "в работе", date: "2025-03-12", color: "yellow" },
            ],
            tasks: [],
          },
        ],
      };
    },
  };
  </script>
  
  <style scoped>
  .order-tracking-company {
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
  .list-enter-active, .list-leave-active {
    transition: all 0.3s ease;
  }
  .list-enter-from, .list-leave-to {
    opacity: 0;
    transform: translateY(-10px);
  }
  </style>
  