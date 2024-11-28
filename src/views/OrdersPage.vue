<template>
    <main class="container mt-5">
      <h2 class="text-center mb-4">Какая у вас задача?</h2>
      <div class="row text-center">
        <div v-for="task in tasks" :key="task.title" class="col-md-4 mb-4">
          <div class="card-custom p-3">
            <img :src="task.image" :alt="task.title" class="img-fluid mb-3">
            <h3 class="h5">{{ task.title }}</h3>
            <ul class="list-unstyled">
              <li v-for="step in task.steps" :key="step">{{ step }}</li>
            </ul>
            <button class="btn btn-success">{{ task.buttonText }}</button>
          </div>
        </div>
      </div>
    </main>
  </template>
  
  <script>
  export default {
    name: 'OrdersPage',
    data() {
      return {
        tasks: [
          {
            title: 'Предложить задание фрилансерам',
            image: 'path/to/image1.png',
            steps: ['Опишите задачу', 'Фрилансеры откликнутся', 'Обсудите детали'],
            buttonText: 'Опубликовать заказ',
          },
          {
            title: 'Найти фрилансера на длительную работу',
            image: 'path/to/image2.png',
            steps: ['Составьте требования', 'Кандидаты оставят резюме', 'Выберите подходящего'],
            buttonText: 'Разместить вакансию',
          },
          {
            title: 'Получить готовые варианты и выбрать лучший',
            image: 'path/to/image3.png',
            steps: ['Опишите условия', 'Укажите вознаграждение', 'Выберите победителя'],
            buttonText: 'Провести конкурс',
          },
        ],
      };
    },
  };
  </script>
  