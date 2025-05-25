<template>
  <div class="flex flex-col items-center justify-center min-h-screen gap-10">
    <h1 class="text-h1 text-text_primary">Locatii</h1>
    <div class="flex w-[80%] flex-col items-center space-y-2">
      <div v-for="(labelText, index) in buttonLabels" :key="index" class="flex rounded-lg overflow-hidden border border-divider bg-cards text-text_secondary w-full max-w-md">
        <warehouse-button :label="labelText"></warehouse-button>
      </div>
    </div>
    <button class="bg-accent2 py-3 px-8 rounded-lg" onclick="location.href='warehouse-details'">Adauga</button>
  </div>
</template>
<script setup lang="ts">
import WarehouseButton from "@/components/Warehouses/warehouse_button.vue";

const buttonLabels = [
    'Solar mare curte',
    'Solar mic curte',
    'Solar 1 afara',
    'Solar 2 afara',
]
</script>