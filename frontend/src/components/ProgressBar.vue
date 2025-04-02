<template>
    <div class="progress-bar animate__animated animate__fadeIn">
      <div class="progress" :style="{ width: progress + '%' }"></div>
      <span class="progress-text">{{ progress }}%</span>
    </div>
  </template>
  
  <script>
  export default {
    name: "ProgressBar",
    props: {
      progress: { type: Number, required: true },
    },
  };
  </script>
  
  <style scoped>
  .progress-bar {
    position: relative;
    height: 20px;
    background-color: #e9ecef;
    border-radius: 10px;
    overflow: hidden;
    margin: 10px 0;
  }
  .progress {
    height: 100%;
    background-color: #28a745;
    transition: width 0.3s ease;
  }
  .progress-text {
    position: absolute;
    top: 0;
    left: 50%;
    transform: translateX(-50%);
    font-size: 0.8rem;
    color: #fff;
  }
  </style>
  