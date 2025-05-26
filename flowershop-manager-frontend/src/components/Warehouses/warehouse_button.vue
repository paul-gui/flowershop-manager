<script setup lang="ts">
defineProps({
  label: String
})
</script>

<template>
  <!-- Left Button -->
  <button
      class="flex-1 px-4 text-left hover:bg-[#3A3A55] transition-colors"
      onclick="location.href='/details-page'"
  >
    {{ label }}
  </button>

  <!-- Divider (optional visual border) -->
  <div class="w-px bg-divider"></div>

  <!-- Right Button (Icon) -->
  <button
      class="p-6 flex items-center justify-center hover:bg-divider transition-colors"
      onclick="location.href='/edit-page'"
  >
    <i class="fa fa-pen"></i>
  </button>
</template>

<style scoped>

</style>