<template>
  <div class="admin-orders">
    <h2 class="page-title">Доступные проекты для менторства</h2>
    <div class="orders-grid">
      <div
        v-for="order in orders"
        :key="order.id"
        class="order-card"
      >
        <!-- Левая часть -->
        <div class="order-left">
          <h3 class="order-title">{{ order.name }}</h3>
          <p class="label">
            <strong>Компания:</strong><br />
            {{ order.company.name }}, {{ order.company.address }}
          </p>
          <p class="label"><strong>Стоимость проекта:</strong></p>
          <p class="order-price">{{ order.price.toLocaleString() }} ₽</p>

          <!-- Если проект ещё не взят -->
          <div v-if="!order.taken" class="order-footer">
            <button class="btn" @click="takeMentorship(order.id)">
              Взять в менторство
            </button>
          </div>
          <!-- Если проект уже взят -->
          <div v-else class="success-message">
            <svg
              class="success-icon"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <rect x="1" y="1" width="22" height="22" rx="4" ry="4" fill="#34d399"/>
              <path d="M7 12l3 3 7-7" stroke="#fff" stroke-width="2" fill="none" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span>Успех! Проект перенесён в раздел «Мои проекты»</span>
          </div>
        </div>

        <!-- Правая часть -->
        <div class="order-right">
          <p class="label"><strong>Описание проекта:</strong></p>
          <p class="order-description">{{ order.description }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdminOrders",
  data() {
    return {
      orders: [
        {
          id: 1,
          name: "Разработка CRM-системы",
          description:
            "Создание веб-приложения для управления контактами клиентов, историей взаимодействий и отчётами по продажам. Интерфейс должен быть адаптивным, с гибкой настройкой воронок продаж и экспортом данных в CSV. Требуется дашборд с интерактивными графиками, система уведомлений и разграничение прав доступа.",
          company: { name: "ООО «Ромашка»", address: "Москва, Ленина 10" },
          price: 60000,
          taken: false
        },
        {
          id: 2,
          name: "Мобильное приложение доставки",
          description:
            "Кроссплатформенное приложение для заказа еды с картой и трекингом курьера. Пользовательский кабинет с историей заказов и избранными ресторанами. Система рейтингов и отзывов, офлайн-режим работы и поддержка push-уведомлений.",
          company: { name: "FoodExpress", address: "Санкт-Петербург, Невский 20" },
          price: 150000,
          taken: false
        },
        {
          id: 3,
          name: "Интернет-магазин электроники",
          description:
            "Полнофункциональный e-commerce сайт с каталогом, фильтрами и быстрой покупкой. Социальная авторизация, список желаемого и несколько платёжных провайдеров. Админ-панель с отчётами по продажам, управлением складом и акциями.",
          company: { name: "ShopElectro", address: "Новосибирск, Советская 5" },
          price: 95000,
          taken: false
        },
        {
          id: 4,
          name: "Корпоративный лендинг",
          description:
            "Одностраничный адаптивный сайт-визитка с анимациями при прокрутке и формой обратной связи. API-интеграция с CRM, SEO-оптимизация и раздел FAQ. Подключение чат-бота поддержки и скорость загрузки менее 2 секунд.",
          company: { name: "StartUpStudio", address: "Казань, Кремлёвская 12" },
          price: 30000,
          taken: false
        }
      ]
    };
  },
  methods: {
    takeMentorship(orderId) {
      const order = this.orders.find(o => o.id === orderId);
      if (order) {
        order.taken = true;
      }
    }
  }
};
</script>

<style scoped>
.admin-orders {
  max-width: 1200px;
  margin: 40px auto;
  padding: 0 20px;
}

.page-title {
  margin-top: 40px;
  margin-bottom: 30px;
  font-size: 2rem;
  text-align: center;
  color: #fff;
}

.orders-grid {
  display: grid;
  grid-template-columns: 1fr;
  grid-gap: 30px;
}

.order-card {
  display: grid;
  grid-template-columns: 2fr 3fr; /* Левая 2/5, правая 3/5 */
  background: #2d3445;
  border-radius: 12px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.5);
  color: #e0e0e0;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  overflow: hidden;
}

.order-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 24px rgba(0, 0, 0, 0.6);
}

.order-left {
  padding: 20px;
  border-right: 1px solid #3a3f4a;
}

.order-right {
  padding: 20px;
}

.order-title {
  font-size: 1.4rem;
  margin-bottom: 10px;
  font-weight: bold;
  color: #fff;
}

.label {
  color: #ffffff;
  font-size: 1rem;
  margin: 8px 0 4px;
}

.order-price {
  font-size: 2rem;
  font-weight: bold;
  color: #fff;
  margin: 4px 0 12px;
  text-shadow: #FF007A 1px 1px 1px;
}

.order-description {
  font-size: 1rem;
  line-height: 1.5;
  color: #e2e8f0;
  margin: 0;
}

.order-footer {
  margin-top: 20px;
}

.btn {
  background-color: #00b5c5;
  color: #fff;
  padding: 10px 18px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.btn:hover {
  background-color: #037485;
}

.success-message {
  display: flex;
  align-items: center;
  color: #34d399;
  font-size: 1rem;
  font-weight: 500;
}

.success-icon {
  width: 24px;
  height: 24px;
  margin-right: 8px;
}
</style>
