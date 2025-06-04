<template>
  <div class="project-dashboard">
    <h2 class="page-title">Мои проекты</h2>

    <div
      v-for="project in projects"
      :key="project.id"
      class="project-card"
    >
      <!-- название -->
      <h3 class="project-title">{{ project.name }}</h3>

      <!-- куратор -->
      <div class="row">
        <span class="label-title">Куратор:</span>
        <span class="person-chip">{{ project.mentor }}</span>
      </div>

      <!-- команда -->
      <div class="row team-row">
        <span class="label-title">Команда проекта:</span>
        <span
          v-for="member in project.team"
          :key="member"
          class="person-chip"
        >
          {{ member }}
        </span>
      </div>

      <!-- МОИ ЗАДАЧИ -->
      <p class="label-title mt16">Мои задачи:</p>
      <ul class="tasks-list">
        <li
          v-for="task in myTasks(project.tasks)"
          :key="task.id"
          class="task-item"
        >
          {{ task.title }}

          <template v-if="task.status === 'pending'">
            <button
              class="mini-btn outline-send"
              @click="sendToReview(project.id, task.id)"
            >
              Отправить&nbsp;на&nbsp;проверку
            </button>
          </template>

          <template v-else-if="task.status === 'review'">
            <span class="on-review">Задача&nbsp;на&nbsp;проверке</span>
          </template>
        </li>
      </ul>

      <!-- ВСЕ ЗАДАЧИ ПРОЕКТА -->
      <p class="label-title mt16">Все задачи проекта:</p>
      <ul class="tasks-list">
        <li
          v-for="task in activeTasks(project.tasks)"
          :key="task.id"
          class="task-item"
        >
          {{ task.title }} — {{ task.student }}
          <span
            v-if="task.status === 'review'"
            class="on-review ml8"
          >(на&nbsp;проверке)</span>
        </li>
      </ul>

      <!-- ВЫПОЛНЕННЫЕ ЗАДАЧИ -->
      <p class="label-title mt16">Выполненные задачи:</p>
      <ul class="tasks-done">
        <li
          v-if="!completedTasks(project.tasks).length"
          class="task-done no-tasks"
        >
          Пока нет выполненных задач
        </li>
        <li
          v-for="task in completedTasks(project.tasks)"
          :key="task.id"
          class="task-done"
        >
          {{ task.title }} — {{ task.student }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "OrderTrackingStudent",

  data() {
    return {
      currentStudent: "Петров П.П.",

      projects: [
        {
          id: 1,
          name: "Разработка CRM-системы",
          mentor: "Иванов И.И.",
          team: ["Петров П.П.", "Сидорова А.А.", "Ким М.М."],
          tasks: [
            { id: 101, title: "Проектирование БД",  student: "Петров П.П.",   status: "pending" },
            { id: 103, title: "Front-end прототип",  student: "Сидорова А.А.", status: "pending" },
            { id: 105, title: "Тесты модулей",       student: "Ким М.М.",      status: "pending" }
          ]
        }
      ]
    };
  },

  methods: {
    /* фильтры */
    myTasks(tasks)        { return tasks.filter(t => t.student === this.currentStudent && t.status !== "done"); },
    activeTasks(tasks)    { return tasks.filter(t => t.status !== "done"); },
    completedTasks(tasks) { return tasks.filter(t => t.status === "done"); },

    /* отправить на проверку */
    sendToReview(projectId, taskId) {
      const proj = this.projects.find(p => p.id === projectId);
      const task = proj?.tasks.find(t => t.id === taskId);
      if (!task || task.status !== "pending") return;

      task.status = "review";
    }
  }
};
</script>

<style scoped>
/* контейнеры */
.project-dashboard { max-width:1050px; margin:40px auto; padding:0 20px; }
.page-title        { margin-bottom:32px; font-size:2rem; text-align:center; color:#fff; }
.project-card      { background:#2d3445; border-radius:12px; box-shadow:0 6px 16px rgba(0,0,0,.5); color:#e0e0e0; padding:24px; margin-bottom:32px; }
.project-title     { font-size:1.6rem; font-weight:700; margin-bottom:10px; color:#fff; }

.row, .team-row { display:flex; flex-wrap:wrap; gap:8px; align-items:center; margin-bottom:14px; }
.label-title    { font-weight:600; color:#fff; }
.mt16           { margin-top:16px; }
.ml8            { margin-left:8px; }

/* чипы участников */
.person-chip { display:inline-flex; padding:4px 12px; border-radius:12px; background:#3a3f4a; white-space:nowrap; }

/* списки */
.tasks-list, .tasks-done { list-style:none; padding:0; margin:0 0 12px; }
.task-item { padding:4px 0; }

/* выполненные задачи */
.task-done {
  position:relative;
  padding:4px 0;
  color:#e0e0e0;                     /* белый/светлый текст */
  text-decoration:line-through;
  text-decoration-color:#9ca3af;     /* серая линия */
  text-decoration-thickness:1px;
}
.task-done.no-tasks { text-decoration:none; }

/* кнопка */
.mini-btn {
  padding:3px 12px;
  font-size:.8rem;
  background:transparent;
  border-radius:12px;
  cursor:pointer;
  margin-left:8px;
}
.outline-send {
  color:#00b5c5;
  border:1px solid #00b5c5;
}
.mini-btn:hover { background-color:#037485; color:#fff; }

/* подпись «на проверке» */
.on-review {
  margin-left:8px;
  font-size:.8rem;
  color:#34d399;
}
</style>
