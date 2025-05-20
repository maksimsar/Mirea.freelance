<template>
    <div>
      <div
        class="modal fade"
        id="createFolderModal"
        tabindex="-1"
        aria-labelledby="createFolderModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content bg-[#2D3445] border-0">
            <div class="modal-header">
              <h5 class="modal-title text-white" id="createFolderModalLabel">Новая папка</h5>
              <button
                type="button"
                class="btn-close btn-close-white"
                data-bs-dismiss="modal"
                aria-label="Закрыть"
              ></button>
            </div>
            <div class="modal-body">
              <input
                v-model="name"
                class="form-control bg-[#0c0e17] text-gray-100 border-gray-600"
                placeholder="Имя папки"
                :disabled="loading"
              />
              <div v-if="error" class="mt-2 text-sm text-red-500">{{ error }}</div>
            </div>
            <div class="modal-footer">
              <button
                type="button"
                class="btn btn-secondary"
                data-bs-dismiss="modal"
                :disabled="loading"
              >
                Отмена
              </button>
              <button
                type="button"
                class="btn btn-primary"
                @click="create"
                :disabled="!name.trim() || loading"
              >
                {{ loading ? 'Создание...' : 'Создать' }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  /* eslint-disable no-undef */
  import { ref, watch, onBeforeUnmount } from 'vue'
  import { Modal } from 'bootstrap'
  import docService from '@/services/docService'
  
  const props = defineProps({
    visible:     Boolean,
    currentPath: { type: String, default: '' }
  })
  const emit = defineEmits(['update:visible','created'])
  
  const name    = ref('')
  const loading = ref(false)
  const error   = ref('')
  let bsModal = null
  
  // Когда props.visible меняется — показываем/скрываем Bootstrap-модал
  watch(() => props.visible, (show) => {
    const el = document.getElementById('createFolderModal')
    if (!el) return
  
    if (!bsModal) {
      bsModal = Modal.getOrCreateInstance(el)
      // при скрытии через UI (кнопка X или backdrop) сообщаем родителю
      el.addEventListener('hidden.bs.modal', () => emit('update:visible', false))
    }
  
    if (show) {
      name.value = ''
      error.value = ''
      loading.value = false
      bsModal.show()
    } else {
      bsModal.hide()
    }
  })
  
  // Убираем слушатели при размонтировании
  onBeforeUnmount(() => {
    if (bsModal) {
      bsModal.dispose()
      bsModal = null
    }
  })
  
  async function create() {
    if (!name.value.trim()) {
      error.value = 'Имя папки не может быть пустым'
      return
    }
    loading.value = true
    error.value   = ''
    const fullPath = props.currentPath
      ? `${props.currentPath}/${name.value}`
      : name.value
  
    try {
      await docService.createFolder(fullPath)
      emit('created')
      bsModal.hide()
    } catch {
      error.value = 'Не удалось создать папку'
    } finally {
      loading.value = false
    }
  }
  </script>
  
  <style scoped>
  .modal-dialog {
    max-width: 500px;
  }
  </style>
  