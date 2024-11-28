<template>
    <div>
      <header class="bg-light py-4">
        <div class="container">
          <h1 class="text-center">Поможем всем!</h1>
        </div>
      </header>
  
      <main class="container my-4">
        <h2>Категории</h2>
        <div class="row row-cols-2 row-cols-md-4 g-3">
          <div v-for="category in categories" :key="category" class="col">
            <button class="btn btn-outline-success w-100">{{ category }}</button>
          </div>
        </div>
      </main>
  
      <section class="container my-4">
        <h2>Профили специалистов</h2>
        <div class="row row-cols-1 row-cols-md-3 g-4">
          <div v-for="profile in profiles" :key="profile.name" class="col">
            <div class="card">
              <div class="card-body">
                <h5 class="card-title">{{ profile.name }}</h5>
                <p class="card-text">{{ profile.specialty }}</p>
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
        categories: [
          'Сайты',
          'Дизайн',
          'Тексты',
          'Программирование',
          '3D Графика',
          'Рисунки и иллюстрации',
          'Аутсорсинг и консалтинг',
          'Реклама и маркетинг',
        ],
        profiles: [
          { name: 'Саранцев Максим', specialty: 'Бэкенд-разработчик' },
          { name: 'Владислав Кирьяков', specialty: 'ML' },
          { name: 'Рустам Искендеров', specialty: 'Базы данных' },
        ],
      };
    },
  };
  </script>
  