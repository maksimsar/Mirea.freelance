<template>
<<<<<<< HEAD
    <div class="auth-header">
      <button
        :class="{ active: isLogin }"
        @click="$emit('switchMode', true)"
      >
        Войти
      </button>
      <button
        :class="{ active: !isLogin }"
        @click="$emit('switchMode', false)"
      >
        Зарегистрироваться
      </button>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      isLogin: {
        type: Boolean,
        required: true,
      },
    },
  };
  </script>
  
  <style scoped>
  .auth-header {
    display: flex;
    justify-content: space-around;
    margin-bottom: 20px;
  }
  
  button {
    padding: 10px 20px;
    font-size: 16px;
    border: 1px solid #007bff;
    background-color: transparent;
    color: #007bff;
    border-radius: 5px;
    cursor: pointer;
    transition: all 0.3s ease;
  }
  
  button.active {
    background-color: #007bff;
    color: white;
  }
  
  button:hover {
    background-color: #0056b3;
    color: white;
  }
  </style>
  
=======
  <div class="auth-header">
    <button
      :class="{ active: isLogin }"
      @click="$emit('switchMode', true)"
    >
      Войти
    </button>
    <button
      :class="{ active: !isLogin }"
      @click="$emit('switchMode', false)"
    >
      Зарегистрироваться
    </button>
  </div>
</template>

<script>
export default {
  props: {
    isLogin: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style scoped>
.auth-header {
  display: flex;
  justify-content: space-around;
  margin-bottom: 20px;
}

button {
  padding: 10px 20px;
  font-size: 16px;
  border: 1px solid #007bff;
  background-color: transparent;
  color: #007bff;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.3s ease;
}

button.active {
  background-color: #007bff;
  color: white;
}

button:hover {
  background-color: #0056b3;
  color: white;
}
</style>
>>>>>>> stenzel
