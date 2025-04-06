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
      <!--
      <p><strong>Описание:</strong> {{ order.Description }}</p>
      <p>
        <strong>Компания:</strong>
        {{ companyName }} – {{ companyAddress }}
      </p>
      <p><strong>Бюджет:</strong> {{ order.Budget }} руб.</p>
      <p><strong>Дедлайн:</strong> {{ formattedDeadline }}</p>
      <p><strong>Статус:</strong> {{ order.Status }}</p>
-->
      <p>{{ order.company.address }}</p>
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
          return { backgroundColor: "#E0E0E0" };
        case "processed_positive":
          return { backgroundColor: "#00a36a" };
        case "processed_negative":
          return { backgroundColor: "#a1014f" }; 
        default:
          return { backgroundColor: "#E0E0E0" };
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
  color:#0D0F1A;
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
  color: #00b5c5;
}
.edit-icon:hover {
  color: #037485;
}
.card-body {
  margin-top: 0.5em;
  background-color: #e0e0e0;
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
  background-color:  #00b5c5 ;
  border: none;
  color: #fff;
  margin-right: 0.5em;
  padding: 0.5em 0.8em;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}
.btn-action:hover {
  background-color: 	#037485;
}
.approve {
  background-color:  #00da87;
}
.approve:hover {
  background-color: #00a36a;
}
.reject {
  background-color: #FF007A;
}
.reject:hover {
  background-color: #a1014f;
}
</style>
