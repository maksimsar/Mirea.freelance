<template>
  <div class="order-card" :style="cardStyle">
    <div class="card-header">
      <h3>{{ order.Title }}</h3>
      <!-- Иконка редактирования видна, если заказ находится в статусе "processing" -->
      <div v-if="order.Status === 'processing'" class="edit-icon" @click.stop="editOrder">
        <i class="fa fa-ellipsis-v"></i>
      </div>
    </div>
    <div class="card-body">
      <p><strong>Описание:</strong> {{ order.Description }}</p>
      <p>
        <strong>Компания:</strong>
        {{ companyName }} – {{ companyAddress }}
      </p>
      <p><strong>Бюджет:</strong> {{ order.Budget }} руб.</p>
      <p><strong>Дедлайн:</strong> {{ formattedDeadline }}</p>
      <p><strong>Статус:</strong> {{ order.Status }}</p>
    </div>
    <div class="card-footer">
      <!-- Кнопка "Начать обработку" для заказов со статусом "Open" или "unprocessed" -->
      <div v-if="order.Status === 'Open' || order.Status === 'unprocessed'">
        <button class="btn-action" @click="$emit('startProcessing', order)">
          Начать обработку
        </button>
      </div>
      <!-- Кнопки для заказов в процессе -->
      <div v-else-if="order.Status === 'processing'">
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
      // Определяем стиль карточки в зависимости от статуса заказа
      switch (this.order.Status) {
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
    formattedDeadline() {
      if (!this.order.Deadline) return "Не установлен";
      const d = new Date(this.order.Deadline);
      return d.toLocaleDateString();
    },
    companyName() {
      return this.order.CompanyProfile && this.order.CompanyProfile.name
        ? this.order.CompanyProfile.name
        : "Не указано";
    },
    companyAddress() {
      return this.order.CompanyProfile && this.order.CompanyProfile.address
        ? this.order.CompanyProfile.address
        : "Не указан";
    },
  },
  methods: {
    editOrder() {
      // Эмитируем событие для открытия модального окна редактирования
      this.$emit("editOrder", this.order);
    },
  },
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";

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
.card-header h3 {
  margin: 0;
  color: #007bff;
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
.card-body p {
  margin: 5px 0;
  font-size: 0.9rem;
  color: #555;
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
