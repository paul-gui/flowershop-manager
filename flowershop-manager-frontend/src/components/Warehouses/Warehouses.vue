<template>
  <div class="h-full bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-4 flex flex-col">
      <div class="flex flex-row justify-between items-baseline">
        <h1 class="text-h1 text-text_primary">Locatii</h1>
        <div class="space-x-2">
          <button
            class="h-12 aspect-square rounded-lg bg-cards hover:bg-divider"
            @click="editingMode = !editingMode"
          >
            <i
              :class="['fa text-white text-2xl',
              editingMode
                ? 'fa-x'
                : 'fa-pen'
              ]"></i>
          </button>
          <button
            class="h-12 aspect-square rounded-lg bg-cards hover:bg-divider"
            @click="goToHistory"
          >
            <i class="fa fa-clock text-white text-2xl"></i>
          </button>
        </div>
      </div>
      <div class="space-y-2 max-h-72 overflow-y-scroll">
        <div v-for="(warehouse, index) in warehouses" :key="index">
          <content-button
            :title="warehouse.name"
            icon="fa fa-pen"
            @primary-click="() => goToCreateSale(warehouse.id)"
            @secondary-click="() => goToEditWarehouse(warehouse.id)"
            :showSecondButton="editingMode"
          ></content-button>
        </div>
      </div>
      <div class="flex items-center justify-center">
        <button
          class="border-gray-300 hover:bg-[#1a1a28] border border-dashed text-gray-300 py-3 px-8 rounded-lg"
          @click="goToAddWarehouse"
          v-if="editingMode"
        >
          <i class="fa fa-plus"></i>
          Adauga
        </button>
      </div>

    </div>
  </div>
</template>
<script setup lang="ts">
import {getWarehouses} from "@/services/WarehousesService.ts";
import {onMounted, ref} from "vue";
import router from "@/router";
import type { WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import ContentButton from "@/components/Warehouses/content-button.vue";


const props = defineProps({
  saleDate: {
    type: String,
    default: null
  }
})

const editingMode = ref<boolean>(false);
const warehouses = ref<WarehouseResponse[]>([])

onMounted(async () => {
  await getWarehousesAsync();
})

async function getWarehousesAsync() {
  warehouses.value = await getWarehouses()
}

function goToAddWarehouse(): void {
  router.push("/warehouse-add")
}

function goToEditWarehouse(id: string): void {
  router.push("/warehouse-details/" + id);
}

function goToCreateSale(warehouseId: string): void {
  router.push({
    name: "CreateSale",
    params: { warehouseId: warehouseId }
  })
}

function goToHistory(){
  router.push("/sales/history")
}
</script>