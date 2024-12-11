<template>
    <div>
      <div
        class="modal fade"
        id="editModal"
        tabindex="-1"
        aria-labelledby="editModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="editModalLabel">Редактировать</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <input
                :value="modelValue"
                @input="$emit('update:modelValue', $event.target.value)"
                :placeholder="placeholder"
                class="form-control"
              />
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                Отмена
              </button>
              <button type="button" class="btn btn-primary" @click="save">
                Сохранить
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { Modal } from 'bootstrap';
  
  export default {
    props: {
      modelValue: String,
      placeholder: String,
    },
    methods: {
      save() {
        this.$emit('save', this.modelValue);
        const modalElement = document.getElementById('editModal');
        if (modalElement) {
          const modal = Modal.getInstance(modalElement) || new Modal(modalElement);
          modal.hide();
        }
      },
    },
  };
  </script>
  