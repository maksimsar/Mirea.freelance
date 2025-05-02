<template>
    <div>
      <label class="block mb-2 text-gray-200">
        Загрузить файл в <code>{{ displayPath }}</code>
      </label>
      <input type="file" @change="onFile" class="mb-2" />
      <div class="flex items-center space-x-4">
        <button
          v-if="file"
          @click="upload"
          class="btn-indigo"
          :disabled="loading"
        >
          {{ loading ? 'Загрузка...' : 'Загрузить' }}
        </button>
        <p v-if="error" class="text-red-400">{{ error }}</p>
      </div>
    </div>
  </template>
  
  <script setup>
  /* eslint-disable no-undef */
  import { ref, computed } from 'vue'
  import svc from '@/services/docService'
  
  // defineProps и defineEmits — это макросы Vue 3, их не нужно импортировать
  const props = defineProps({
    // если props.path — папка, иначе пустая строка для корня
    path: { type: String, default: '' }
  })
  const emit = defineEmits(['uploaded'])
  
  const file = ref(null)
  const loading = ref(false)
  const error = ref('')
  
  // Отображаемый путь в UI
  const displayPath = computed(() => props.path || '/ (корень)')
  
  function onFile(e) {
    error.value = ''
    file.value = e.target.files[0]
  }
  
  async function upload() {
    if (!file.value) return
    loading.value = true
    error.value = ''
    // составляем полный путь: папка/имя_файла или просто имя_файла
    const targetPath = props.path
      ? `${props.path.replace(/\/$/, '')}/${file.value.name}`
      : file.value.name
    try {
      await svc.upload(targetPath, file.value)
      emit('uploaded')
      file.value = null
    } catch (err) {
      error.value =
        err.response?.data ||
        `Ошибка загрузки: ${err.response?.status || err.message}`
    } finally {
      loading.value = false
    }
  }
  </script>
  
  <style scoped>
  .btn-indigo {
    @apply px-4 py-2 bg-indigo-600 hover:bg-indigo-700 rounded text-white;
  }
  </style>
  