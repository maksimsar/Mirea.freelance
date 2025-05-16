<template>
    <div>
      <div
        class="modal fade"
        id="createFileModal"
        tabindex="-1"
        aria-labelledby="createFileModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content bg-[#2D3445] border-0">
            <div class="modal-header">
              <h5 class="modal-title text-white" id="createFileModalLabel">Новый файл</h5>
              <button
                type="button"
                class="btn-close btn-close-white"
                data-bs-dismiss="modal"
                aria-label="Закрыть"
              ></button>
            </div>
            <div class="modal-body space-y-3">
              <input
                v-model="fileName"
                class="form-control bg-[#0c0e17] text-gray-100 border-gray-600"
                placeholder="Имя файла (с расширением)"
                :disabled="loading"
              />
              <select
                v-model="type"
                class="form-select bg-[#0c0e17] text-gray-100 border-gray-600"
              >
                <option value="txt">TXT</option>
                <option value="pdf">PDF</option>
                <option value="docx">Word (.docx)</option>
                <option value="xlsx">Excel (.xlsx)</option>
                <option value="link">Ссылка</option>
              </select>
              <textarea
                v-if="type==='link'"
                v-model="linkUrl"
                class="form-control bg-[#0c0e17] text-gray-100 border-gray-600"
                placeholder="URL"
                :disabled="loading"
              ></textarea>
              <div v-if="error" class="text-sm text-red-500">{{ error }}</div>
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
                :disabled="!fileName.trim() || (type==='link' && !linkUrl.trim()) || loading"
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
  
  const fileName = ref('')
  const type     = ref('txt')
  const linkUrl  = ref('')
  const loading  = ref(false)
  const error    = ref('')
  let bsModal = null
  
  watch(() => props.visible, (show) => {
    const el = document.getElementById('createFileModal')
    if (!el) return
  
    if (!bsModal) {
      bsModal = Modal.getOrCreateInstance(el)
      el.addEventListener('hidden.bs.modal', () => emit('update:visible', false))
    }
  
    if (show) {
      fileName.value = ''
      type.value     = 'txt'
      linkUrl.value  = ''
      error.value    = ''
      loading.value  = false
      bsModal.show()
    } else {
      bsModal.hide()
    }
  })
  
  onBeforeUnmount(() => {
    if (bsModal) {
      bsModal.dispose()
      bsModal = null
    }
  })
  
  async function create() {
    if (!fileName.value.trim()) {
      error.value = 'Имя файла не может быть пустым'
      return
    }
    if (type.value === 'link' && !linkUrl.value.trim()) {
      error.value = 'URL не может быть пустым'
      return
    }
    loading.value = true
    error.value   = ''
  
    const path = props.currentPath
      ? `${props.currentPath}/${fileName.value}`
      : fileName.value
  
    let blob
    if (type.value === 'link') {
      blob = new Blob([`[${fileName.value}](${linkUrl.value})`], { type: 'text/markdown' })
    } else {
      blob = new Blob([], { type: 'application/octet-stream' })
    }
  
    try {
      await docService.upload(path, blob)
      emit('created')
      bsModal.hide()
    } catch {
      error.value = 'Не удалось создать файл'
    } finally {
      loading.value = false
    }
  }
  </script>
  
  <style scoped>
  .modal-dialog {
    max-width: 600px;
  }
  </style>
  