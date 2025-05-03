<template>
    <div class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div class="bg-[#1a1f2a] rounded-lg p-6 w-2/3 max-h-[80vh] overflow-auto relative">
        <button @click="$emit('close')" class="absolute top-4 right-4 text-gray-400">✖</button>
        <h2 class="text-xl mb-4">История: {{ props.path }}</h2>
        <ul class="space-y-3">
          <li v-for="c in commits" :key="c.commitId" class="p-4 bg-[#0c0e17] rounded">
            <div class="flex justify-between mb-1">
              <span class="font-mono">{{ c.commitId.slice(0,7) }}</span>
              <button @click="revert(c.commitId)" class="btn-yellow text-sm">Откат</button>
            </div>
            <p>{{ c.title }}</p>
            <p class="text-xs text-gray-400">{{ new Date(c.date).toLocaleString() }}</p>
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted, watch, defineProps, defineEmits } from 'vue'
  import svc from '@/services/docService'
  
  const props = defineProps({ path: String })
  defineEmits(['close'])
  
  const commits = ref([])
  
  async function load() {
    try {
      const res = await svc.history(props.path)
      commits.value = res.data
    } catch (error) {
      console.error(error)
    }
  }
  
  async function revert(id) {
    try {
      const blob = await svc.download(props.path, id)
      const file = new File([blob.data], props.path.split('/').pop())
      await svc.upload(props.path, file)
      load()
    } catch (error) {
      console.error(error)
    }
  }
  
  onMounted(load)
  watch(() => props.path, load)
  </script>
  