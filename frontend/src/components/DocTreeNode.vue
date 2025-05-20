<template>
    <li>
      <div
        class="tree-node flex items-center justify-between px-2 py-1 rounded hover:bg-gray-700 group"
      >
        <div class="flex items-center space-x-1">
          <!-- стрелка сворачивания -->
          <span
            v-if="node.type==='tree'"
            @click.stop="toggle"
            class="w-4 h-4 text-gray-400 cursor-pointer select-none transition-transform"
            :class="{ 'rotate-90': expanded }"
          >
            ▶
          </span>
          <span v-else class="w-4 h-4"></span>
  
          <!-- иконка папки/файла -->
          <span v-if="node.type==='tree'" class="w-5 h-5 text-yellow-400">📁</span>
          <span v-else                 class="w-5 h-5 text-gray-400">📄</span>
  
          <!-- название -->
          <span
            @click="$emit('select', node)"
            class="ml-1 text-gray-200 hover:text-white cursor-pointer"
          >
            {{ node.name }}
          </span>
        </div>
  
        <!-- кнопки действий -->
        <div class="invisible group-hover:visible flex space-x-1">
          <button
            v-if="node.type==='tree'"
            @click.stop="$emit('action', { type:'newFolder', node })"
            title="Новая папка"
            class="text-green-400 hover:text-green-200"
          >➕</button>
          <button
            @click.stop="$emit('action', { type:'rename', node })"
            title="Переименовать"
            class="text-blue-400 hover:text-blue-200"
          >✏️</button>
          <button
            @click.stop="$emit('action', { type:'move', node })"
            title="Переместить"
            class="text-yellow-400 hover:text-yellow-200"
          >🔀</button>
        </div>
      </div>
  
      <!-- рекурсивные дети -->
      <ul
        v-if="expanded && node.children?.length"
        class="ml-6 mt-1 list-none"
      >
        <DocTreeNode
          v-for="child in node.children"
          :key="child.path"
          :node="child"
          @select="$emit('select', $event)"
          @action="$emit('action',  $event)"
        />
      </ul>
    </li>
  </template>
  
  <script setup>
  /* eslint-disable */
  const props = defineProps({
    node: { type: Object, required: true }
  })
  const emit = defineEmits(['select','action'])
  
  import { ref } from 'vue'
  const expanded = ref(false)
  
  function toggle() {
    expanded.value = !expanded.value
  }
  </script>
  
  <style scoped>
  .tree-node { font-size: .95rem; }
  ul { margin: 0; padding: 0; list-style: none; }
  </style>
  