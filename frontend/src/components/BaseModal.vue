<template>
  <teleport to="body">
    <transition name="modal">
      <div
        v-if="visible"
        class="fixed inset-0 z-[9999] flex items-center justify-center bg-black bg-opacity-70"
      >
        <div
          class="bg-[#1a1f2a] rounded-2xl shadow-2xl w-full max-w-lg mx-4 overflow-hidden transform transition-transform"
        >
          <header class="px-6 py-4 border-b border-gray-700">
            <h3 class="text-xl font-semibold text-gray-100">{{ title }}</h3>
          </header>
          <div class="p-6">
            <slot />
          </div>
        </div>
      </div>
    </transition>
  </teleport>
</template>

<script setup>
/* eslint-disable vue/multi-word-component-names, no-undef */
defineProps({
  visible: { type: Boolean, required: true },
  title:   { type: String,  default: '' }
})
</script>

<style>
.modal-enter-active, .modal-leave-active {
  transition: opacity 0.3s ease, transform 0.3s ease;
}
.modal-enter-from, .modal-leave-to {
  opacity: 0;
  transform: scale(0.9);
}
</style>
