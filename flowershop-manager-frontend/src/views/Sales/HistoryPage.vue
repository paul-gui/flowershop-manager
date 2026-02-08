<template>
  <div class="h-full p-4 sm:p-6">
    <div class="mx-auto max-w-7xl space-y-4 sm:space-y-6">
      <!-- Header -->
      <div class="flex items-end justify-between">
        <div>
          <button
            class="text-sm text-text_primary rounded-xl mb-3"
            @click="goBack"
          >
            <i class="fa fa-arrow-left"></i>
            Înapoi
          </button>
          <h1 class="text-xl font-semibold text-text_primary sm:text-2xl">Istoric Vânzări</h1>
        </div>

        <!-- Filter button (mobile) -->
        <button
          class="rounded-lg bg-cards px-3 py-2 text-sm shadow-sm sm:hidden"
          @click="showFilters = !showFilters"
        >
          <i class="fa fa-rectangle-list text-text_primary"></i>
        </button>
      </div>

      <!-- Filters -->
      <div class="rounded-xl bg-white p-4 shadow-sm" v-if="showFilters">
        <div class="grid grid-cols-1 gap-4 sm:grid-cols-[3fr_3fr_1fr]">
          <!-- Origin -->
          <div>
            <label class="mb-1 block text-sm font-medium text-gray-700"> Origine </label>
            <select
              class="w-full rounded-lg p-2 text-sm hover:bg-gray-100 cursor-pointer"
              v-model="salesFilterForm.warehouseId"
            >
              <option
                v-for="warehouse in warehouses"
                :key="warehouse.id ?? 'all'"
                :value="warehouse.id"
              >
                {{ warehouse.name }}
              </option>
            </select>
          </div>

          <!-- Destination -->
          <div>
            <label class="mb-1 block text-sm font-medium text-gray-700"> Destinație </label>
            <select
              class="w-full rounded-lg p-2 text-sm hover:bg-gray-100 cursor-pointer"
              v-model="salesFilterForm.destinationId"
            >
              <option
                v-for="destination in destinations"
                :key="destination.id || 'all'"
                :value="destination.id"
              >
                {{ destination.name }}
              </option>
            </select>
          </div>

          <!-- Actions -->
          <div class="flex items-end justify-end">
            <button
              class="w-full rounded-lg bg-accent2 hover:bg-accent2_hover px-4 py-2 text-sm text-white"
              @click="getSalesData"
            >
              Aplică
            </button>
          </div>
        </div>
      </div>

      <!-- Date Navigator -->
      <div class="flex items-center justify-between rounded-xl bg-white p-4 shadow-sm mb-4">
        <button
          @click="goPrevious"
          class="rounded-lg px-3 py-1 text-sm bg-gray-100 hover:bg-gray-200"
        >
          ←
        </button>

        <!-- Current period display + open picker -->
        <div
          class="flex items-center gap-2 cursor-pointer relative px-3 py-1 rounded-xl hover:bg-gray-100"
        >
          <div @click="pickerOpen = !pickerOpen" class="flex items-center gap-2">
            <p class="text-lg font-medium text-gray-900">{{ formattedPeriod }}</p>
            <i class="fa fa-angle-down text-gray-500"></i>
          </div>

          <!-- Picker Popover -->
          <div
            v-if="pickerOpen"
            class="absolute top-10 left-1/2 -translate-x-1/2 z-20 w-60 rounded-lg bg-white p-4 shadow-lg border"
          >
            <label class="block text-sm font-medium text-gray-700 mb-1">Vizualizare</label>
            <select v-model="viewType" class="w-full p-1 mb-3 rounded-lg border-gray-300 text-sm">
              <option value="day">Zi</option>
              <option value="month">Lună</option>
            </select>

            <label class="block text-sm font-medium text-gray-700 mb-1">
              Selectează {{ viewType === 'day' ? 'Ziua' : 'Luna' }}
            </label>

            <input
              :type="viewType === 'day' ? 'date' : 'month'"
              :key="viewType"
              v-model="inputDate"
              class="w-full rounded-lg p-1 border-gray-300 text-sm"
              @change="acceptSelectedDate"
            />

            <button
              @click="acceptSelectedDate"
              class="mt-3 w-full rounded-lg bg-accent2 hover:bg-accent2_hover px-3 py-2 text-white text-sm"
            >
              Aplică
            </button>
          </div>
        </div>

        <button @click="goNext" class="rounded-lg px-3 py-1 text-sm bg-gray-100 hover:bg-gray-200">
          →
        </button>
      </div>

      <!-- Sales Table -->
      <div
        class="rounded-xl bg-white shadow-sm mb-4 p-10 flex flex-col items-center justify-center gap-3"
        v-if="isLoading"
      >
        <div class="flex justify-center items-center gap-2">
          <div class="h-10 w-10 animate-spin rounded-full border-4 border-gray-300 border-t-gray-600"></div>
        </div>
      </div>
      <div v-else>
        <div
          class="rounded-xl bg-white shadow-sm mb-4 p-5 flex flex-col items-center justify-center gap-3"
          v-if="!hasAnySales"
        >
          <span class="text-sm font-medium text-gray-700">Nu există vânzări</span>
          <button
            class="border-dashed border-2 p-3 rounded-xl text-sm"
            @click="addProduct(inputDate)"
          >
            <i class="fa fa-plus"></i>
            Adaugă vânzare
          </button>
        </div>

        <div v-else v-for="(daySales, date) in groupedSales" :key="date" class="mt-4">
          <div class="overflow-x-auto rounded-xl bg-white shadow-sm">
            <table class="min-w-full divide-y divide-gray-200 text-sm">
              <caption class="p-4">
                <div class="grid grid-cols-3 items-center">
                  <div class="justify-self-start text-lg font-bold text-gray-700">
                    {{ getFormattedDate(date) }}
                  </div>
                  <div class="justify-self-center text-lg font-bold text-gray-700">
                    {{ getWeekday(date) }}
                  </div>
                  <div class="justify-self-end">
                    <button
                      class="rounded-lg h-10 aspect-square bg-gray-100 hover:bg-gray-200"
                      @click="addProduct(date)"
                    >
                      <i class="fa fa-plus"></i>
                    </button>
                  </div>
                </div>
              </caption>
              <thead class="bg-gray-100">
                <tr>
                  <th class="px-4 py-3 text-left font-semibold text-gray-600">Produs</th>
                  <th class="px-4 py-3 text-left font-semibold text-gray-600">Origine</th>
                  <th class="px-4 py-3 text-left font-semibold text-gray-600">Destinație</th>
                  <th class="px-4 py-3 text-right font-semibold text-gray-600">Cantitate</th>
                  <th class="px-4 py-3 text-right font-semibold text-gray-600">Preț / unitate</th>
                  <th class="px-4 py-3 text-right font-semibold text-gray-600">Total</th>
                  <th class="px-4 py-3 text-right">Acțiuni</th>
                </tr>
              </thead>

              <tbody class="divide-y divide-gray-100">
                <tr class="hover:bg-gray-50" v-for="sale in daySales" :key="sale.id">
                  <td class="px-4 py-3 font-medium text-gray-900">{{ sale.productName }}</td>
                  <td class="px-4 py-3 text-gray-700">{{ sale.warehouseName }}</td>
                  <td class="px-4 py-3 text-gray-700">{{ sale.destinationName }}</td>
                  <td class="px-4 py-3 text-right">{{ sale.quantity }}</td>
                  <td class="px-4 py-3 text-right">{{ sale.priceAtSale }} Lei</td>
                  <td class="px-4 py-3 text-right text-gray-700">
                    {{ Number(sale.quantity) * Number(sale.priceAtSale) }} Lei
                  </td>
                  <td class="px-4 py-3 text-right whitespace-nowrap">
                    <button
                      @click="goToEditProduct(sale.id)"
                      class="inline-flex items-center gap-1 rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 hover:bg-blue-100"
                    >
                      Editare
                    </button>
                  </td>
                </tr>
              </tbody>
              <tfoot>
                <tr class="border-t font-semibold text-gray-900">
                  <td colspan="7">
                    <div class="flex justify-end items-end p-3 gap-3">
                      <span class="text-lg">Total</span>
                      <span class="text-lg">{{ groupedTotals[date] }} Lei</span>
                    </div>
                  </td>
                </tr>
              </tfoot>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref, watch } from 'vue'
import router from '@/router'
import { getSales } from '@/services/SalesService.ts'
import type { SalesFilterForm } from '@/types/models/salesFilterForm.ts'
import { getWarehouses } from '@/services/WarehousesService.ts'
import { getDestinations } from '@/services/ProductsService.ts'
import type { SaleResponse } from '@/types/dtos/sales/saleResponses.ts'
import type { DestinationOption, WarehouseOption } from '@/types/models/salesFilterForm.ts'
import { toast } from 'vue-sonner'

const salesFilterForm = ref<SalesFilterForm>({
  warehouseId: '',
  destinationId: '',
  startDate: '',
  endDate: '',
})

// Field values
const warehouses = ref<WarehouseOption[]>([])
const destinations = ref<DestinationOption[]>([])

// State
const pickerOpen = ref(false)
const viewType = ref<'day' | 'month'>('day')
const inputDate = ref('')
const showFilters = ref(true)
const sales = ref<SaleResponse[]>([])
const isLoading = ref(false)

const groupedSales = computed(() => {
  const groups: Record<string, SaleResponse[]> = {}

  for (const sale of sales.value) {
    const key = getDateKey(sale.saleDate)

    if (!groups[key]) {
      groups[key] = []
    }
    groups[key].push(sale)
  }
  return groups
})

const groupedTotals = computed(() => {
  const totals: Record<string, number> = {}

  for (const [date, daySales] of Object.entries(groupedSales.value)) {
    totals[date] = 0
    for (const sale of daySales) {
      totals[date] += sale.priceAtSale * sale.quantity
    }
  }

  return totals
})

const hasAnySales = computed(() => {
  return Object.values(groupedSales.value).some((daySales) => daySales.length > 0)
})

const formattedPeriod = computed(() => {
  if (viewType.value == 'day') {
    return new Date(inputDate.value).toLocaleDateString('ro-RO')
  } else {
    return new Date(inputDate.value)
      .toLocaleDateString('Ro-RO', { month: 'long', year: 'numeric' })
      .replace(/^\p{L}/u, (c) => c.toUpperCase())
  }
})

onMounted(async () => {
  await hydrateFilters()
  await getSalesData()
})

watch(viewType, () => {
  inputDate.value = getTodayDate()
})

function goPrevious() {
  const [y, m, d] = inputDate.value.split('-').map(Number)
  const date = new Date(y, m - 1, d ? d : 1, 12, 0, 0)

  if (viewType.value === 'day') {
    date.setDate(date.getDate() - 1)
    inputDate.value = date.toISOString().split('T')[0]
  }
  else {
    date.setMonth(date.getMonth() - 1)
    inputDate.value = date.toISOString().split('T')[0].slice(0, 7)
  }

  getSalesData()
}

function goNext() {
  const [y, m, d] = inputDate.value.split('-').map(Number)
  const date = new Date(y, m - 1, d ? d : 1, 12, 0, 0)
  if (viewType.value === 'day') {
    date.setDate(date.getDate() + 1)
    inputDate.value = date.toISOString().split('T')[0]
  }
  else {
    date.setMonth(date.getMonth() + 1)
    inputDate.value = date.toISOString().split('T')[0].slice(0, 7)
  }

  getSalesData()
}

function getFormattedDate(source: string) {
  const date = new Date(source)
  const y = date.getFullYear()
  const m = String(date.getMonth() + 1).padStart(2, '0')
  const d = String(date.getDate()).padStart(2, '0')
  return `${d}.${m}.${y}`
}

function getWeekday(source: string) {
  const date = new Date(source)
  return date
    .toLocaleDateString('ro-RO', { weekday: 'long' })
    .replace(/^\p{L}/u, (c) => c.toUpperCase())
}

function acceptSelectedDate() {
  pickerOpen.value = false
  getSalesData()
}

function getDateRange() {
  if (viewType.value === 'day') {
    const start = inputDate.value
    const end = inputDate.value
    return [start, end]
  }

  const [y, m] = inputDate.value.split('-').map(Number)
  const start = `${y}-${String(m).padStart(2, '0')}-01`

  const lastDay = new Date(y, m, 0).getDate()
  const end = `${y}-${String(m).padStart(2, '0')}-${String(lastDay).padStart(2, '0')}`

  return [start, end]
}

function getDateKey(dateString: string) {
  return dateString.slice(0, 10)
}

function getTodayDate() {
  const now = new Date()
  const y = now.getFullYear()
  const m = String(now.getMonth() + 1).padStart(2, '0')

  if (viewType.value === 'day') {
    const d = String(now.getDate()).padStart(2, '0')
    return `${y}-${m}-${d}`
  }

  return `${y}-${m}`
}

async function hydrateFilters() {
  inputDate.value = getTodayDate()
  warehouses.value = [{ id: null, name: 'Toate originile' }, ...(await getWarehouses())]
  salesFilterForm.value.warehouseId = null

  destinations.value = [{ id: null, name: 'Toate destinațiile' }, ...(await getDestinations())]
  salesFilterForm.value.destinationId = null
}

function addProduct(date: string) {
  router.push({
    name: 'HistoryCreateSale',
    params: {
      saleDate: date,
    }
  })
}

function goToEditProduct(id: string) {
  router.push({ name: 'HistoryEditSale', params: { id: id } })
}

function goBack() {
  router.push({ path:'/warehouses' })
}

async function getSalesData() {
  const [start, end] = getDateRange()
  salesFilterForm.value.startDate = start
  salesFilterForm.value.endDate = end

  try {
    isLoading.value = true
    const res = await getSales(salesFilterForm.value)
    sales.value = res.data
  }
  catch (error) {
    toast.error('A apărut o eroare la încărcarea vânzărilor')
  }
  finally {
    isLoading.value = false
  }

}
</script>
