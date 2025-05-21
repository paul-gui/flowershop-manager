<template>
  <div class="flex flex-col items-center justify-center min-h-screen gap-10">
    <h1 class="text-h1 text-text_primary">Locatii</h1>
    <div class="flex w-[80%] flex-col items-center space-y-2">
      <div v-for="(label, index) in buttonLabels" :key="index" class="flex rounded-lg overflow-hidden border border-divider bg-cards text-text_secondary w-full max-w-md">
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
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
const buttonLabels = [
    'Solar mare curte',
    'Solar mic curte',
    'Solar 1 afara',
    'Solar 2 afara',
]
</script>