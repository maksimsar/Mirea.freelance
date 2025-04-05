<template>
  <div class="container mt-5">
    <h2 class="text-center mb-3">Профиль</h2>
    <div class="row g-4 align-items-start">
      <!-- Левая колонка -->
      <div class="col-md-4">
        <div class="card p-3 shadow-sm">
          <ProfileAvatar :avatar="user.avatar" />
        </div>
        <div class="card p-3 mt-4">
          <ReviewsSection :reviews="user.reviews" />
        </div>
      </div>

      <!-- Правая колонка -->
      <div class="col-md-8">
        <div class="card p-4">
          <TeacherInfo :user="user" @edit="openEditModal" />
        </div>
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
import { Modal } from 'bootstrap';
import ProfileAvatar from '../components/ProfileAvatar.vue';
import TeacherInfo from '../components/TeacherInfo.vue';
import ReviewsSection from '../components/ReviewsSection.vue';
import ModalEdit from '../components/ModalEdit.vue';

export default {
  name: 'ProfilePage',
  components: {
    ProfileAvatar,
    TeacherInfo,
    ReviewsSection,
    ModalEdit,
  },
  data() {
    return {
      user: {
        avatar: 'path/to/avatar.jpg',
        name: 'Иван',
        surname: 'Иванов',
        patronymic: 'Иванович',
        phone: '+7-999-123-45-67',
        telegram: '@ivan_ivanov',
        rating: 4.8,
        developmentArea: 'Web-разработка',
        reviews: [
          {
            customerFeedback: 'Прекрасная работа! Все выполнено в срок.',
            customerRating: 5,
            teacherFeedback: 'Студент продемонстрировал отличные навыки.',
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
          {
            customerFeedback: 'Работа выполнена качественно и быстро.',
            customerRating: 5,
            teacherFeedback: 'Студент превосходно справился с задачей.',
            teacherRating: 5,
          },
          {
            customerFeedback: 'Все хорошо, но не хватило креативности.',
            customerRating: 4,
            teacherFeedback: 'Можно добавить больше примеров в работе.',
            teacherRating: 4,
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
  h2 {
  font-size: 2rem;
  font-family: 'BezierSans-Regular';
  text-shadow: #FF007A 1px 1px 1px;
}

.card {
  background-color:#0D0F1A;
}
</style>
