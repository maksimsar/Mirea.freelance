<template>
    <div>
      <input v-model="q" placeholder="Поиск..." class="w-full p-2 mb-4 rounded border bg-[#0c0e17] text-gray-100" />
      <ul class="space-y-1 overflow-y-auto max-h-[400px]">
        <li v-for="n in filtered" :key="n.path" @click="$emit('select', n)"
            class="flex justify-between items-center p-2 hover:bg-[#272c3a] cursor-pointer rounded">
          <div class="flex items-center space-x-2">
            <span v-if="n.type==='tree'">📁</span><span v-else>📄</span>
            <span>{{ n.fileName || n.name }}</span>
          </div>
          <span v-if="n.type==='tree'">▶︎</span>
        </li>
      </ul>
    </div>
  </template>
  
  <script setup>
  import { ref, computed, defineProps, watch } from 'vue'
  const props = defineProps({ nodes: Array })
  const q = ref('')
  const filtered = computed(() => {
    return props.nodes.filter(n => (n.fileName||n.name).toLowerCase().includes(q.value.toLowerCase()))
  })
  watch(() => props.nodes, () => q.value = '')
  </script>
  