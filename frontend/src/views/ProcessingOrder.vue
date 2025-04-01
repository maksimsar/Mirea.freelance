<template>
  <div class="processing-order">
    <!-- Первая колонка: Не обработан -->
    <div class="column unprocessed animate__animated animate__fadeInLeft">
      <h3>Не обработан</h3>
      <transition-group name="list" tag="div">
        <SOrderCard
          v-for="(order, index) in orders.unprocessed"
          :key="index"
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
          v-for="(order, index) in orders.processing"
          :key="index"
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
          v-for="(order, index) in orders.processed"
          :key="index"
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
            company: {
              name: "Компания 1",
              ogrn: "123",
              inn: "456",
              address: "ул. Примерная, 1",
            },
            status: "unprocessed",
          },
          {
            company: {
              name: "Компания 2",
              ogrn: "124",
              inn: "457",
              address: "ул. Примерная, 2",
            },
            status: "unprocessed",
          },
        ],
        processing: [],
        processed: [],
      },
      editingOrder: null, // Заказ, который редактируем в модалке
    };
  },
  methods: {
    // Перевод из "Не обработан" -> "В обработке"
    moveToProcessing(order) {
      this.orders.unprocessed = this.orders.unprocessed.filter(o => o !== order);
      order.status = "processing";
      this.orders.processing.push(order);
    },
    // Одобрить заказ
    approveOrder(order) {
      this.orders.processing = this.orders.processing.filter(o => o !== order);
      order.status = "processed_positive"; // Заказ одобрен
      this.orders.processed.push(order);
    },
    // Отклонить заказ
    rejectOrder(order) {
      this.orders.processing = this.orders.processing.filter(o => o !== order);
      order.status = "processed_negative"; // Заказ отклонён
      this.orders.processed.push(order);
    },
    // Открываем модальное окно для редактирования – передаём сам объект заказа для двусторонней привязки
    openEditModal(order) {
      this.editingOrder = order;
    },
    // Сохраняем отредактированные данные – изменения уже отражены, просто закрываем модалку
    saveOrder() {
      this.editingOrder = null;
    },
  },
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";

/* Контейнер с тремя колонками */
.processing-order {
  display: flex;
  justify-content: space-between;
  background-color: #007bff;
  padding: 1em;
  gap: 1em;
  min-height: calc(100vh - 60px);
}

/* Оформление колонок */
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
}

/* Плавные перемещения карточек */
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
