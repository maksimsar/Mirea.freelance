<template>
  <div
    class="order-card"
    :style="cardStyle"
    draggable="true"
    @dragstart="onDragStart"
    @dragend="onDragEnd"
    @mouseover="hover = true"
    @mouseleave="hover = false"
    @click="handleClick"
  >
    <p class="company-name">{{ order.company.name }}</p>
    <p class="company-address">{{ order.company.address }}</p>

    <!-- Кнопки для выбора статуса (показываются только в обработке) -->
    <div v-if="order.status === 'processing' && showButtons" class="status-buttons">
      <button @click="approveOrder">Одобрить</button>
      <button @click="rejectOrder">Отклонить</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "SOrderCard",
  props: {
    order: Object,
  },
  data() {
    return {
      hover: false,
      showButtons: false, // Показывать ли кнопки для выбора статуса
    };
  },
  computed: {
    cardStyle() {
      let bgColor;
      switch (this.order.status) {
        case 'unprocessed':
          bgColor = '#f0f0f0'; // Серый
          break;
        case 'processing':
          bgColor = '#ffeb3b'; // Желтый
          break;
        case 'processed_positive':
          bgColor = '#4caf50'; // Зеленый
          break;
        case 'processed_negative':
          bgColor = '#f44336'; // Красный
          break;
        default:
          bgColor = '#f0f0f0';
      }
      return {
        backgroundColor: bgColor,
        transition: 'all 0.3s ease-in-out',
        transform: this.hover ? 'scale(1.05)' : 'scale(1)',
      };
    },
  },
  methods: {
    onDragStart(event) {
      event.dataTransfer.setData("order", JSON.stringify(this.order));
      event.dataTransfer.effectAllowed = "move";
    },
    onDragEnd() {
      this.$emit('dragend');
    },
    handleClick() {
      if (this.order.status === 'processing') {
        this.showButtons = !this.showButtons; // Показать/скрыть кнопки при клике на заказ
      }
    },
    approveOrder() {
      // Эмитируем событие изменения статуса
      this.$emit('statusChanged', { order: this.order, status: 'processed_positive' });
      this.showButtons = false; // Скрываем кнопки
    },
    rejectOrder() {
      // Эмитируем событие изменения статуса
      this.$emit('statusChanged', { order: this.order, status: 'processed_negative' });
      this.showButtons = false; // Скрываем кнопки
    },
  },
};
</script>

<style scoped>
.order-card {
  border: 1px solid #ddd;
  padding: 1em;
  margin-bottom: 0.5em;
  cursor: grab;
  border-radius: 5px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
}

.order-card:hover {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
}

.company-name {
  font-size: 1.1em;
  font-weight: bold;
  margin-bottom: 0.3em;
}

.company-address {
  font-size: 0.9em;
  color: #555;
}

.status-buttons {
  margin-top: 1em;
  display: flex;
  gap: 10px;
}

.status-buttons button {
  padding: 0.5em 1em;
  border-radius: 5px;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.status-buttons button:hover {
  background-color: #ddd;
}
</style>
