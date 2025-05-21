<template>
  <div class="modal-overlay">
    <div class="modal-window animate__animated animate__zoomIn">
      <h4 class="text-center">Редактирование заказа</h4>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Название заказа:</label>
          <input v-model="localOrder.Title" type="text" />
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Описание:</label>
          <textarea v-model="localOrder.Description"></textarea>
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Бюджет:</label>
          <input v-model.number="localOrder.Budget" type="number" min="0" />
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Дедлайн:</label>
          <input v-model="localOrder.Deadline" type="date" />
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Требуемые специалисты:</label>
          <div class="roles-list">
            <div v-for="role in predefinedRoles" :key="role" class="role-item">
              <label class="checkbox-label">
                <input
                  type="checkbox"
                  :value="role"
                  v-model="localRequiredRoles"
                />
                <span>{{ role }}</span>
              </label>
            </div>
            <div v-for="customRole in customRoles" :key="'custom-' + customRole" class="role-item">
              <label class="checkbox-label">
                <input
                  type="checkbox"
                  :value="customRole"
                  v-model="localRequiredRoles"
                />
                <span>{{ customRole }}</span>
              </label>
            </div>
            <div class="custom-role">
              <input
                v-model="newCustomRole"
                type="text"
                placeholder="Добавить роль"
                @keyup.enter="addCustomRole"
              />
              <button @click="addCustomRole" :disabled="!newCustomRole">Добавить</button>
            </div>
          </div>
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Предпочтительный способ связи:</label>
          <p>{{ formattedContactMethods }}</p>
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Компания – Название:</label>
          <p>{{ localOrder.CompanyProfile?.name || 'Не указано' }}</p>
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Компания – Адрес:</label>
          <p>{{ localOrder.CompanyProfile?.address || 'Не указан' }}</p>
        </div>
      </transition>
      <transition name="fade-slide">
        <div class="form-group">
          <label>Контакты компании:</label>
          <div v-if="localOrder.CompanyProfile?.contacts">
            <div v-for="(contact, index) in localOrder.CompanyProfile.contacts" :key="index">
              <p>Представитель {{ index + 1 }}: {{ contact.name }}</p>
              <p>Телефон: {{ contact.phone || 'Не указан' }}</p>
              <p>Telegram: {{ contact.telegram || 'Не указан' }}</p>
            </div>
          </div>
          <p v-else>Контакты не указаны</p>
        </div>
      </transition>
      <div class="buttons">
        <button class="btn-cancel" @click="cancelChanges">Отмена</button>
        <button class="btn-save" @click="saveChanges">Сохранить</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: "EditOrderModal",
  props: {
    order: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      localOrder: {}, // Локальная копия заказа
      localRequiredRoles: [], // Локальный массив ролей
      newCustomRole: '',
      customRoles: [],
      predefinedRoles: [
        'Frontend Developer',
        'Backend Developer',
        'Designer',
        'QA Engineer',
        'Project Manager',
        'ML-engineer'
      ]
    };
  },
  computed: {
    formattedContactMethods() {
      if (!this.localOrder.PreferredContactMethods) return 'Не указаны';
      const methods = this.localOrder.PreferredContactMethods.split(',').map(method => {
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
  created() {
    // Создаём глубокую копию order
    this.localOrder = JSON.parse(JSON.stringify(this.order));
    // Инициализируем роли
    this.localRequiredRoles = this.localOrder.RequiredRoles
      ? this.localOrder.RequiredRoles.split(',').filter(role => role)
      : [];
    // Определяем кастомные роли
    this.customRoles = this.localRequiredRoles.filter(role => !this.predefinedRoles.includes(role));
  },
  methods: {
    addCustomRole() {
      if (this.newCustomRole && !this.localRequiredRoles.includes(this.newCustomRole)) {
        this.customRoles.push(this.newCustomRole);
        this.localRequiredRoles = [...this.localRequiredRoles, this.newCustomRole];
        this.newCustomRole = '';
      }
    },
    async saveChanges() {
      // Обновляем RequiredRoles в localOrder
      this.localOrder.RequiredRoles = this.localRequiredRoles.join(',');
      try {
        // Отправляем обновлённый заказ на бэкенд
        await axios.put(`/api/orders/${this.localOrder.Id}`, this.localOrder, {
          headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
        });
        // Отправляем обновлённый заказ в родительский компонент
        this.$emit('save', this.localOrder);
      } catch (error) {
        console.error('Ошибка при сохранении заказа:', error);
      }
    },
    cancelChanges() {
      // Закрываем модалку без мутации prop
      this.$emit('close');
    }
  }
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

.modal-window {
  background: #2D3445;
  padding: 1.5em;
  border-radius: 8px;
  min-width: 500px;
  max-width: 600px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal-window h4 {
  color: #00b5c5;
  font-size: 1.5rem;
  font-weight: bold;
}

.form-group {
  margin-bottom: 1em;
}

.form-group label {
  font-weight: bold;
  display: block;
  margin-bottom: 5px;
  color: #E0E0E0;
}

.form-group input,
.form-group textarea,
.form-group select {
  width: 100%;
  padding: 8px;
  background-color: #e0e0e0;
  border-radius: 5px;
  font-size: 1rem;
  box-sizing: border-box;
  color: #2D3445;
}

.form-group textarea {
  height: 100px;
}

.form-group p {
  margin: 5px 0;
  color: #E0E0E0;
}

.roles-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-top: 8px;
}

.role-item {
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

.custom-role {
  display: flex;
  gap: 10px;
  margin-top: 10px;
  margin-right: 5px;
}

.custom-role input {
  flex: 1;
  padding: 8px;
}

.custom-role button {
  background-color: #00b5c5;
  color: #fff;
  border: none;
  padding: 8px 12px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 0.9rem;
}

.custom-role button:hover {
  background-color: #037485;
}

.custom-role button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.buttons {
  display: flex;
  justify-content: flex-end;
  gap: 0.5em;
}

.btn-cancel,
.btn-save {
  border: none;
  padding: 0.5em 1em;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1rem;
}

.btn-cancel {
  background-color: #FF007A;
  color: #fff;
}

.btn-cancel:hover {
  background-color: #a1014f;
}

.btn-save {
  background-color: #00b5c5;
  color: #fff;
}

.btn-save:hover {
  background-color: #037485;
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

.modal-window::-webkit-scrollbar {
  width: 10px;
}

.modal-window::-webkit-scrollbar-track {
  background: #1c1e2a;
  border-radius: 5px;
}

.modal-window::-webkit-scrollbar-thumb {
  background-color: #037485;
  border-radius: 5px;
  border: 2px solid #1c1e2a;
}
</style>