<template>
    <div>
      <h3 class="text-lg mb-2">Управление правами</h3>
      <div v-for="u in users" :key="u.id" class="flex items-center justify-between mb-2">
        <span>{{ u.name }}</span>
        <select v-model="u.role" @change="save(u)" class="p-1 rounded bg-[#0c0e17] text-gray-100">
          <option value="viewer">Viewer</option>
          <option value="editor">Editor</option>
          <option value="admin">Admin</option>
        </select>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue'
  const users = ref([])
  async function load(){
    // TODO: fetch from /api/gitdocs/access
    users.value = [{id:1,name:'alice',role:'editor'},{id:2,name:'bob',role:'viewer'}]
  }
  async function save(u){
    // TODO: patch to /api/gitdocs/access
    console.log('save',u)
  }
  onMounted(load)
  </script>
  