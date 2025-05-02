<template>
    <div>
      <h3 class="text-lg mb-2">Test Connection</h3>
      <button @click="test" class="btn-blue">Проверить GitLab</button>
      <p v-if="status" class="mt-2">{{ status }}</p>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  import svc from '@/services/docService'
  const status = ref('')
  async function test(){
    try{ await svc.testConnection(); status.value = 'OK — связь установлена' }
    catch{ status.value = 'Ошибка подключения' }
  }
  </script>
  