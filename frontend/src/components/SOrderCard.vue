<template>
  <div class="order-card" :style="cardStyle">
    <div class="card-header">
      <h3>{{ order.Title }}</h3>
      <!-- Иконка редактирования видна, если заказ находится в статусе "processing" -->
      <div v-if="order.Status === 'processing'" class="edit-icon" @click.stop="editOrder">
        <i class="fa fa-ellipsis-v"></i>
      </div>
    </div>

    <div class="card-body" :style="cardStyle">
      <p><strong>Название:</strong> {{ order.Title }}</p>
      <p><strong>Описание:</strong> {{ order.Description }}</p>
      <p><strong>Бюджет:</strong> {{ order.Budget }} руб.</p>
      <p><strong>Дедлайн:</strong> {{ formattedDeadline }}</p>
      <p><strong>Требуемые роли:</strong> {{ order.RequiredRoles || 'Не указаны' }}</p>
      <p><strong>Предпочтительный способ связи:</strong> {{ formattedContactMethods }}</p>
      <p><strong>Компания:</strong> {{ companyName }}</p>
      <p><strong>Адрес:</strong> {{ companyAddress }}</p>
      <div v-if="order.CompanyProfile && order.CompanyProfile.contacts">
        <p><strong>Контакты:</strong></p>
        <div v-for="(contact, index) in order.CompanyProfile.contacts" :key="index">
          <p>Представитель {{ index + 1 }}: {{ contact.name }}</p>
          <p>Телефон: {{ contact.phone || 'Не указан' }}</p>
          <p>Telegram: {{ contact.telegram || 'Не указан' }}</p>
        </div>
      </div>
      <p><strong>Статус:</strong> {{ statusDisplay }}</p>
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
      switch (this.order.Status) {
        case "processing":
          return { backgroundColor: "#E0E0E0" };
        case "processed_positive":
          return { backgroundColor: "#00a36a", color: "#e0e0e0" };
        case "processed_negative":
          return { backgroundColor: "#a1014f", color: "#e0e0e0" };
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
      return this.order.CompanyProfile?.name || "Не указано";
    },
    companyAddress() {
      return this.order.CompanyProfile?.address || "Не указан";
    },
    statusDisplay() {
      switch (this.order.Status) {
        case "unprocessed":
          return "Не обработан";
        case "processing":
          return "В обработке";
        case "processed_positive":
          return "Обработан (Одобрен)";
        case "processed_negative":
          return "Обработан (Отклонен)";
        default:
          return this.order.Status;
      }
    },
    formattedContactMethods() {
      if (!this.order.PreferredContactMethods) return "Не указаны";
      const methods = this.order.PreferredContactMethods.split(',').map(method => {
        switch (method.trim()) {
          case 'Calls': return 'Звонки';
          case 'Telegram': return 'Telegram';
          case 'InternalChat': return 'Внутренний чат';
          case 'Any': return 'Не важно';
          default: return method;
        }
      });
      return methods.filter(m => m).join(', ');
    }
  },
  methods: {
    editOrder() {
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
  color: #0D0F1A;
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
}

.card-body p {
  margin: 5px 0;
  font-size: 0.9rem;
}

.card-footer {
  margin-top: 0.5em;
}

.btn-action {
  background-color: #00b5c5;
  border: none;
  color: #fff;
  margin-right: 0.5em;
  padding: 0.5em 0.8em;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-action:hover {
  background-color: #037485;
}

.approve {
  background-color: #00da87;
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