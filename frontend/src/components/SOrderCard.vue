<template>
  <div class="order-card" :style="cardStyle">
    <div class="card-header">
      <span>{{ order.company.name }}</span>
      <!-- Три точечки для редактирования (видны только в статусе "processing") -->
      <div v-if="order.status === 'processing'" class="edit-icon" @click.stop="editOrder">
        <i class="fa fa-ellipsis-v"></i>
      </div>
    </div>
    <div class="card-body">
      <p>{{ order.company.address }}</p>
    </div>
    <div class="card-footer">
      <!-- Кнопка "Начать обработку" для заказов "Не обработан" -->
      <div v-if="order.status === 'unprocessed'">
        <button class="btn-action" @click="$emit('startProcessing', order)">
          Начать обработку
        </button>
      </div>
      <!-- Кнопки для заказов "В обработке" -->
      <div v-else-if="order.status === 'processing'">
        <button class="btn-action approve" @click="$emit('approve', order)">
          Одобрить
        </button>
        <button class="btn-action reject" @click="$emit('reject', order)">
          Отклонить
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SOrderCard",
  props: {
    order: {
      type: Object,
      required: true,
    },
  },
  computed: {
    cardStyle() {
      // Определяем стиль карточки в зависимости от статуса
      switch (this.order.status) {
        case "processing":
          return { backgroundColor: "#fff9c4" }; // светло-жёлтый
        case "processed_positive":
          return { backgroundColor: "#d4edda" }; // светло-зелёный
        case "processed_negative":
          return { backgroundColor: "#f8d7da" }; // светло-красный
        default:
          return { backgroundColor: "#f8f9fa" };
      }
    },
  },
  methods: {
    editOrder() {
      // Эмитируем событие редактирования заказа
      this.$emit("editOrder", this.order);
    },
  },
};
</script>

<style scoped>
.order-card {
  position: relative;
  margin-bottom: 1em;
  padding: 1em;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  background-color: #f8f9fa;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
}
.edit-icon {
  cursor: pointer;
  color: #007bff;
}
.edit-icon:hover {
  color: #0056b3;
}
.card-body {
  margin-top: 0.5em;
}
.card-footer {
  margin-top: 0.5em;
}
.btn-action {
  background-color: #007bff;
  border: none;
  color: #fff;
  margin-right: 0.5em;
  padding: 0.5em 0.8em;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
.btn-action:hover {
  background-color: #0056b3;
}
.approve {
  background-color: #28a745;
}
.approve:hover {
  background-color: #218838;
}
.reject {
  background-color: #dc3545;
}
.reject:hover {
  background-color: #c82333;
}
</style>
