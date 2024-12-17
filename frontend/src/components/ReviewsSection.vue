<template>
    <div class="mt-3">
      <h5>Отзывы</h5>
      <div class="reviews-container">
        <div
          v-for="(review, index) in reviews"
          :key="index"
          class="card mb-3 shadow-sm"
        >
          <div class="card-body">
            <h6 class="card-title">Отзыв от заказчика</h6>
            <p class="card-text">{{ review.customerFeedback }}</p>
            <div class="stars">
              <span v-for="star in 5" :key="star" class="star">
                <i
                  class="fa"
                  :class="star <= review.customerRating ? 'fa-star' : 'fa-star-o'"
                ></i>
              </span>
            </div>
            <hr />
            <h6 class="card-title">Отзыв от преподавателя</h6>
            <p class="card-text">{{ review.teacherFeedback }}</p>
            <div class="stars">
              <span v-for="star in 5" :key="star + 't'" class="star">
                <i
                  class="fa"
                  :class="star <= review.teacherRating ? 'fa-star' : 'fa-star-o'"
                ></i>
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      reviews: {
        type: Array,
        required: true,
      },
    },
  };
  </script>
  
  <style>
  .reviews-container {
    max-height: 300px; /* Ограничиваем высоту контейнера */
    overflow-y: auto; /* Включаем прокрутку по вертикали */
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    background-color: #f9f9f9;
    margin-bottom: 10px;
  }
  
  .card {
    border: none;
  }
  
  .stars {
    color: #ffc107; /* Золотой цвет для звёзд */
  }
  
  .star {
    margin-right: 5px;
  }
  </style>
  