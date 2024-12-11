<template>
    <div class="processing-order">
      <div
        class="column unprocessed animate__animated animate__fadeInLeft"
        @drop="onDrop('unprocessed', $event)"
        @dragover.prevent
      >
        <h3>Не обработан</h3>
        <transition-group name="list" tag="div">
          <SOrderCard
            v-for="(order, index) in orders.unprocessed"
            :key="index"
            :order="order"
            @dragstart="onDragStart"
          />
        </transition-group>
      </div>
      <div
        class="column processing animate__animated animate__fadeInDown"
        @drop="onDrop('processing', $event)"
        @dragover.prevent
      >
        <h3>В обработке</h3>
        <transition-group name="list" tag="div">
          <SOrderCard
            v-for="(order, index) in orders.processing"
            :key="index"
            :order="order"
            @dragstart="onDragStart"
          />
        </transition-group>
      </div>
      <div
        class="column processed animate__animated animate__fadeInRight"
        @drop="onDrop('processed', $event)"
        @dragover.prevent
      >
        <h3>Обработан</h3>
        <transition-group name="list" tag="div">
          <SOrderCard
            v-for="(order, index) in orders.processed"
            :key="index"
            :order="order"
            @dragstart="onDragStart"
          />
        </transition-group>
      </div>
    </div>
  </template>

<script>
import SOrderCard from "../components/SOrderCard.vue";

export default {
  name: "ProcessingOrder",
  components: { SOrderCard },
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
    };
  },
  methods: {
    onDragStart(event, order) {
      // Проверка: если order существует, сохраняем его в dataTransfer
      if (order) {
        console.log("Начато перетаскивание:", order);
        event.dataTransfer.setData("order", JSON.stringify(order));
      }
    },
    onDrop(newStatus, event) {
      console.log("Событие drop:", event);
      
      // Проверка: если данные в dataTransfer не существуют, возвращаем ошибку
      if (!event || !event.dataTransfer) {
        console.error("Ошибка: объект dataTransfer отсутствует");
        return;
      }

      const data = event.dataTransfer.getData("order");

      // Проверка: если данных нет или они невалидны
      if (!data) {
        console.error("Ошибка: данные order отсутствуют в dataTransfer");
        return;
      }

      try {
        const order = JSON.parse(data); // Попытка разобрать данные
        console.log("Полученные данные:", order);
        this.moveOrder(order, newStatus);
      } catch (e) {
        console.error("Ошибка при разборе JSON:", e);
      }
    },
    moveOrder(order, newStatus) {
      // Логика перемещения заказа по статусам
      this.orders[order.status] = this.orders[order.status].filter(
        (o) => o !== order
      );
      order.status = newStatus;
      this.orders[newStatus].push(order);
    },
  },  // Удалена лишняя запятая
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
}
.order-card {
  margin-bottom: 1em;
  padding: 1em;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  background-color: #f8f9fa;
}
.list-move {
  transition: transform 0.5s;
}
</style>

  