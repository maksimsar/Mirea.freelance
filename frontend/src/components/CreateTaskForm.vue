<template>
    <div class="create-task-form animate__animated animate__fadeInUp">
      <h3>Создать задачу</h3>
      <form @submit.prevent="handleSubmit">
        <input
          type="text"
          v-model="taskTitle"
          placeholder="Название задачи"
          required
        />
        <button type="submit">Добавить задачу</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    name: "CreateTaskForm",
    props: {
      orderId: { type: Number, required: true },
    },
    data() {
      return {
        taskTitle: "",
      };
    },
    methods: {
      handleSubmit() {
        if (this.taskTitle.trim() === "") return;
        // Эмитируем событие для добавления задачи
        this.$emit("add-task", { orderId: this.orderId, title: this.taskTitle });
        this.taskTitle = "";
      },
    },
  };
  </script>
  
  <style scoped>
  .create-task-form {
    margin-top: 10px;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
    background-color: #fdfdfd;
  }
  .create-task-form h3 {
    margin: 0 0 10px;
    font-size: 1rem;
    color: #007bff;
  }
  .create-task-form form {
    display: flex;
    gap: 10px;
  }
  .create-task-form input {
    flex: 1;
    padding: 5px;
    border: 1px solid #ccc;
    border-radius: 3px;
  }
  .create-task-form button {
    padding: 5px 10px;
    border: none;
    background-color: #007bff;
    color: #fff;
    border-radius: 3px;
    cursor: pointer;
  }
  .create-task-form button:hover {
    background-color: #0056b3;
  }
  </style>
  