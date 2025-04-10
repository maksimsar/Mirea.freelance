
  <template>
    <div class="container mt-5">
      <h2 class="text-center mb-3">Профиль компании</h2>
      <button class="create-order-btn" @click="navigateToCreateOrder">Создать заказ</button>
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
            <CompanyInfo :user="user" @edit="openEditModal" />
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
  import ReviewsSection from '../components/ReviewsSection.vue';
  import ModalEdit from '../components/ModalEdit.vue';
  import CompanyInfo from '../components/CompanyInfo.vue';
  
  export default {
    name: 'ProfilePage',
    components: {
      ProfileAvatar,
      ReviewsSection,
      ModalEdit,
      CompanyInfo,
    },
    data() {
      return {
        user: {
          avatar: 'path/to/avatar.jpg',
          companyName: 'Tech Solutions LLC',
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
            },
          ],
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
      navigateToCreateOrder() {
        this.$router.push({ name: 'CreateOrder' });
      },
    },
  };
  </script>
  
  <style scoped>
  .create-order-btn {
    position: absolute;
    top: 100px; /* Смещено ниже */
    right: 130px;
    background-color: #00b5c5;
    color: #fff;
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
    font-size: 16px;
    cursor: pointer;
    
  }
  
  .create-order-btn:hover {
    background-color: #037485;
  }
  
  h2 {
  font-size: 2rem;
  font-family: 'BezierSans-Regular';
  text-shadow: #FF007A 1px 1px 1px;
}

.card {
  background-color:#0D0F1A;
}
  </style>
  