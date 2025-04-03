<template>
  <main class="container my-4">
    <div class="row">
      <header>
        <div class="container">
          <h1 class="text-center font-logo">MIREA FREELANCE</h1>
          <p class="text-center logo-text">Шаг в профессию через реальные проекты!</p>
        </div>
      </header>

      <div class="col-4">
        <div class="list-group" id="list-tab" role="tablist">
          <a class="list-group-item list-group-item-action" :class="{ active: activeTab === 'home' }" @click="setActiveTab('home')">О ПРОЕКТЕ</a>
          <a class="list-group-item list-group-item-action" :class="{ active: activeTab === 'student' }" @click="setActiveTab('student')">Студентам</a>
          <a class="list-group-item list-group-item-action" :class="{ active: activeTab === 'teacher' }" @click="setActiveTab('teacher')">Преподавателям</a>
          <a class="list-group-item list-group-item-action" :class="{ active: activeTab === 'client' }" @click="setActiveTab('client')">Заказчикам</a>
        </div>
      </div>

      <div class="col-8">
        <div class="tab-content" id="nav-tabContent">
          <div v-show="activeTab === 'home'" class="tab-pane fade show active" id="list-home" role="tabpanel">
            <h5>Уникальный сервис для будущих IT-специалистов.</h5>
            <p>Инновационная платформа, где компании публикуют реальные кейсы и проекты, а студенты <b>РТУ МИРЭА</b> получают возможность применить свои знания на практике.</p>
          </div>
          <div v-show="activeTab === 'student'" class="tab-pane fade show active" id="list-profile" role="tabpanel">
            <h5>Преимущества для студентов</h5>
            <p>
              - Работа над реальными проектами<br>
              - Получение опыта на практике<br>
              - Пополнение портфолио<br>
              - Возможность зарабатывать во время учебы<br>
              - Работа в сильных командах и новые знакомства<br>
              - Шанс получить оффер в компанию<br>
              - Бонусы во время учебы от университета
            </p>
          </div>
          <div v-show="activeTab === 'teacher'" class="tab-pane fade show active" id="list-messages" role="tabpanel">
            <h5>Преимущества для преподавателей</h5>
            <p>
              - Интеграция практических проектов в образовательный процесс <br>
              - Помощь студентам в развитии профессиональных навыков <br>
              - Контроль и координация деятельности студентов для более успешной реализации проектов <br>
              - Поднятие авторитета университета у работодателей <br>
            </p>
          </div>
          <div v-show="activeTab === 'client'" class="tab-pane fade show active" id="list-settings" role="tabpanel">
            <h5>Преимущества для заказчиков</h5>
            <p>
              - Получение решений для своих задач от молодых разработчиков при поддержке проверенных опытных специалистов <br>
              - Возможность найти талантливых разработчиков и привлечь их в команду <br>
              - Возможность экономии путем выгодного сотрудничества
            </p>
          </div>
        </div>
      </div>
    </div>
    <section class="container my-4">
      <h2 class="text-center">Последние заказы</h2>
      <div class="row">
        <div class="col">
          <div class="card">
            <h4 class="card-header">Разработка сайта</h4>
            <div class="card-body">
              <h5 class="card-title">Разработка корпоративного сайта для компании</h5>
              <p class="card-text">Требуется разработка функционального и привлекательного сайта для компании. Сроки: 2 месяца.</p>
              <a href="#" class="btn">Перейти к заказу</a>
            </div>
          </div>
        </div>
        <div class="col">
          <div class="card">
            <h4 class="card-header">Дизайн логотипа</h4>
            <div class="card-body">
              <h5 class="card-title">Создание логотипа для стартапа</h5>
              <p class="card-text">Необходим уникальный и запоминающийся логотип для нового стартапа. Сроки: 1 месяц.</p>
              <a href="#" class="btn">Перейти к заказу</a>
            </div>
          </div>
        </div>
        <div class="col">
          <div class="card">
            <h4 class="card-header">Маркетинг</h4>
            <div class="card-body">
              <h5 class="card-title">Разработка стратегии продвижения</h5>
              <p class="card-text">Нужна стратегия продвижения для нового продукта на рынке. Сроки: 1,5 месяца.</p>
              <a href="#" class="btn">Перейти к заказу</a>
            </div>
          </div>
        </div>
      </div>
    </section>
      <h5 class="text-center"> Присоединяйтесь к MIREA FREELANCE и начните путь к успешной карьере через реальные проекты!</h5>
      <ScrollToTop />
  </main>
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
  background-color: #303841;
  color: #E0E0E0;
  border: none;
  font-family: "BezierSans-Regular";
  font-size: 100%;
}

.list-group-item:hover {
  background-color: #00b5c5;
  color: white;
}

.list-group-item.active {
  background-color: #037485;
  color: white;
}

.btn {
  background-color: #00b5c5;
  color: white;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}
h5 {
 font-weight:bold;
 letter-spacing: 2px;

}
.font-logo {
  font-family: "BezierSans-Regular";
  font-size: 10vw;
  opacity: 0;
  animation: ani 3s forwards;
}
@keyframes ani {
  0% { opacity: 0; }
  100% { opacity: 1; 
  text-shadow: #FF007A 5px 5px 5px;}
}

.logo-text {
  font-family: "BezierSans-Regular";
  opacity: 0;  /* Начальное значение непрозрачности */
  animation: ani2 6s forwards; 
  font-size: 2vw;
  letter-spacing:10%;
}
@keyframes ani2 {
  0% {opacity: 0;}
  100% {opacity: 1;}
}

.container {
  margin-bottom: 10%;
  margin-top:10%;
}

.card {
  background-color: #303841;
  color: #E0E0E0;
  margin-bottom: 10px;
}

.card-header{
  background-color: #1A1F2A;
  color: #E0E0E0;
}
h2 {
  font-family: "BezierSans-Regular";
  margin: 5%;
}
</style>
