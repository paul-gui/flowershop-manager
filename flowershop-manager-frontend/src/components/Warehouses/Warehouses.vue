<template>
  <div class="flex flex-col items-center justify-center h-screen gap-10">
    <h1 class="text-h1 text-text_primary">Locatii</h1>
    <div class="w-[80%] space-y-2 max-h-[60%] overflow-y-scroll">
      <div v-for="(warehouse, index) in warehouses" :key="index">
        <content-button
          :title="warehouse.name"
          icon="fa fa-pen"
          @primary-click="() => goToCreateSale(warehouse.id)"
          @secondary-click="() => goToEditWarehouse(warehouse.id)"
        ></content-button>
      </div>
    </div>
    <button class="bg-accent2 py-3 px-8 rounded-lg" v-on:click="goToAddWarehouse">Adauga</button>
  </div>
</template>
<script setup lang="ts">
import {getWarehouses} from "@/services/WarehousesService.ts";
import {onMounted, ref} from "vue";
import router from "@/router";
import type { WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import ContentButton from "@/components/Warehouses/content-button.vue";

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
    name: "CreateSaleFromWarehouse",
    params: { warehouseId: warehouseId }
  })
}
</script>