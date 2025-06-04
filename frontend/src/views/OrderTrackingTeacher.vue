<template>
  <div class="project-dashboard">
    <h2 class="page-title">Проекты в работе</h2>

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

      <!-- команда + добавление студента -->
      <div class="row team-row">
        <span class="label-title">Команда проекта:</span>

        <span
          v-for="member in project.team"
          :key="member"
          class="person-chip"
        >
          {{ member }}
        </span>

        <form
          class="add-student-form"
          @submit.prevent="addStudent(project.id)"
        >
          <input
            v-model.number="studentInputs[project.id]"
            type="number"
            min="1"
            placeholder="ID"
            class="input-student"
            required
          />
          <button class="btn">Добавить&nbsp;студента&nbsp;в&nbsp;команду</button>
        </form>
      </div>

      <!-- сообщения -->
      <p
        v-if="messages[project.id]"
        :class="messages[project.id].type"
      >
        {{ messages[project.id].text }}
      </p>

      <!-- активные задачи -->
      <p class="label-title mt16">Список задач:</p>
      <ul class="tasks-list">
        <li
          v-for="task in activeTasks(project.tasks)"
          :key="task.id"
          class="task-item"
        >
          {{ task.title }} — {{ task.student }}

          <template v-if="task.controls && task.status === 'pending'">
            <button
              class="mini-btn outline-done"
              @click="markDone(project.id, task.id)"
            >
              Выполнено
            </button>
            <button
              class="mini-btn outline-redo"
              @click="markRedo(project.id, task.id)"
            >
              Доработать
            </button>
          </template>
        </li>
      </ul>

      <!-- форма добавления задачи -->
      <form
        class="new-task-form"
        @submit.prevent="addTask(project.id)"
      >
        <input
          v-model="newTask.title"
          type="text"
          placeholder="Новая задача"
          class="input-task"
          required
        />
        <select
          v-model="newTask.student"
          class="select-student"
          required
        >
          <option disabled value="">Ответственный</option>
          <option
            v-for="member in project.team"
            :key="member"
            :value="member"
          >
            {{ member }}
          </option>
        </select>
        <button class="btn">Добавить задачу</button>
      </form>

      <!-- выполненные задачи -->
      <p
        v-if="completedTasks(project.tasks).length"
        class="label-title mt16"
      >
        Выполненные задачи:
      </p>
      <ul class="tasks-done">
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
  name: "OrderTrackingTeacher",

  data() {
    return {
      /* ID → ФИО студентов */
      studentsById: {
        1: "Петров П.П.",
        2: "Сидорова А.А.",
        3: "Ким М.М.",
        4: "Васильев Д.Д.",
        5: "Романов Г.Г."
      },

      /* проекты (по умолчанию по одной базовой задаче) */
      projects: [
        {
          id: 1,
          name: "Разработка CRM-системы",
          mentor: "Иванов И.И.",
          team: ["Петров П.П.", "Сидорова А.А."],
          tasks: [
            { id: 101, title: "Проектирование БД", student: "Петров П.П.", status: "pending", controls: true }
          ]
        },
        {
          id: 2,
          name: "Мобильное приложение доставки",
          mentor: "Соколова Е.В.",
          team: ["Васильев Д.Д.", "Романов Г.Г."],
          tasks: [
            { id: 201, title: "Экран ресторанов", student: "Васильев Д.Д.", status: "pending", controls: true }
          ]
        }
      ],

      newTask: { title: "", student: "" },
      studentInputs: {},   // id студента, вводимые в форму
      messages: {},        // { [projectId]: { type, text } }
      messageTimers: {}    // { [projectId]: timeoutId }
    };
  },

  methods: {
    /* --- служебные фильтры --- */
    activeTasks(tasks)    { return tasks.filter(t => t.status !== "done"); },
    completedTasks(tasks) { return tasks.filter(t => t.status === "done"); },

    /* --- универсальный показ сообщения с авто-скрытием --- */
    showMessage(projectId, type, text) {
      // если для этого проекта уже запущен таймер, чистим его
      if (this.messageTimers[projectId]) {
        clearTimeout(this.messageTimers[projectId]);
      }

      this.messages[projectId] = { type, text };

      // сохраняем id таймера, чтобы можно было отменить
      this.messageTimers[projectId] = setTimeout(() => {
        delete this.messages[projectId];
        delete this.messageTimers[projectId];
      }, 10_000); // 10 секунд
    },

    /* --- поиск задачи --- */
    findTask(projectId, taskId) {
      return this.projects
        .find(p => p.id === projectId)
        ?.tasks.find(t => t.id === taskId);
    },

    /* --- кнопки оценки задач --- */
    markDone(projectId, taskId) {
      const task = this.findTask(projectId, taskId);
      if (task) { task.status = "done"; task.controls = false; }
    },
    markRedo(projectId, taskId) {
      const task = this.findTask(projectId, taskId);
      if (task) task.controls = false; // остаётся pending, но без кнопок
    },

    /* --- добавить студента в команду --- */
    addStudent(projectId) {
      const id   = this.studentInputs[projectId];
      const name = this.studentsById[id];
      const proj = this.projects.find(p => p.id === projectId);
      if (!proj) return;

      if (!name) {
        this.showMessage(projectId, "error", `ID ${id} не найден`);
        return;
      }

      if (proj.team.includes(name)) {
        this.showMessage(projectId, "error", `${name} уже в команде`);
        return;
      }

      proj.team.push(name);
      this.showMessage(projectId, "success", `${name} добавлен`);
      this.studentInputs[projectId] = "";
    },

    /* --- добавить задачу --- */
    addTask(projectId) {
      const { title, student } = this.newTask;
      if (!title || !student) return;

      const proj = this.projects.find(p => p.id === projectId);
      proj?.tasks.unshift({
        id: Date.now(),
        title,
        student,
        status: "pending",
        controls: false // задачи, добавленные учителем, без кнопок проверки
      });

      this.newTask.title = "";
      this.newTask.student = "";
    }
  }
};
</script>

<style scoped>
/* --- базовые контейнеры --- */
.project-dashboard { max-width:1050px; margin:40px auto; padding:0 20px; }
.page-title        { margin-bottom:32px; font-size:2rem; text-align:center; color:#fff; }
.project-card      { background:#2d3445; border-radius:12px; box-shadow:0 6px 16px rgba(0,0,0,.5); color:#e0e0e0; padding:24px; margin-bottom:32px; }
.project-title     { font-size:1.6rem; font-weight:700; margin-bottom:10px; color:#fff; }

/* строки и подписи */
.row, .team-row { display:flex; flex-wrap:wrap; gap:8px; align-items:center; margin-bottom:14px; }
.label-title     { font-weight:600; color:#fff; }
.mt16            { margin-top:16px; }

/* чипы */
.person-chip { display:inline-flex; padding:4px 12px; border-radius:12px; background:#3a3f4a; white-space:nowrap; }

/* формы */
.add-student-form { display:flex; gap:8px; }
.input-student    { width:70px; padding:6px 8px; background:#1e2330; border:1px solid #3a3f4a; border-radius:12px; color:#fff; }

.new-task-form    { display:flex; flex-wrap:wrap; gap:10px; margin:10px 0 18px; }
.input-task       { flex:1 1 260px; padding:8px 12px; background:#1e2330; border:1px solid #3a3f4a; border-radius:12px; color:#fff; }
.select-student   { width:180px; padding:8px 12px; background:#1e2330; border:1px solid #3a3f4a; border-radius:12px; color:#fff; }

/* основные кнопки */
.btn {
  padding:8px 18px;
  background:#00b5c5;
  border:none;
  border-radius:12px;
  color:#fff;
  cursor:pointer;
  white-space:nowrap;
}
.btn:hover { background:#037485; }

/* мини-кнопки */
.mini-btn {
  padding:3px 12px;
  font-size:.8rem;
  background:transparent;
  border-radius:12px;
  cursor:pointer;
  margin-left:8px;
}
.outline-done { color:#22c55e; border:1px solid #22c55e; }
.outline-redo { color:#00b5c5; border:1px solid #00b5c5; }
.mini-btn:hover { background:#037485; color:#fff; }

/* списки */
.tasks-list, .tasks-done { list-style:none; padding:0; margin:0 0 12px; }
.task-item  { padding:4px 0; }

/* выполненные задачи */
.task-done {
  position:relative;
  padding:4px 0;
  color:#e0e0e0;
}
.task-done::after {
  content:"";
  position:absolute;
  left:0;
  right:0;
  top:50%;
  height:1px;
  background:#9ca3af;
  transform:translateY(-50%);
}

/* сообщения */
.error   { color:#ff7272; }
.success { color:#34d399; }
</style>
