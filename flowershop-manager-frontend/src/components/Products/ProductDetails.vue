<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { addProduct, getDestinations } from '@/services/ProductsService.ts'
import router from '@/router'
import { useRoute } from 'vue-router'
import type {
  CreateProductRequest,
  ProductPriceForDestinationWithName,
} from '@/types/dtos/products/product.dto.ts'
import type { Destination } from '@/types/models/destination.ts'

const route = useRoute()
const selectedWarehouseId = route.params.id.toString()
const pricesInput = ref<ProductPriceForDestinationWithName[]>([])

const createProductForm = ref<CreateProductRequest>({
  name: '',
  warehouseId: '',
  prices: [],
})

onMounted(async () => {
  getDestinations().then((response: Destination[]) => {
    for (const d of response) {
      if (!(d.id in pricesInput.value))
        pricesInput.value.push({
          destinationId: d.id,
          destinationName: d.name,
          value: 0,
        })
    }
  })
})

function cancel() {
  router.back()
}

async function save() {
  createProductForm.value.warehouseId = selectedWarehouseId
  createProductForm.value.prices = pricesInput.value.map(p => ({
    destinationId: p.destinationId,
    value: p.value,
  }))
  const response = await addProduct(createProductForm.value)
  console.log(response)
}
</script>

<template>
  <div class="min-h-screen bg-[#1A1A2E] text-white flex items-center justify-center p-6">
    <div class="w-full max-w-sm space-y-6">
      <div>
        <label class="block text-sm mb-1">Denumire</label>
        <input
          v-model="createProductForm.name"
          type="text"
          class="w-full bg-cards text-xl text-text_secondary px-4 py-2 rounded-xl focus:outline-none"
        />
      </div>

      <div>
        <label class="block text-gray-300 mb-2">Pret</label>
        <div class="space-y-3">
          <div
            v-for="(p, idx) in pricesInput"
            :key="p.destinationId"
            class="flex items-center justify-between"
          >
            <span class="text-h3 text-gray-300">{{ p.destinationName }}</span>
            <input
              v-model="p.value"
              type="number"
              class="w-2/3 px-4 py-2 rounded-xl bg-[#2E2E4D] text-white focus:outline-none"
            />
          </div>
        </div>
      </div>

      <div class="fixed bottom-8 left-0 flex justify-center mt-6 space-x-4 w-full">
        <button
          @click="cancel"
          class="bg-error hover:bg-red-600 text-text_accents py-3 px-10 rounded-xl"
        >
          Anuleaza
        </button>
        <button
          @click="save"
          class="bg-accent2 hover:bg-green-600 text-text_accents py-3 px-10 rounded-xl"
        >
          Salveaza
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped></style>