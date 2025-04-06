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
        <button class="btn" type="submit">Добавить задачу</button>
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
    border-radius: 5px;
    background-color: #2D3445;
  }
  .create-task-form h3 {
    margin: 0 0 10px;
    font-size: 1.5rem;
    color: #fff;

  }
  .create-task-form form {
    display: flex;
    gap: 10px;
  }
  .create-task-form input {
    flex: 1;
    padding: 5px;
    border: 1px solid #0D0F1A;
    border-radius: 5px;
  }
  .btn {
  background-color: #00b5c5;
  color: white;
  border-radius: 3px;
  cursor: pointer;

}
.btn:hover {
  background-color:  #037485;
  color: white;
}

  </style>
  