<template>
    <div class="grid grid-cols-4 gap-6">
      <div v-for="stat in stats" :key="stat.label" class="bg-[#1a1f2a] p-6 rounded-lg shadow">
        <h3 class="text-sm text-gray-400">{{ stat.label }}</h3>
        <p class="mt-2 text-2xl font-bold">{{ stat.value }}</p>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue'
  import svc from '@/services/docService'
  
  const stats = ref([])
  
  async function loadStats() {
    try {
      const res = await svc.stats()
      // ожидаем { totalFiles, totalVersions, totalBytes, activeCommits }
      stats.value = [
        { label: 'Всего документов', value: res.data.totalFiles },
        { label: 'Всего версий',     value: res.data.totalVersions },
        { label: 'Объём (байт)',     value: res.data.totalBytes },
        { label: 'Коммитов за 7дн',  value: res.data.recentCommits }
      ]
    } catch {
      stats.value = []
    }
  }
  
  onMounted(loadStats)
  </script>
  