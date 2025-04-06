<template>
  <div class="order-card" :style="cardStyle">
    <div class="card-header">
      <span>{{ order.company.name }}</span>
      <!-- Три точечки для редактирования (видны только в статусе "processing") -->
      <div v-if="order.status === 'processing'" class="edit-icon" @click.stop="editOrder">
        <i class="fa fa-ellipsis-v"></i>
      </div>
    </div>
    <div>
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
          return { backgroundColor: "#E0E0E0" };
        case "processed_positive":
          return { backgroundColor: "#00a36a" };
        case "processed_negative":
          return { backgroundColor: "#a1014f" }; 
        default:
          return { backgroundColor: "#E0E0E0" };
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
  color:#0D0F1A;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
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
