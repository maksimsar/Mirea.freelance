<template>
  <div class="order-details-form">
    <h3 class="text-center">Создание заказа</h3>
    <p class="info-message">
      После отправки заявки с вами свяжется преподаватель для обсуждения деталей проекта.
    </p>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="title" class="tooltip-container">
          Название проекта
          <span class="tooltip">?</span>
          <span class="tooltip-text">Укажите краткое и понятное название проекта, например, "Разработка веб-приложения".</span>
        </label>
        <input
          id="title"
          v-model="order.title"
          type="text"
          placeholder="Например, Разработка мобильного приложения"
          required
        />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="description" class="tooltip-container">
          Описание проекта
          <span class="tooltip">?</span>
          <span class="tooltip-text">Опишите цели, задачи и требования к проекту. Укажите, какие технологии или навыки нужны.</span>
        </label>
        <textarea
          id="description"
          v-model="order.description"
          placeholder="Опишите ваш проект..."
          required
        ></textarea>
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="budget" class="tooltip-container">
          Бюджет (₽)
          <span class="tooltip">?</span>
          <span class="tooltip-text">Укажите бюджет проекта в рублях. Если бюджет не определен, поставьте 0.</span>
        </label>
        <input
          id="budget"
          v-model.number="order.budget"
          type="number"
          placeholder="Введите рассчитываемый бюджет"
          min="0"
          required
        />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label for="deadline" class="tooltip-container">
          Дедлайн
          <span class="tooltip">?</span>
          <span class="tooltip-text">Выберите дату, к которой планируется завершить проект.</span>
        </label>
        <input
          id="deadline"
          v-model="order.deadline"
          type="date"
          required
        />
      </div>
    </transition>
    <transition name="fade-slide">
      <div class="form-group">
        <label>
          Предпочтительный способ связи
          <span class="tooltip">?</span>
          <span class="tooltip-text">Выберите, как с вами лучше связаться для обсуждения проекта.</span>
        </label>
        <div class="contact-methods-list">
          <div v-for="method in contactMethods" :key="method.value" class="contact-method-item">
            <label class="checkbox-label">
              <input
                type="checkbox"
                :value="method.value"
                v-model="order.preferredContactMethods"
              />
              <span>{{ method.label }}</span>
            </label>
          </div>
        </div>
      </div>
    </transition>
    <p v-if="error" class="error">Ошибка: {{ error }}</p>
    <p v-if="success" class="success">Заявка успешно создана!</p>
  </div>
</template>

<script>
/* eslint-disable no-unused-vars */
import axios from 'axios';

export default {
  data() {
    return {
      order: {
        title: '',
        description: '',
        budget: 0,
        deadline: '',
        preferredContactMethods: []
      },
      contactMethods: [
        { value: 'Calls', label: 'Звонки' },
        { value: 'Telegram', label: 'Telegram' },
        { value: 'InternalChat', label: 'Внутренний чат' },
        { value: 'Any', label: 'Не важно' }
      ],
      isSubmitting: false,
      error: '',
      success: false
    };
  },
  methods: {
    async submitOrder() {
      this.isSubmitting = true;
      this.error = '';
      this.success = false;
      try {
        const response = await axios.post('/api/orders', {
          title: this.order.title,
          description: this.order.description,
          budget: this.order.budget,
          deadline: this.order.deadline ? new Date(this.order.deadline).toISOString() : null,
          preferredContactMethods: this.order.preferredContactMethods.join(','),
          status: 'Open'
          // companyProfileId не отправляем, так как данные компании заполняются в других компонентах
        }, {
          headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
        });
        this.success = true;
        this.resetForm();
      } catch (error) {
        this.error = 'Ошибка при создании заявки: ' + (error.response?.data?.message || error.message);
      } finally {
        this.isSubmitting = false;
      }
    },
    resetForm() {
      this.order = {
        title: '',
        description: '',
        budget: 0,
        deadline: '',
        preferredContactMethods: []
      };
    }
  }
};
</script>

<style scoped>
.order-details-form {
  margin-bottom: 20px;
  padding: 20px;
  border-radius: 5px;
  background-color: #2d3445;
}

.form-group {
  margin-bottom: 15px;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 5px;
  color: #e0e0e0;
  position: relative;
}

.tooltip-container {
  position: relative;
  display: inline-block;
}

input, textarea {
  padding: 10px;
  font-size: 16px;
  border: 2px solid #00b5c5;
  border-radius: 5px;
  outline: none;
  width: 100%;
  box-sizing: border-box;
  background-color: #e0e0e0;
  color: #2d3445;
}

textarea {
  resize: none;
  height: 120px;
}

.contact-methods-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-top: 8px;
}

.contact-method-item {
  display: flex;
  align-items: center;
}

.checkbox-label {
  display: flex;
  align-items: center;
  gap: 8px;
  color: #e0e0e0;
  font-size: 0.9rem;
}

.checkbox-label input[type="checkbox"] {
  width: 16px;
  height: 16px;
  accent-color: #00b5c5;
  cursor: pointer;
}

.info-message {
  color: #00b5c5;
  text-align: center;
  margin-bottom: 15px;
  font-size: 13px;
}

.error {
  color: #ff007a;
  text-align: center;
}

.success {
  color: #00b5c5;
  text-align: center;
}

.tooltip {
  display: inline-block;
  width: 16px;
  height: 16px;
  background-color: #00b5c5;
  color: #fff;
  text-align: center;
  border-radius: 50%;
  margin-left: 5px;
  cursor: help;
  z-index: 2;
  font-size: 12px;
  line-height: 16px;
}

.tooltip-text {
  visibility: hidden;
  width: 200px;
  background-color: #1c1e2a;
  color: #e0e0e0;
  text-align: center;
  border-radius: 5px;
  padding: 5px;
  position: absolute;
  z-index: 1;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  opacity: 0;
  transition: opacity 0.3s ease, visibility 0.3s ease;
}

.tooltip:hover + .tooltip-text {
  visibility: visible;
  opacity: 1;
}

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: opacity 0.5s ease, transform 0.5s ease;
}

.fade-slide-enter-from,
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(20px);
}
</style>