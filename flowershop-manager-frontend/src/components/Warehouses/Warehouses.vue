<template>
  <div class="flex flex-col items-center justify-center min-h-screen gap-10">
    <h1 class="text-h1 text-text_primary">Locatii</h1>
    <div class="flex w-[80%] flex-col items-center space-y-2">
      <div v-for="(labelText, index) in buttonLabels" :key="index" class="flex rounded-lg overflow-hidden border border-divider bg-cards text-text_secondary w-full max-w-md">
        <warehouse-button :label="labelText"></warehouse-button>
      </div>
    </div>
    <button class="bg-accent2 py-3 px-8 rounded-lg" v-on:click="goToAddWarehouse">Adauga</button>
  </div>
</template>
<script setup lang="ts">
import WarehouseButton from "@/components/Warehouses/warehouse_button.vue";
import { getWarehouses } from "@/services/WarehousesService";
import { onMounted, ref } from "vue";
import router from "@/router";
import type { Warehouse } from "@/components/Warehouses/Models/Warehouse";

const buttonLabels = ref([''])

onMounted(async () => {
  await getWarehousesAsync();
})

async function getWarehousesAsync() {
  const res:Warehouse[] = await getWarehouses()
  buttonLabels.value = res.map(i => i.name)
  console.log(buttonLabels.value)
}

function goToAddWarehouse(): void {
  router.push("/warehouse-details")
}

</script>