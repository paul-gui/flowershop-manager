<script setup lang="ts">
import { onMounted, ref } from 'vue'
import router from '@/router'
import type { UpdateProductRequest } from '@/types/dtos/products/productRequests.dto.ts'
import { getProductById, updateProduct } from '@/services/ProductsService.ts'
import { toast } from 'vue-sonner'
import { useRoute } from 'vue-router'
import type {
  CreateProductForm,
  PriceForProductCreation,
} from '@/types/models/createProductForm.ts'

const route = useRoute()

const form = ref<CreateProductForm>({ name: '', warehouseId: '', prices: [] })
const validationErrors = ref<Record<string, string>>({})
const isLoading = ref<boolean>(false)

onMounted(async () => {
  await hydrate()
})

async function onSubmit() {
  validationErrors.value = {}

  const updateProductRequest: UpdateProductRequest = {
    id: String(route.params.id),
    name: form.value.name,
    warehouseId: form.value.warehouseId,
    prices: form.value.prices.map((p) => ({ destinationId: p.destinationId, value: p.value })),
  }

  if (updateProductRequest.name === ''){
    validationErrors.value['name'] = 'Introduceti numele produsului'
  }
  if (updateProductRequest.prices.some(p => p.value <= 0 || !p.value)){
    validationErrors.value['prices'] = 'Pretul produsului trebuie sa fie mai mare decat 0'
  }

  if (Object.values(validationErrors.value).length > 0) {
    return
  }

  try {
    await updateProduct(updateProductRequest)
    toast.success('Produs modificat cu succes!')
    goBack()
  } catch (error) {
    toast.error('A aparut o eroare')
  }
}

async function hydrate() {
  try {
    isLoading.value = true
    const p = await getProductById(String(route.params.id))
    form.value.name = p.name
    form.value.warehouseId = p.warehouseId!
    form.value.prices = p.prices.sort((a: PriceForProductCreation, b: PriceForProductCreation) =>
      b.destinationName.localeCompare(a.destinationName),
    )
  }
  catch (error) {
    toast.error('A aparut o eroare la incarcarea produsului')
  }
  finally {
    isLoading.value = false
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
      <h1 class="text-h1 text-text_primary">Modificare produs</h1>
      <div v-if="isLoading" class="flex justify-center items-center py-10 gap-2">
        <div class="h-10 w-10 animate-spin rounded-full border-4 border-gray-300 border-t-gray-600"></div>
      </div>
      <div v-else>
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
          class="bg-accent2 hover:bg-accent2_hover text-text_accents py-3 px-10 rounded-xl"
        >
          Salveaza
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
