<script setup lang="ts">
defineProps({
  title: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    default: '',
  },
  icon: {
    type: String, // Can be a component or string name
    default: null,
  },
});

  defineEmits(['primary-click', 'secondary-click']);
</script>

<template>
  <div class="flex rounded-lg overflow-hidden border border-divider bg-cards text-text_secondary w-full p-1">
    <!-- Left Button -->
    <button
        class="flex-1 px-4 text-left hover:bg-[#3A3A55] transition-colors"
        @click="$emit('primary-click')"
    >
      <span class="block text-h2 text-text_primary">{{ title }}</span>
      <span class="block text-body">{{description}}</span>
    </button>

    <!-- Divider (optional visual border) -->
    <div class="w-px bg-divider h-auto"></div>

    <!-- Right Button (Icon) -->
    <button
        class="p-6 flex items-center justify-center hover:bg-divider transition-colors"
        @click="$emit('secondary-click')"
    >
      <i :class="icon" ></i>
    </button>
  </div>
</template>

<style scoped>

</style>