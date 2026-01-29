<template>
  <div class="h-full bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-4 flex flex-col">
      <div class="flex flex-row justify-between items-baseline">
        <h1 class="text-h1 text-text_primary">Locatii</h1>
        <div
          class="space-x-2"
          v-if="isAdminLoggedIn"
        >
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
        <div v-if="isLoading" class="flex justify-center items-center py-10 gap-2">
          <div class="h-10 w-10 animate-spin rounded-full border-4 border-gray-300 border-t-gray-600"></div>
        </div>
        <div v-else v-for="(warehouse, index) in warehouses" :key="index">
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
import { computed, onMounted, ref} from "vue";
import router from "@/router";
import type { WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import ContentButton from "@/components/Warehouses/content-button.vue";
import { useAuthStore } from '@/stores/auth.ts'
import { toast } from 'vue-sonner'

const editingMode = ref<boolean>(false);
const warehouses = ref<WarehouseResponse[]>([])
const isLoading = ref(false)
const auth = useAuthStore()

const isAdminLoggedIn = computed(() => {
  return auth.hasRole('Admin')
})

onMounted(async () => {
  await getWarehousesAsync();
})

async function getWarehousesAsync() {
  try{
    isLoading.value = true
    warehouses.value = await getWarehouses()
  }
  catch (error) {
    toast.error('A aparut o eroare la incarcarea locatiilor')
  }
  finally {
    isLoading.value = false
  }
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