<template>
    <div class="min-h-screen bg-[#0c0e17] text-gray-100">
      <div class="container mx-auto py-8 space-y-8">
        <!-- Заголовок -->
        <h1 class="text-3xl font-bold">Admin: Документация</h1>
  
        <div class="grid grid-cols-4 gap-6">
          <!-- Левый столбец: глобальная загрузка + дерево -->
          <div class="col-span-1 space-y-4">
            <div class="bg-[#1a1f2a] p-4 rounded-lg shadow space-y-4">
              <DocUploader
                v-if="!selected || selected.type === 'tree'"
                :path="selected?.type === 'tree' ? selected.path : ''"
                @uploaded="refresh"
              />
              <DocTree :nodes="nodes" @select="onSelect" />
            </div>
          </div>
  
          <!-- Правый столбец: статистика, тест подключения, права, и действия над файлом -->
          <div class="col-span-3 space-y-6">
            <!-- 1. Статистика и Test Connection -->
            <div class="bg-[#1a1f2a] p-6 rounded-lg shadow space-y-4">
              <h2 class="text-xl font-semibold">Статистика</h2>
              <ul class="space-y-2">
                <li>Всего документов: <strong>{{ stats.totalFiles }}</strong></li>
                <li>Всего версий: <strong>{{ stats.totalVersions }}</strong></li>
                <li>Объём (байт): <strong>{{ stats.totalBytes }}</strong></li>
                <li>Коммитов за 7дн: <strong>{{ stats.recentCommits }}</strong></li>
              </ul>
  
              <h2 class="text-xl font-semibold mt-4">Test Connection</h2>
              <div class="flex items-center space-x-4">
                <button @click="testConn" class="btn-blue">
                  {{ connLoading ? 'Проверка...' : 'Проверить GitLab' }}
                </button>
                <span v-if="connStatus" class="ml-2">{{ connStatus }}</span>
              </div>
            </div>
  
            <!-- 2. Управление правами -->
            <div class="bg-[#1a1f2a] p-6 rounded-lg shadow">
              <RoleManagement />
            </div>
  
            <!-- 3. Действия над выбранным файлом -->
            <div v-if="selected?.type === 'blob'" class="bg-[#1a1f2a] p-6 rounded-lg shadow space-y-4">
              <h2 class="text-xl font-semibold">Действия над документом</h2>
              <DocUploader :path="selected.path" @uploaded="refresh" />
              <div class="flex space-x-4">
                <button @click="onDownload" class="btn-green">Скачать</button>
                <button @click="onDelete" class="btn-red">Удалить</button>
                <button @click="showHistory = true" class="btn-yellow">История</button>
              </div>
              <DocHistoryModal
                v-if="showHistory"
                :path="selected.path"
                @close="closeHistory"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, reactive, onMounted } from 'vue'
  import docService from '@/services/docService'
  import DocTree from '@/components/DocTree.vue'
  import DocUploader from '@/components/DocUploader.vue'
  import DocHistoryModal from '@/components/DocHistoryModal.vue'
  import RoleManagement from '@/components/RoleManagement.vue'
  
  // Данные
  const nodes = ref([])
  const selected = ref(null)
  const showHistory = ref(false)
  
  // Статистика и статус подключения
  const stats = reactive({
    totalFiles: 0,
    totalVersions: 0,
    totalBytes: 0,
    recentCommits: 0
  })
  const connStatus = ref('')
  const connLoading = ref(false)
  
  // Загрузка дерева и статистики
  async function load(folder = null) {
    // файловая структура
    const res = await docService.list(folder)
    nodes.value = res.data.map(n => ({
      ...n,
      type: n.type || (n.versionCount != null ? 'blob' : 'tree')
    }))
  
    // статистика
    try {
      const s = (await docService.stats()).data
      stats.totalFiles = s.totalFiles
      stats.totalVersions = s.totalVersions
      stats.totalBytes = s.totalBytes
      stats.recentCommits = s.recentCommits
    } catch {
      // игнор
    }
  }
  
  // Навигация по дереву
  function refresh() {
    const folder = selected.value?.type === 'tree'
      ? selected.value.path
      : selected.value?.path.replace(/\/[^/]+$/, '')
    load(folder || null)
  }
  function onSelect(node) {
    selected.value = node
    if (node.type === 'tree') load(node.path)
  }
  
  // Действия над файлом
  async function onDelete() {
    if (!selected.value) return
    if (confirm(`Удалить ${selected.value.path}?`)) {
      await docService.delete(selected.value.path)
      selected.value = null
      load()
    }
  }
  async function onDownload() {
    if (!selected.value) return
    const res = await docService.download(selected.value.path)
    const blob = new Blob([res.data])
    const a = document.createElement('a')
    a.href = URL.createObjectURL(blob)
    a.download = selected.value.fileName
    a.click()
  }
  
  // Test Connection
  async function testConn() {
    connLoading.value = true
    connStatus.value = ''
    try {
      await docService.testConnection()
      connStatus.value = 'OK — связь установлена'
    } catch {
      connStatus.value = 'Ошибка подключения'
    } finally {
      connLoading.value = false
    }
  }
  
  // История
  function closeHistory() {
    showHistory.value = false
    refresh()
  }
  
  // Запуск
  onMounted(() => load())
  </script>
  
  <style scoped>
  .btn-green {
    @apply px-4 py-2 bg-green-600 hover:bg-green-700 text-white rounded;
  }
  .btn-red {
    @apply px-4 py-2 bg-red-600 hover:bg-red-700 text-white rounded;
  }
  .btn-yellow {
    @apply px-4 py-2 bg-yellow-500 hover:bg-yellow-600 text-white rounded;
  }
  .btn-blue {
    @apply px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded;
  }
  </style>
  