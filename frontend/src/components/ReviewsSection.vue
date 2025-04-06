<template>
    <div class="mt-3">
      <h5 class="text-center">Отзывы</h5>
      <div class="reviews-container">
        <div
          v-for="(review, index) in reviews"
          :key="index"
          class="card mb-3"
        >
          <div class="card-body">
            <h6 class="card-title">От заказчика: </h6>
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
            <h6 class="card-title">От преподавателя: </h6>
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
    max-height: 300px; 
    overflow-y: auto; 
    padding: 10px;
    border-radius: 5px;
    background-color:  #0D0F1A;
   }

  .card-body {
    background-color: #2D3445;
    border-radius: 5px;
  }
  h5 {
    color: white !important;
    font-family: "BezierSans-Regular";
    text-shadow: #a1014f 1px 1px 1px;

  }
  .card-title {
    color: white !important;
    font-family: "BezierSans-Regular";
  }
 .card-text {
  color: #E0E0E0;
}
  
  .stars {
    color: #ffc107;
  }
  .star {
    margin-right: 5px;
  }

  </style>
  