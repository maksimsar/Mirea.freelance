<template>
  <div class="modal-overlay">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Отклик на задание</h5>
          <button type="button" class="close" @click="$emit('close')">&times;</button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="submitForm">
            <!-- ФИО -->
            <div class="form-group">
              <label for="fullName">ФИО</label>
              <input
                type="text"
                id="fullName"
                class="form-control"
                :value="profile.fullName"
                disabled
              />
            </div>
            
            <!-- Группа -->
            <div class="form-group">
              <label for="group">Группа</label>
              <input
                type="text"
                id="group"
                class="form-control"
                :value="profile.group"
                disabled
              />
            </div>
            
            <!-- Уровень -->
            <div class="form-group">
              <label for="level">Уровень</label>
              <input
                type="text"
                id="level"
                class="form-control"
                :value="profile.level"
                disabled
              />
            </div>
            
            <!-- Роль (выбирается через радиокнопки) -->
            <div class="form-group">
              <label>Выберите предпочитаемую роль</label>
              <div>
                <div class="form-check">
                  <input
                    type="radio"
                    id="Backend-developer"
                    class="form-check-input"
                    value="Backend-developer"
                    v-model="selectedCategory"
                  />
                  <label class="form-check-label" for="Backend-developer">Backend-developer</label>
                </div>
                <div class="form-check">
                  <input
                    type="radio"
                    id="Frontend-developer"
                    class="form-check-input"
                    value="Frontend-developer"
                    v-model="selectedCategory"
                  />
                  <label class="form-check-label" for="Frontend-developer">Frontend-developer</label>
                </div>
                <div class="form-check">
                  <input
                    type="radio"
                    id="ML-engineer"
                    class="form-check-input"
                    value="ML-engineer"
                    v-model="selectedCategory"
                  />
                  <label class="form-check-label" for="ML-engineer">ML-engineer</label>
                </div>
                <div class="form-check">
                  <input
                    type="radio"
                    id="Data-Scientist"
                    class="form-check-input"
                    value="Data Scientist"
                    v-model="selectedCategory"
                  />
                  <label class="form-check-label" for="Data-Scientist">Data Scientist</label>
                </div>
                <div class="form-check">
                  <input
                    type="radio"
                    id="Business-Analyst"
                    class="form-check-input"
                    value="Business Analyst"
                    v-model="selectedCategory"
                  />
                  <label class="form-check-label" for="Business-Analyst">Business Analyst</label>
                </div>
              </div>
            </div>
            
            <!-- Кнопка отправки -->
            <button type="submit" class="btn btn-primary" style="background-color: #007bff;">Отправить заявку</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ResponseModal',
  props: {
    order: Object,
  },
  data() {
    return {
      profile: {
        fullName: 'Иван Иванов',
        group: 'МИРЭА-2024',
        level: 'Средний',
      },
      selectedCategory: null,
    };
  },
  methods: {
    submitForm() {
      const formData = {
        fullName: this.profile.fullName,
        group: this.profile.group,
        level: this.profile.level,
        selectedCategory: this.selectedCategory,
        order: this.order,
      };
      this.$emit('submit', formData);
    },
  },
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-dialog {
  background: white;
  padding: 1.5rem;
  border-radius: 8px;
  max-width: 500px;
  width: 100%;
}

.form-check-input:checked {
  background-color: #007bff;
  border-color: #007bff;
}
</style>
