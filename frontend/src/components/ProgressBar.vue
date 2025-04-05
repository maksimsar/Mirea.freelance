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
    background-color:  #0D0F1A;
    border-radius: 10px;
    overflow: hidden;
    margin: 10px 0;
  }
  .progress {
    height: 100%;
    background-color: #037485;
  }
  .progress-text {
    position: absolute;
    top: 0;
    left: 50%;
  
    font-size: 0.8rem;
    color: #ffffff;
    text-shadow: #181B29 1px 1px 2px;
  }
  </style>
  