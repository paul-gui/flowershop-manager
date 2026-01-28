<script setup lang="ts">
import { onMounted, ref } from 'vue'
import router from '@/router'
import type { CreateProductRequest } from '@/types/dtos/products/productRequests.dto.ts'
import { createProduct, getDestinations } from '@/services/ProductsService.ts'
import { toast } from 'vue-sonner'
import type { CreateProductForm } from '@/types/models/createProductForm.ts'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'
import { useRoute } from 'vue-router'

const route = useRoute()

const form = ref<CreateProductForm>({ name: '', warehouseId: '', prices: [] })
const destinations = ref<DestinationResponse[]>([])
const validationErrors = ref<Record<string, string>>({})

onMounted(async () => {
  await hydrate()
})

async function hydrate() {
  form.value.warehouseId = String(route.params.id)
  destinations.value = await getDestinations()
  form.value.prices = destinations.value.map((d) => ({
    destinationId: d.id,
    destinationName: d.name,
    value: 0,
  }))
}

async function onSubmit() {
  validationErrors.value = {}

  const createProductRequest: CreateProductRequest = {
    name: form.value.name,
    warehouseId: form.value.warehouseId,
    prices: form.value.prices.map((p) => ({ destinationId: p.destinationId, value: p.value })),
  }

  if (createProductRequest.name === ''){
    validationErrors.value['name'] = 'Introduceti numele produsului'
  }
  if (createProductRequest.prices.some(p => p.value <= 0 || !p.value)){
    validationErrors.value['prices'] = 'Pretul produsului trebuie sa fie mai mare decat 0'
  }

  if (Object.values(validationErrors.value).length > 0) {
    return
  }

  try {
    await createProduct(createProductRequest)
    toast.success('Produs creat cu succes!')
    goBack()
  } catch (error) {
    toast.error('A aparut o eroare')
  }
}

function onCancel() {
  goBack()
}

function goBack() {
  router.replace({ path: '/warehouse-details/' + form.value.warehouseId })
}
</script>

<template>
  <div class="h-full bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-6">
      <h1 class="text-h1 text-text_primary">Adaugare produs</h1>
      <div>
        <label class="block text-sm mb-1">Denumire produs</label>
        <input
          v-model="form.name"
          type="text"
          class="w-full bg-cards text-xl text-gray-50 px-4 py-2 rounded-xl focus:outline-none"
        />
        <p class="text-red-500 text-sm" v-if="validationErrors['name']">{{ validationErrors['name'] }}</p>
      </div>

      <div>
        <label class="block mb-2">Pret</label>
        <div class="space-y-3">
          <div
            v-for="p in form.prices"
            :key="p.destinationId"
            class="flex items-center justify-between"
          >
            <span class="text-h3 ms-3">{{ p.destinationName }}</span>
            <input
              v-model="p.value"
              type="number"
              class="w-2/3 px-4 py-2 rounded-xl bg-[#2E2E4D] text-white focus:outline-none"
            />
            <span>Lei</span>
          </div>
          <p class="text-red-500 text-sm" v-if="validationErrors['prices']">{{ validationErrors['prices'] }}</p>
        </div>
      </div>

      <div class="fixed bottom-8 left-0 flex justify-center mt-6 gap-2 w-full">
        <button
          @click="onCancel"
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
  </div>
</template>
