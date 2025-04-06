<template>
    <div class="order-tracking-company container mt-5">
      <h2 class="text-center md-4">Заказы в работе</h2>
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

  </style>
  