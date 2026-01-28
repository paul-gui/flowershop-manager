<template>
  <div class="h-screen bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-2">
      <h1 class="text-h1 text-text_primary mb-4">Detalii locatie</h1>
      <!-- Name Field + Delete location button-->
      <div class="flex items-end gap-2 relative">
        <div class="flex-1">
          <label class="block text-sm mb-1">Denumire locatie</label>
          <input
            v-model="name"
            type="text"
            class="w-full bg-cards text-xl text-gray-50 px-4 py-2 rounded-xl focus:outline-none"
          />
        </div>

        <button
          @click="showConfirmDialog = !showConfirmDialog"
          class="h-11 aspect-square rounded-lg bg-cards hover:text-red-500 hover:bg-divider">
          <i class=" fa fa-trash"></i>
        </button>
        <div
          v-if="showConfirmDialog"
          class="absolute sm:max-w-lg max-w-xs right-0 ml-2 top-20 bg-cards shadow-lg border border-gray-600 rounded-xl p-2 flex items-center space-x-2 z-10 w-max"
        >
          <p class="overflow-auto">Doresti sa stergi locatia?</p>
          <button
            @click="deleteWarehouseButton"
            class="bg-red-600 text-white p-2 text-sm rounded-lg hover:bg-red-700"
          >
            Confirma
          </button>
          <button
            @click="showConfirmDialog = !showConfirmDialog"
            class="bg-gray-600 text-white p-2 text-sm rounded-lg hover:bg-gray-700"
          >
            Anuleaza
          </button>
        </div>
      </div>

      <!-- Flowers List -->
      <div>
        <label class="block text-sm mb-2">Produse</label>
        <div class="max-h-96 overflow-scroll">
          <div v-for="(f, index) in products" :key="f.id" class="mb-2">
            <content-button
              :title="f.name"
              :pricesDescription="f.prices.map((p) => `${p.destinationName}: ${p.value} Lei`)"
              icon="fa fa-x text-gray-300"
              @primary-click="
              () => {
                goToProductDetailsPage(f.id)
              }
            "
              @secondary-click="
              () => {
                removeProduct(index)
              }
            "
            ></content-button>
          </div>
        </div>

        <button
          v-on:click="goToCreateProductPage"
          class="mt-2 flex items-center gap-2 text-sm text-text_primary bg-cards px-4 py-2 rounded-xl hover:bg-[#3c3860]"
        >
          <i class="fa fa-plus" />
          Adauga
        </button>
      </div>
    </div>

    <!-- Actions -->
    <div class="fixed bottom-8 left-0 flex justify-center mt-6 gap-2 w-full">
      <button
        @click="goBack"
        class="bg-cards hover:bg-[#3c3860] text-gray-50 py-3 px-10 rounded-xl"
      >
        Anuleaza
      </button>
      <button
        @click="onSubmit"
        class="bg-accent2 hover:bg-green-600 text-text_accents py-3 px-10 rounded-xl"
      >
        Salveaza
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getWarehouse, updateWarehouse, deleteWarehouse } from '@/services/WarehousesService.ts'
import { deleteProduct } from '@/services/ProductsService.ts'
import type { WarehouseDetailsResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import ContentButton from '@/components/Warehouses/content-button.vue'
import router from '@/router'
import type { UpdateWarehouseRequest } from '@/types/dtos/warehouse/warehouseRequests.dto.ts'
import type { ProductResponse } from '@/types/dtos/products/productResponses.dto.ts'
import { toast } from 'vue-sonner'

const props = defineProps<{
  id: string
}>()

const name = ref('')
const products = ref<ProductResponse[]>([])
const warehouse = ref<WarehouseDetailsResponse>()
const showConfirmDialog = ref<boolean>(false)

onMounted(async () => {
  await getWarehouseDetails()
})

async function getWarehouseDetails() {
  warehouse.value = await getWarehouse(props.id)
  if (warehouse.value) {
    name.value = warehouse.value.name
    products.value = warehouse.value.products
  }
}
async function goToCreateProductPage() {
  await router.push({
    name: 'warehouseAddProduct',
    params: {
      id: props.id,
    },
  })
}

async function goToProductDetailsPage(id: string) {
  await router.push({
    name: 'productDetails',
    params: {
      id: id,
    },
  })
}

async function onSubmit() {
  if (warehouse.value) {
    const updateWarehouseRequest: UpdateWarehouseRequest = {
      id: props.id,
      name: name.value,
    }
    await updateWarehouse(updateWarehouseRequest)
    await goBack()
  }
}

async function goBack() {
  await router.replace({ name: 'Warehouses' })
}

async function deleteWarehouseButton() {
  if (warehouse.value) {
    try{
      await deleteWarehouse(warehouse.value.id)
      toast.success('Locatie stearsa cu succes')
      await goBack()
    }
    catch (error) {
      toast.error('A aparut o eroare')
    }
  }
}

async function removeProduct(index: number) {
  try{
    const id = products.value[index].id
    await deleteProduct(id)
    await getWarehouseDetails()
    toast.success('Produs sters cu succes')
  }
  catch (error) {
    toast.error('A aparut o eroare')
  }
}
</script>
