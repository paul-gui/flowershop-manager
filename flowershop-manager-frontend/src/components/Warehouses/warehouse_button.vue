<script setup lang="ts">
import type { WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import router from "@/router";
const props = defineProps<{
  warehouse: WarehouseResponse;
}>()

function onEditClick(){
  router.push({ path: `/warehouse-details/${props.warehouse.id}`})
}
</script>

<template>
  <!-- Left Button -->
  <button
      class="flex-1 px-4 text-left hover:bg-divider transition-colors"
      onclick="location.href='/details-page'"
  >
    {{ warehouse.name }}
  </button>

  <!-- Divider (optional visual border) -->
  <div class="w-px bg-divider"></div>

  <!-- Right Button (Icon) -->
  <button
      class="p-6 flex items-center justify-center hover:bg-divider transition-colors"
      v-on:click="onEditClick"
  >
    <i class="fa fa-pen"></i>
  </button>
</template>

<style scoped>

</style>