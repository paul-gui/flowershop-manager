<template>
  <div class="h-full p-4 sm:p-6">
    <div class="mx-auto max-w-7xl space-y-4 sm:space-y-6">
      <h1 class="text-xl font-semibold text-text_primary sm:text-2xl">Editeaza vanzare</h1>
      <div class="bg-white rounded-xl shadow-sm px-4 py-6">
        <div
          class="flex items-center justify-center w-full"
          v-if="!sale"
        >
          <p>Nu exista vanzarea</p>
        </div>
        <div class="max-w-sm mx-auto space-y-4 relative" v-if="sale">
          <button class="absolute top-3 right-0 h-10 aspect-square bg-red-100 hover:bg-red-200 rounded-xl" @click="showConfirmButton = !showConfirmButton">
            <i class="fa fa-trash text-red-600 text-lg"></i>
          </button>

          <div class="flex flex-col items-center justify-center">
            <h1 class="font-semibold text-2xl">{{ sale.productName }}</h1>
            <p>{{ sale.warehouseName }}</p>
          </div>

          <div
            v-if="showConfirmButton"
            class="absolute right-0 ml-2 top-10 bg-white shadow-lg border rounded-xl p-2 flex items-center space-x-2 z-10 w-max"
          >
            <button
              @click="onDeleteSale()"
              class="px-2 py-1 bg-red-600 text-white text-sm rounded-lg hover:bg-red-700"
            >
              Confirma
            </button>
          </div>

          <div>
            <label for="saleDate" class="block">Data vanzarii</label>
            <input
              type="date"
              name="saleDate"
              class="bg-gray-100 rounded-xl p-2 w-full"
              v-model="saleDate"
            />
          </div>
          <div>
            <label for="destination" class="block">Destinatie</label>
            <select
              name="destination"
              class="p-2 rounded-xl w-full bg-gray-100"
              v-model="selectedDestinationId"
              @change="getPriceForProduct"
            >
              <option value="">Selecteaza destinatie</option>
              <option
                v-for="destination in destinations"
                :key="destination.id"
                :value="destination.id"
              >
                {{ destination.name }}
              </option>
            </select>
            <span class="text-red-600 text-sm" v-if="errors['destination']">
              {{ errors['destination'] }}
            </span>
          </div>
          <div>
            <label for="quatity" class="block">Cantitate</label>
            <input type="number" class="p-2 rounded-xl w-full bg-gray-100" v-model="quantity" />
            <span class="text-red-600 text-sm" v-if="errors['quantity']">
              {{ errors['quantity'] }}
            </span>
          </div>
          <div>
            <label for="price" class="block">Pret</label>
            <input type="number" class="p-2 rounded-xl w-full bg-gray-100" v-model="priceAtSale" />
            <span class="text-red-600 text-sm" v-if="errors['price']">
              {{ errors['price'] }}
            </span>
          </div>
          <div class="flex items-center justify-center gap-2">
            <button
              class="rounded-xl bg-gray-200 hover:bg-gray-300 p-3"
              @click="router.replace({ name: 'SalesHistory' })"
            >
              Anuleaza
            </button>
            <button class="rounded-xl bg-green-600 hover:bg-green-700 text-gray-50 p-3" @click="submitForm">Salveaza</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getDestinations, getPrice } from '@/services/ProductsService.ts'
import type { DestinationResponse } from '@/types/dtos/destinations/destinationResponses.dto.ts'
import type { SaleResponseForEdit } from '@/types/dtos/sales/saleResponses.ts'
import { deleteSale, getSaleForEdit, updateSale } from '@/services/SalesService.ts'
import { useRoute } from 'vue-router'
import router from '@/router'
import type { SaleEditRequest } from '@/types/dtos/sales/saleRequests.ts'
import { toast } from 'vue-sonner'

const route = useRoute()

const destinations = ref<DestinationResponse[]>([])
const sale = ref<SaleResponseForEdit>()


const selectedDestinationId = ref<string>('')
const quantity = ref<number>(0)
const priceAtSale = ref<number>(0)
const saleDate = ref<string>('')

const showConfirmButton = ref<boolean>()

const errors = ref<Record<string, string>>({})

onMounted(async () => {
  await hydrateFields()
})

function getDateFromUTCDate(utcDate: string) {
  return utcDate.split('T')[0]
}

async function getPriceForProduct() {
  if (selectedDestinationId.value && sale.value && sale.value.productId) {
    priceAtSale.value = await getPrice(sale.value.productId, selectedDestinationId.value)
  }
}

async function hydrateFields() {
  destinations.value = await getDestinations()

  const saleId = String(route.params.id)
  sale.value = await getSaleForEdit(saleId)
  if (sale.value) {
    saleDate.value = getDateFromUTCDate(sale.value.saleDate)
    selectedDestinationId.value = sale.value.destinationId
    quantity.value = Number(sale.value.quantity)
    priceAtSale.value = Number(sale.value.priceAtSale)
  }
}

async function onDeleteSale() {
  if (sale.value) {
    try {
      await deleteSale(sale.value.id)
      toast.success('Produsul a fost sters cu succes')
      await router.replace({ name: 'SalesHistory' })
    } catch (error) {
      toast.error('A aparut o eroare la stergerea produsului')
    }
  }
}

async function submitForm() {
  errors.value = {}

  if (!selectedDestinationId.value) {
    errors.value['destination'] = 'Selecteaza o destinatie'
  }
  if (!quantity.value || quantity.value <= 0) {
    errors.value['quantity'] = 'Cantitatea trebuie sa fie un numar pozitiv'
  }
  if (!priceAtSale.value || priceAtSale.value <= 0) {
    errors.value['price'] = 'Pretul trebuie sa fie un numar pozitiv'
  }

  if(Object.values(errors.value).length > 0) {
    return;
  }

  const saleEditRequest: SaleEditRequest = {
    saleDate: saleDate.value,
    destinationId: selectedDestinationId.value,
    quantity: quantity.value,
    priceAtSale: priceAtSale.value,
  }
  if (!sale.value) {
    return
  }
  try {
    await updateSale(sale.value.id, saleEditRequest)
    toast.success('Produs actualizat cu succes')
    await router.replace({ name: 'SalesHistory' })
  } catch (error) {
    toast.error('A aparut o eroare')
  }
}
</script>