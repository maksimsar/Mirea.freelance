<template>
    <div class="container mt-4">
      <!-- Компонент переключения профилей -->
      <ProfileSwitcher />
    
      <div class="row mt-4">
        <div class="col-md-4">
          <ProfileAvatar :avatar="user.avatar" />
          <ReviewsSection :reviews="user.reviews" />
        </div>
        <div class="col-md-8">
          <CompanyInfo :user="user" @edit="openEditModal" />
        </div>
      </div>
    
      <ModalEdit
        v-if="currentField"
        :placeholder="getPlaceholder()"
        v-model:modelValue="currentValue"
        @save="saveEdit"
      />
    </div>
  </template>
  
  <script>
  import { Modal } from 'bootstrap'; // Используем объектный импорт
  import ProfileAvatar from '../components/ProfileAvatar.vue';
  import ReviewsSection from '../components/ReviewsSection.vue';
  import ModalEdit from '../components/ModalEdit.vue';
  import ProfileSwitcher from '../components/ProfileSwitcher.vue'; // Импортируем новый компонент
  import CompanyInfo from '../components/CompanyInfo.vue'; // Импортируем новый компонент для компании
  
  export default {
    name: 'ProfilePage',
    components: {
      ProfileAvatar,
      ReviewsSection,
      ModalEdit,
      ProfileSwitcher,
      CompanyInfo, // Добавляем компонент компании
    },
    data() {
      return {
        user: {
          avatar: 'path/to/avatar.jpg',
          companyName: 'Tech Solutions LLC', // Название компании
          contacts: [
            {
              name: 'Дмитрий Иванов',
              telegram: '@dmitriy_ivanov',
              phone: '+7-999-123-45-67',
            },
            {
              name: 'Анастасия Кузнецова',
              telegram: '@nastya_kuznetsova',
              phone: '+7-999-234-56-78',
            }
          ], // Контакты представителей
          reviews: [
            {
              customerFeedback: 'Прекрасная работа! Все выполнено в срок.',
              customerRating: 5,
              teacherFeedback: 'Компания продемонстрировала отличные навыки.',
              teacherRating: 5,
            },
            {
              customerFeedback: 'Задание выполнено хорошо, но были мелкие недочёты.',
              customerRating: 4,
              teacherFeedback: 'Рекомендую лучше изучить тему оптимизации.',
              teacherRating: 4,
            },
            {
              customerFeedback: 'Все было сделано, но позже указанного срока.',
              customerRating: 3,
              teacherFeedback: 'Работа средняя, не хватило глубины анализа.',
              teacherRating: 3,
            },
          ],
        },
        currentField: null,
        currentValue: '',
      };
    },
    methods: {
      openEditModal(field) {
        this.currentField = field;
        this.currentValue = this.user[field];
    
        // Инициализация модального окна
        const modalElement = document.getElementById('editModal');
        if (modalElement) {
          const modal = Modal.getInstance(modalElement) || new Modal(modalElement);
          modal.show();
        } else {
          console.error('Modal element not found.');
        }
      },
      getPlaceholder() {
        return this.currentField === 'phone' ? '+7-***-***-**-**' : '@username';
      },
      saveEdit(newValue) {
        if (this.currentField) {
          this.user[this.currentField] = newValue;
          this.currentField = null;
          this.currentValue = '';
        }
      },
    },
  };
  </script>
  
  <style scoped>
  /* Дополнительные стили, если нужно */
  </style>
  