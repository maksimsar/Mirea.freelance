<template>
  <div class="space-y-2">
    <!-- поиск -->
    <input
      v-model="searchTerm"
      type="text"
      placeholder="Поиск..."
      class="w-full px-3 py-2 rounded bg-gray-800 text-gray-100 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-500"
    />

    <!-- корневое дерево -->
    <ul class="tree-root">
      <DocTreeNode
        v-for="node in filteredTree"
        :key="node.path"
        :node="node"
        @select="$emit('select',  $event)"
        @action="$emit('action',   $event)"
      />
    </ul>
  </div>
</template>

<script setup>
/* eslint-disable */
import { ref, computed } from 'vue'
import DocTreeNode from './DocTreeNode.vue'

const props = defineProps({
  nodes: { type: Array, required: true }
})
const emit = defineEmits(['select','action'])
const searchTerm = ref('')

function buildTree(list) {
  const map = {}
  list.forEach(n => {
    const name = n.name || n.fileName || n.path.split('/').pop()
    map[n.path] = { ...n, name, children: [] }
  })
  const roots = []
  Object.values(map).forEach(node => {
    const idx = node.path.lastIndexOf('/')
    if (idx === -1) roots.push(node)
    else {
      const p = map[node.path.slice(0, idx)]
      if (p) p.children.push(node)
      else roots.push(node)
    }
  })
  // сортировка: папки вверх, по алфавиту
  function sortArr(arr) {
    arr.sort((a,b) => {
      if (a.type !== b.type) return a.type==='tree' ? -1 : 1
      return a.name.localeCompare(b.name)
    })
    arr.forEach(x => sortArr(x.children))
  }
  sortArr(roots)
  return roots
}

function filterNodes(nodes, term) {
  const out = []
  nodes.forEach(n => {
    const match = n.name.toLowerCase().includes(term)
    const kids  = filterNodes(n.children, term)
    if (match || kids.length) out.push({ ...n, children:kids })
  })
  return out
}

const tree         = computed(() => buildTree(props.nodes))
const filteredTree = computed(() => {
  const t = searchTerm.value.trim().toLowerCase()
  return t ? filterNodes(tree.value, t) : tree.value
})
</script>

<style scoped>
.tree-root {
  margin: 0;
  padding: 0;
  list-style: none;
}
</style>
