<template>
  <div class="processing-order">
    <!-- Первая колонка: Не обработан -->
    <div class="column unprocessed animate__animated animate__fadeInLeft">
      <h3>Не обработан</h3>
      <transition-group name="list" tag="div">
        <SOrderCard
          v-for="order in orders.unprocessed"
          :key="order.Id"
          :order="order"
          @startProcessing="moveToProcessing"
        />
      </transition-group>
    </div>

    <!-- Вторая колонка: В обработке -->
    <div class="column processing animate__animated animate__fadeInDown">
      <h3>В обработке</h3>
      <transition-group name="list" tag="div">
        <SOrderCard
          v-for="order in orders.processing"
          :key="order.Id"
          :order="order"
          @approve="approveOrder"
          @reject="rejectOrder"
          @editOrder="openEditModal"
        />
      </transition-group>
    </div>

    <!-- Третья колонка: Обработан -->
    <div class="column processed animate__animated animate__fadeInRight">
      <h3>Обработан</h3>
      <transition-group name="list" tag="div">
        <SOrderCard
          v-for="order in orders.processed"
          :key="order.Id"
          :order="order"
        />
      </transition-group>
    </div>

    <!-- Модальное окно для редактирования (если выбрано) -->
    <EditOrderModal
      v-if="editingOrder"
      :order="editingOrder"
      @close="editingOrder = null"
      @save="saveOrder"
    />
  </div>
</template>

<script>
import SOrderCard from "../components/SOrderCard.vue";
import EditOrderModal from "../components/EditOrderModal.vue";

export default {
  name: "ProcessingOrder",
  components: { SOrderCard, EditOrderModal },
  data() {
    return {
      orders: {
        unprocessed: [
          {
            Id: 1,
            Title: "Заказ №1",
            Description: "Описание заказа №1. Общая суть и задачи.",
            Status: "unprocessed",
            Budget: 10000,
            Deadline: "2025-04-20",
            CompanyProfile: { name: "Компания 1", address: "ул. Примерная, 1" },
            // Дополнительные коллекции (FreelancerProfiles, Feedbacks) можно добавить по необходимости
          },
          {
            Id: 2,
            Title: "Заказ №2",
            Description: "Описание заказа №2. Детали и этапы выполнения.",
            Status: "unprocessed",
            Budget: 15000,
            Deadline: "2025-05-10",
            CompanyProfile: { name: "Компания 2", address: "ул. Примерная, 2" },
          },
        ],
        processing: [],
        processed: [],
      },
      editingOrder: null, // Заказ для редактирования
    };
  },
  methods: {
    moveToProcessing(order) {
      // Удаляем заказ из не обработанных
      this.orders.unprocessed = this.orders.unprocessed.filter(o => o.Id !== order.Id);
      order.Status = "processing";
      this.orders.processing.push(order);
    },
    approveOrder(order) {
      this.orders.processing = this.orders.processing.filter(o => o.Id !== order.Id);
      order.Status = "processed_positive";
      this.orders.processed.push(order);
    },
    rejectOrder(order) {
      this.orders.processing = this.orders.processing.filter(o => o.Id !== order.Id);
      order.Status = "processed_negative";
      this.orders.processed.push(order);
    },
    openEditModal(order) {
      // Передаём сам объект заказа для двусторонней привязки
      this.editingOrder = order;
    },
    saveOrder() {
      // Изменения уже сохранены, просто закрываем модальное окно
      this.editingOrder = null;
    },
  },
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";

.processing-order {
  display: flex;
  justify-content: space-between;
  background-color: #007bff;
  padding: 1em;
  gap: 1em;
  min-height: calc(100vh - 60px);
}
.column {
  flex: 1;
  background-color: #fff;
  border-radius: 5px;
  padding: 1em;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
.column h3 {
  margin-bottom: 1em;
  text-align: center;
  color: #007bff;
}
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
