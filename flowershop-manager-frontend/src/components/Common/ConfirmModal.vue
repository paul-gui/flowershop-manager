<!-- components/ConfirmModal.vue -->
<template>
  <div v-if="modelValue" class="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
    <div class="bg-cards rounded-lg p-6 shadow-xl max-w-sm w-full">
      <h2 class="text-text_primary text-lg font-semibold mb-4">{{ title }}</h2>
      <p class="text-text_primary mb-4">{{ message }}</p>
      <div class="flex justify-end space-x-2">
        <button
            @click="$emit('update:modelValue', false)"
            class="px-4 py-2 bg-divider text-text_primary rounded hover:bg-gray-700"
        >
          Anulare
        </button>
        <button
            @click="confirm"
            class="px-4 py-2 bg-red-500 text-text_primary rounded hover:bg-red-600"
        >
          Confirmare
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps({
  modelValue: Boolean,
  title: {
    type: String,
    default: "Are you sure?"
  },
  message: {
    type: String,
    default: "This action cannot be undone."
  }
})

const emit = defineEmits(['update:modelValue', 'confirm'])

function confirm() {
  emit('confirm')
  emit('update:modelValue', false)
}
</script>
