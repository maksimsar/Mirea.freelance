<template>
  <div class="min-h-screen bg-[#0c0e17] text-gray-100">
    <div class="container mx-auto py-8 space-y-8">
      <h1 class="text-3xl font-bold">Admin: Документация</h1>

      <div class="flex justify-end space-x-4">
        <button @click="newFolderVisible = true" class="btn-blue">Новая папка</button>
        <button @click="newFileVisible   = true" class="btn-green">Новый файл</button>
      </div>

      <div class="grid grid-cols-4 gap-6">
        <!-- Левая панель -->
        <aside class="col-span-1 space-y-4">
          <div class="bg-[#1a1f2a] p-4 rounded-lg shadow space-y-4">
            <DocUploader
              v-if="!selected || selected.type==='tree'"
              :path="selected?.type==='tree'?selected.path:''"
              @uploaded="refresh"
            />
            <DocTree
              :nodes="nodes"
              @select="onSelect"
              @action="onTreeAction"
            />
          </div>
        </aside>

        <!-- Правая панель -->
        <section class="col-span-3 space-y-6">
          <!-- Статистика -->
          <div class="bg-[#1a1f2a] p-6 rounded-lg shadow">
            <h2 class="text-xl font-semibold">Статистика</h2>
            <ul class="mt-2 space-y-2">
              <li>Всего документов: <strong>{{ stats.totalFiles }}</strong></li>
              <li>Всего версий: <strong>{{ stats.totalVersions }}</strong></li>
              <li>Объём (байт): <strong>{{ stats.totalBytes }}</strong></li>
              <li>Коммитов за 7дн: <strong>{{ stats.recentCommits }}</strong></li>
            </ul>
            <div class="mt-4 flex items-center space-x-4">
              <button @click="testConn" class="btn-blue" :disabled="connLoading">
                {{ connLoading ? 'Проверка...' : 'Проверить GitLab' }}
              </button>
              <span v-if="connStatus">{{ connStatus }}</span>
            </div>
          </div>

          <!-- Управление правами -->
          <div class="bg-[#1a1f2a] p-6 rounded-lg shadow">
            <RoleManagement />
          </div>

          <!-- Действия над файлом -->
          <div
            v-if="selected?.type==='blob'"
            class="bg-[#1a1f2a] p-6 rounded-lg shadow space-y-4"
          >
            <h2 class="text-xl font-semibold">Действия над документом</h2>
            <DocUploader :path="selected.path" @uploaded="refresh" />
            <div class="mt-4 flex space-x-4">
              <button @click="onDownload" class="btn-green">Скачать</button>
              <button @click="onDelete"   class="btn-red">Удалить</button>
              <button @click="showHistory = true" class="btn-yellow">История</button>
            </div>
            <DocHistoryModal
              v-if="showHistory"
              :path="selected.path"
              @close="closeHistory"
            />
          </div>
        </section>
      </div>
    </div>

    <!-- Модальные компоненты всегда в теле документа, центрируются BaseModal'ом -->
    <CreateFolderModal
      :visible="newFolderVisible"
      :currentPath="selected?.type==='tree'?selected.path:''"
      @update:visible="newFolderVisible = $event"
      @created="onFolderCreated"
    />
    <CreateFileModal
      :visible="newFileVisible"
      :currentPath="selected?.type==='tree'?selected.path:''"
      @update:visible="newFileVisible = $event"
      @created="onFileCreated"
    />
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import docService       from '@/services/docService'
import DocTree          from '@/components/DocTree.vue'
import DocUploader      from '@/components/DocUploader.vue'
import DocHistoryModal  from '@/components/DocHistoryModal.vue'
import RoleManagement   from '@/components/RoleManagement.vue'
import CreateFolderModal from '@/components/CreateFolderModal.vue'
import CreateFileModal   from '@/components/CreateFileModal.vue'

const nodes = ref([])
const selected = ref(null)
const showHistory = ref(false)

const stats = reactive({
  totalFiles: 0,
  totalVersions: 0,
  totalBytes: 0,
  recentCommits: 0
})
const connStatus  = ref('')
const connLoading = ref(false)

const newFolderVisible = ref(false)
const newFileVisible   = ref(false)

async function load(folder = null) {
  const r = await docService.list(folder)
   nodes.value = r.data.map(n=>({
   ...n,
  type: n.type    // теперь приходит из бэка "tree" или "blob"
 }));

  const s = (await docService.stats(folder)).data
  Object.assign(stats, s)
}

function refresh() {
  const f = selected.value?.type === 'tree'
    ? selected.value.path
    : selected.value?.path.replace(/\/[^/]+$/, '')
  load(f || null)
}

function onSelect(node) {
  selected.value = node
  if (node.type === 'tree') load(node.path)
}

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
  const r = await docService.download(selected.value.path)
  const blob = new Blob([r.data])
  const a = document.createElement('a')
  a.href = URL.createObjectURL(blob)
  a.download = selected.value.fileName
  a.click()
}

async function testConn() {
  connLoading.value = true; connStatus.value = ''
  try {
    await docService.testConnection()
    connStatus.value = 'OK — связь установлена'
  } catch {
    connStatus.value = 'Ошибка подключения'
  } finally {
    connLoading.value = false
  }
}

function closeHistory() {
  showHistory.value = false
  refresh()
}

async function onTreeAction({ type, node }) {
  if (type === 'newFolder')        newFolderVisible.value = true
  else if (type === 'rename') {
    const name = prompt('Новое имя:', node.name)
    if (!name) return
    const folder = node.path.replace(/\/[^/]+$/, '')
    await docService.rename(node.path, folder ? `${folder}/${name}` : name)
    load(folder || null)
  }
  else if (type === 'move') {
    const dest = prompt('Путь папки назначения:', '')
    if (!dest) return
    await docService.move(node.path, `${dest}/${node.name}`)
    load()
  }
}

function onFolderCreated() {
  newFolderVisible.value = false
  load(selected.value?.path || null)
}
function onFileCreated() {
  newFileVisible.value = false
  load(selected.value?.path || null)
}

onMounted(() => load())
</script>

<style scoped>
.btn-green  { @apply px-4 py-2 bg-green-600 hover:bg-green-700 text-white rounded }
.btn-red    { @apply px-4 py-2 bg-red-600   hover:bg-red-700   text-white rounded }
.btn-yellow { @apply px-4 py-2 bg-yellow-500 hover:bg-yellow-600 text-white rounded }
.btn-blue   { @apply px-4 py-2 bg-blue-600  hover:bg-blue-700  text-white rounded }
</style>
