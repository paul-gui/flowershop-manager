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
  pricesDescription: {
    type: Array,
    default: () => [],
  },
  icon: {
    type: String, // Can be a component or string name
    default: null,
  },
  showSecondButton: {
    type: Boolean,
    default: true,
  }
});

  defineEmits(['primary-click', 'secondary-click']);
</script>

<template>
  <div class="flex rounded-lg overflow-hidden border border-divider bg-cards text-text_secondary w-full p-1">
    <!-- Left Button -->
    <div
        class="flex-1 flex flex-col justify-center h-16 px-4 text-left hover:bg-[#3A3A55] rounded-lg transition-colors cursor-pointer"
        @click="$emit('primary-click')"
    >
      <span class="block text-h2 text-text_primary">{{ title }}</span>
      <span class="inline-block text-body" v-if="description">{{description}}</span>
      <div v-if="pricesDescription.length !== 0">
        <span class="inline-block text-sm ms-2" v-if="pricesDescription">{{ pricesDescription[0] }}</span>
        <span> • </span>
        <span class="inline-block text-sm" v-if="pricesDescription[1]">{{ pricesDescription[1] }}</span>
      </div>
    </div>

    <!-- Divider -->
    <div
      class="w-px bg-divider h-auto mx-1"
      v-if="showSecondButton"
    ></div>

    <!-- Right Button -->
    <button
        class="p-6 aspect-square flex items-center justify-center hover:bg-divider rounded-lg transition-colors"
        @click="$emit('secondary-click')"
        v-if="showSecondButton"
    >
      <i :class="icon" ></i>
    </button>
  </div>
</template>

<style scoped>

</style>