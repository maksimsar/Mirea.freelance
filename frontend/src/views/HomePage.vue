<template>
  <div>
    <!-- Header Section -->
    <header class="bg-light py-4">
      <div class="container">
        <h1 class="text-center">Поможем всем!</h1>
        <p class="text-center">Этот сайт поможет студентам РТУ МИРЭА и компаниям эффективно взаимодействовать для выполнения проектов.</p>
      </div>
    </header>

    <!-- Interactive Explanation Section with Tabs -->
    <main class="container my-4">
      <div class="row">
        <div class="col-4">
          <div class="list-group" id="list-tab" role="tablist">
            <a 
              class="list-group-item list-group-item-action" 
              :class="{ active: activeTab === 'home' }"
              @click="setActiveTab('home')"
            >
              Преимущества
            </a>
            <a 
              class="list-group-item list-group-item-action" 
              :class="{ active: activeTab === 'student' }"
              @click="setActiveTab('student')"
            >
              Для студентов
            </a>
            <a 
              class="list-group-item list-group-item-action" 
              :class="{ active: activeTab === 'teacher' }"
              @click="setActiveTab('teacher')"
            >
              Для преподавателей
            </a>
            <a 
              class="list-group-item list-group-item-action" 
              :class="{ active: activeTab === 'client' }"
              @click="setActiveTab('client')"
            >
              Для заказчиков
            </a>
          </div>
        </div>
        <div class="col-8">
          <div class="tab-content" id="nav-tabContent">
            <div v-show="activeTab === 'home'" class="tab-pane fade show active" id="list-home" role="tabpanel" aria-labelledby="list-home-list">
              <h5>Почему это выгодно?</h5>
              <p>Проект предоставляет уникальную возможность студентам РТУ МИРЭА выполнить реальные кейсы от компаний, развить навыки и заработать деньги, а заказчикам — получить качественное решение от перспективных специалистов.</p>
            </div>
            <div v-show="activeTab === 'student'" class="tab-pane fade" id="list-profile" role="tabpanel" aria-labelledby="list-profile-list">
              <h5>Преимущества для студентов</h5>
              <p>Студенты могут работать над реальными проектами, развивать профессиональные навыки и строить портфолио.</p>
            </div>
            <div v-show="activeTab === 'teacher'" class="tab-pane fade" id="list-messages" role="tabpanel" aria-labelledby="list-messages-list">
              <h5>Преимущества для преподавателей</h5>
              <p>Преподаватели смогут назначать студентов на реальные задачи, помогать им развивать навыки и координировать работу над проектами.</p>
            </div>
            <div v-show="activeTab === 'client'" class="tab-pane fade" id="list-settings" role="tabpanel" aria-labelledby="list-settings-list">
              <h5>Преимущества для заказчиков</h5>
              <p>Заказчики смогут получить решение своих задач от студентов, поддерживая инновации и развивая молодежь.</p>
            </div>
          </div>
        </div>
      </div>
    </main>

    <!-- Latest Orders Section -->
    <section class="container my-4">
      <h2>Последние заказы</h2>
      <div class>
        <div class="col">
          <div class="card">
            <h5 class="card-header">Разработка сайта</h5>
            <div class="card-body">
              <h5 class="card-title">Разработка корпоративного сайта для компании</h5>
              <p class="card-text">Требуется разработка функционального и привлекательного сайта для компании. Сроки: 2 месяца.</p>
              <a href="#" class="btn btn-primary">Перейти к заказу</a>
            </div>
          </div>
        </div>
        <div class="col">
          <div class="card">
            <h5 class="card-header">Дизайн логотипа</h5>
            <div class="card-body">
              <h5 class="card-title">Создание логотипа для стартапа</h5>
              <p class="card-text">Необходим уникальный и запоминающийся логотип для нового стартапа. Сроки: 1 месяц.</p>
              <a href="#" class="btn btn-primary">Перейти к заказу</a>
            </div>
          </div>
        </div>
        <div class="col">
          <div class="card">
            <h5 class="card-header">Маркетинг</h5>
            <div class="card-body">
              <h5 class="card-title">Разработка стратегии продвижения</h5>
              <p class="card-text">Нужна стратегия продвижения для нового продукта на рынке. Сроки: 1,5 месяца.</p>
              <a href="#" class="btn btn-primary">Перейти к заказу</a>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'HomePage',
  data() {
    return {
      activeTab: 'home', // Начальная активная вкладка
    };
  },
  methods: {
    setActiveTab(tab) {
      this.activeTab = tab; // Изменяет активную вкладку
    },
  },
};
</script>

<style scoped>
.list-group-item {
  cursor: pointer;
}

.list-group-item.active {
  background-color: #007bff;
  color: white;
}
</style>
