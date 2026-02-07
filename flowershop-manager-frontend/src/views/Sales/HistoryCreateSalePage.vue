<template>
  <div class="h-full p-4 sm:p-6">
    <div class="mx-auto max-w-7xl space-y-4 sm:space-y-6">
      <h1 class="text-xl font-semibold text-text_primary sm:text-2xl">Adaugă vânzare în istoric</h1>
      <div class="bg-white rounded-xl shadow-sm px-4 py-6">
        <div v-if="isLoading" class="flex justify-center items-center py-10 gap-2">
          <div class="h-10 w-10 animate-spin rounded-full border-4 border-gray-300 border-t-gray-600"></div>
        </div>
        <div v-else class="max-w-sm mx-auto space-y-4">
          <div>
            <label for="saleDate" class="block">Data vânzării</label>
            <input
              type="date"
              name="saleDate"
              class="bg-gray-100 rounded-xl p-2 w-full"
              v-model="saleDate"
            />
          </div>
          <div>
            <label for="warehouse" class="block">Depozit</label>
            <select
              name="warehouse"
              class="p-2 rounded-xl w-full bg-gray-100"
              v-model="selectedWarehouseId"
              @change="getProductsForWarehouse"
            >
              <option value="">Selectează depozit</option>
              <option
                v-for="warehouse of warehouses"
                :key="warehouse.id ?? 'all'"
                :value="warehouse.id"
              >
                {{ warehouse.name }}
              </option>
            </select>
            <span
              class="text-red-600 text-sm"
              v-if="errors['warehouse']"
            >
              {{ errors['warehouse'] }}
            </span>
          </div>
          <div>
            <label for="product" class="block">Produs</label>
            <select
              name="product"
              class="p-2 rounded-xl w-full bg-gray-100"
              v-model="selectedProductId"
              @change="getPriceForProduct"
            >
              <option value="">Selectează produs</option>
              <option v-for="product in products" :key="product.id" :value="product.id">
                {{ product.name }}
              </option>
            </select>
            <span
              class="text-red-600 text-sm"
              v-if="errors['product']"
            >
              {{ errors['product'] }}
            </span>
          </div>
          <div>
            <label for="destination" class="block">Destinație</label>
            <select
              name="destination"
              class="p-2 rounded-xl w-full bg-gray-100"
              v-model="selectedDestinationId"
              @change="getPriceForProduct"
            >
              <option value="">Selectează destinație</option>
              <option
                v-for="destination in destinations"
                :key="destination.id"
                :value="destination.id"
              >
                {{ destination.name }}
              </option>
            </select>
            <span
              class="text-red-600 text-sm"
              v-if="errors['destination']"
            >
              {{ errors['destination'] }}
            </span>
          </div>
          <div>
            <label for="quatity" class="block">Cantitate</label>
            <input type="number" class="p-2 rounded-xl w-full bg-gray-100" v-model="quantity" />
            <span
              class="text-red-600 text-sm"
              v-if="errors['quantity']"
            >
              {{ errors['quantity'] }}
            </span>
          </div>
          <div>
            <label for="price" class="block">Preț</label>
            <input type="number" class="p-2 rounded-xl w-full bg-gray-100" v-model="priceAtSale" />
            <span
              class="text-red-600 text-sm"
              v-if="errors['price']"
            >
              {{ errors['price'] }}
            </span>
          </div>
          <div class="flex items-center justify-center gap-2">
            <button
              class="rounded-xl bg-gray-200 hover:bg-gray-300 p-3"
              @click="router.replace({ name: 'SalesHistory' })"
            >
              Anulează
            </button>
            <button
              class="rounded-xl bg-accent2 hover:bg-[#62c678] text-gray-50 p-3"
              @click="submitForm"
            >
              Salvează
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import router from '@/router'
import type { CreateSaleForm } from '@/types/models/createSaleForm.ts'
import type { WarehouseResponse } from '@/types/dtos/warehouse/warehouseResponses.dto.ts'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'
import type { ProductResponse } from '@/types/dtos/products/productResponses.dto.ts'
import { getDestinations, getPrice, getProductsByWarehouseId } from '@/services/ProductsService.ts'
import { getWarehouses } from '@/services/WarehousesService.ts'
import { createSale } from '@/services/SalesService.ts'
import { toast } from 'vue-sonner'

const route = useRoute()
const errors = ref<Record<string, string>>({})
const isLoading = ref<boolean>(false)

// Form state
const selectedWarehouseId = ref<string>('')
const selectedDestinationId = ref<string>('')
const selectedProductId = ref<string>('')
const quantity = ref<number>(0)
const priceAtSale = ref<number>(0)
const saleDate = ref<string>(String(route.params['saleDate']))

// Form options
const warehouses = ref<WarehouseResponse[]>([])
const destinations = ref<DestinationResponse[]>([])
const products = ref<ProductResponse[]>([])

onMounted(async () => {
  try {
    isLoading.value = true
    warehouses.value = await getWarehouses()
    destinations.value = await getDestinations()
  }
  catch (error) {
    toast.error('A apărut o eroare la încărcarea formularului')
  }
  finally {
    isLoading.value = false
  }
})

async function getProductsForWarehouse() {
  if (selectedWarehouseId.value) {
    products.value = await getProductsByWarehouseId(selectedWarehouseId.value)
  }
}

async function getPriceForProduct() {
  if (selectedDestinationId.value) {
    if (selectedProductId.value) {
      priceAtSale.value = await getPrice(selectedProductId.value, selectedDestinationId.value)
    }
  }
}

async function submitForm() {
  errors.value = {}

  if (!selectedWarehouseId.value) {
    errors.value['warehouse'] = 'Selectează un depozit'
  }
  if (!selectedProductId.value) {
    errors.value['product'] = 'Selectează un produs'
  }
  if (!selectedDestinationId.value) {
    errors.value['destination'] = 'Selectează o destinație'
  }
  if (!quantity.value || quantity.value <= 0) {
    errors.value['quantity'] = 'Cantitatea trebuie să fie mai mare decât 0'
  }
  if (!priceAtSale.value || priceAtSale.value <= 0) {
    errors.value['price'] = 'Prețul trebuie să fie mai mare decât 0'
  }

  if(Object.values(errors.value).length > 0) {
    return;
  }

  const createSaleForm: CreateSaleForm = {
    productId: selectedProductId.value,
    destinationId: selectedDestinationId.value,
    quantity: quantity.value,
    priceAtSale: priceAtSale.value,
    saleDate: saleDate.value
  }

  try{
    await createSale(createSaleForm)
    toast.success('Produs adăugat cu succes')
    await router.replace({ name: 'SalesHistory' })
  }
  catch(error) {
    toast.error('A apărut o eroare')
  }

}
</script>
