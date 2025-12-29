<template>
  <div class="min-h-screen p-4 sm:p-6">
    <div class="mx-auto max-w-7xl space-y-4 sm:space-y-6">
      <!-- Header -->
      <div class="flex items-center justify-between ">
        <div>
          <h1 class="text-xl font-semibold text-text_primary sm:text-2xl">
            Istoric Vanzari
          </h1>
        </div>

        <!-- Filter button (mobile) -->
        <button
          class="rounded-lg bg-cards px-3 py-2 text-sm shadow-sm sm:hidden"
          @click="showfilters = !showfilters"
        >
          <i class="fa fa-rectangle-list text-white"></i>
        </button>
      </div>

      <!-- Filters -->
      <div class="rounded-xl bg-white p-4 shadow-sm" v-if="showfilters">
        <div class="grid grid-cols-1 gap-4 sm:grid-cols-3">
          <!-- Origin -->
          <div>
            <label class="mb-1 block text-sm font-medium text-gray-700">
              Origine
            </label>
            <select class="w-full rounded-lg p-2 text-sm">
              <option>Toate originile</option>
            </select>
          </div>

          <!-- Destination -->
          <div>
            <label class="mb-1 block text-sm font-medium text-gray-700">
              Destinatie
            </label>
            <select class="w-full rounded-lg p-2 text-sm">
              <option>Toate destinatiile</option>
            </select>
          </div>

          <!-- Actions -->
          <div class="flex items-end justify-end">
            <button class="w-full sm:w-[50%] rounded-lg bg-accent2 px-4 py-2 text-sm text-white">
              Aplica
            </button>
          </div>

        </div>
      </div>

      <!-- Date Navigator -->
      <div class="flex items-center justify-between rounded-xl bg-white p-3 shadow-sm mb-4">
        <button @click="goPrevious" class="rounded-lg border px-3 py-1 text-sm">←</button>

        <!-- Current period display + open picker -->
        <div class="flex items-center gap-2 cursor-pointer relative">
          <div @click="pickerOpen = !pickerOpen" class="flex items-center gap-2">
            <p class="text-lg font-medium text-gray-900">{{ formattedPeriod }}</p>
            <i class="fa fa-angle-down text-gray-500"></i>
          </div>

          <!-- Picker Popover -->
          <div v-if="pickerOpen" class="absolute top-10 left-1/2 -translate-x-1/2 z-20 w-60 rounded-lg bg-white p-4 shadow-lg">
            <label class="block text-sm font-medium text-gray-700 mb-1">Vizualizare</label>
            <select v-model="viewType" class="w-full mb-3 rounded-lg border-gray-300 text-sm">
              <option value="day">Zi</option>
              <option value="month">Luna</option>
            </select>

            <label class="block text-sm font-medium text-gray-700 mb-1">
              Selecteaza {{ viewType === 'day' ? 'Ziua' : 'Luna' }}
            </label>

            <input
              :type="viewType === 'day' ? 'date' : 'month'"
              :key="viewType"
              v-model="inputDate"
              class="w-full rounded-lg border-gray-300 text-sm"
              @change="updateSelectedDate"
            />

            <button @click="pickerOpen = false"
                    class="mt-3 w-full rounded-lg bg-accent2 px-3 py-2 text-white text-sm">
              Aplica
            </button>
          </div>
        </div>

        <button @click="goNext" class="rounded-lg border px-3 py-1 text-sm">→</button>
      </div>

      <!-- Summary -->
<!--      <div class="grid grid-cols-1 gap-4 md:grid-cols-3">-->
<!--        <div class="rounded-xl bg-white p-4 shadow-sm">-->
<!--          <p class="text-sm text-gray-500">Total Sales</p>-->
<!--          <p class="text-2xl font-semibold text-gray-900">124</p>-->
<!--        </div>-->

<!--        <div class="rounded-xl bg-white p-4 shadow-sm">-->
<!--          <p class="text-sm text-gray-500">Total Revenue</p>-->
<!--          <p class="text-2xl font-semibold text-gray-900">€8,420</p>-->
<!--        </div>-->

<!--        <div class="rounded-xl bg-white p-4 shadow-sm">-->
<!--          <p class="text-sm text-gray-500">Products Sold</p>-->
<!--          <p class="text-2xl font-semibold text-gray-900">1,352</p>-->
<!--        </div>-->
<!--      </div>-->


      <!-- Sales Table -->
      <div>
        <div class="overflow-x-auto rounded-xl bg-white shadow-sm">
          <table class="min-w-full divide-y divide-gray-200 text-sm">
            <caption v-if="viewType === 'month'" class="caption-top text-left px-4 py-3 text-lg font-bold text-gray-700">Luni - 12.12.2025</caption>
            <thead class="bg-gray-50">
            <tr>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Produs</th>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Origine</th>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Destinatie</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Cantitate</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Pret / unitate</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Total</th>
              <th class="px-4 py-3 text-right">Actiuni</th>
            </tr>
            </thead>

            <tbody class="divide-y divide-gray-100">
            <tr class="hover:bg-gray-50">
              <td class="px-4 py-3 font-medium text-gray-900">Roses (Red)</td>
              <td class="px-4 py-3 text-gray-700">Warehouse A</td>
              <td class="px-4 py-3 text-gray-700">Store B</td>
              <td class="px-4 py-3 text-right">24</td>
              <td class="px-4 py-3 text-right">€2.50</td>
              <td class="px-4 py-3 text-right text-gray-700">6 Lei</td>
              <td class="px-4 py-3 text-right whitespace-nowrap">
                <button
                  @click="()=>{}"
                  class="inline-flex items-center gap-1 rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 hover:bg-blue-100"
                >
                  Editare
                </button>
              </td>
            </tr>
            <tr class="hover:bg-gray-50">
              <td class="px-4 py-3 font-medium text-gray-900">Tulips (Yellow)</td>
              <td class="px-4 py-3 text-gray-700">Warehouse C</td>
              <td class="px-4 py-3 text-gray-700">Store D</td>
              <td class="px-4 py-3 text-right">15</td>
              <td class="px-4 py-3 text-right">€1.80</td>
              <td class="px-4 py-3 text-right text-gray-700">8 Lei</td>
              <td class="px-4 py-3 text-right whitespace-nowrap">
                <button
                  @click="()=>{}"
                  class="inline-flex items-center gap-1 rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 hover:bg-blue-100"
                >
                  Editare
                </button>
              </td>
            </tr>
            </tbody>
            <tfoot>
            <tr class=" border-t font-semibold text-gray-900">
              <td colspan="7">
                <div class="flex justify-end items-end p-3 gap-3">
                  <span class="text-lg">Total</span>
                  <span class="text-lg">100 Lei</span>
                </div>
              </td>
            </tr>
            </tfoot>
          </table>
        </div>

        <div class="w-full flex justify-end mt-4">
          <button
            @click="()=>{}"
            class="inline-flex items-center gap-2 rounded-lg bg-accent2 px-4 py-2 text-sm font-medium text-white "
          >
            <i class="fa fa-plus"></i>
            Adauga vanzare
          </button>
        </div>
      </div>

      <div v-if="viewType === 'month'">
        <div class="overflow-x-auto rounded-xl bg-white shadow-sm">
          <table class="min-w-full divide-y divide-gray-200 text-sm">
            <caption v-if="viewType === 'month'" class="caption-top text-left px-4 py-3 text-lg font-bold text-gray-700">Marti - 12.12.2025</caption>
            <thead class="bg-gray-50">
            <tr>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Product</th>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Origin</th>
              <th class="px-4 py-3 text-left font-medium text-gray-600">Destination</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Qty</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Price / unit</th>
              <th class="px-4 py-3 text-right font-medium text-gray-600">Total</th>
              <th class="px-4 py-3 text-right">Actions</th>
            </tr>
            </thead>

            <tbody class="divide-y divide-gray-100">
            <tr class="hover:bg-gray-50">
              <td class="px-4 py-3 font-medium text-gray-900">Roses (Red)</td>
              <td class="px-4 py-3 text-gray-700">Warehouse A</td>
              <td class="px-4 py-3 text-gray-700">Store B</td>
              <td class="px-4 py-3 text-right">24</td>
              <td class="px-4 py-3 text-right">€2.50</td>
              <td class="px-4 py-3 text-right text-gray-700">6</td>
              <td class="px-4 py-3 text-right whitespace-nowrap">
                <button
                  @click="()=>{}"
                  class="inline-flex items-center gap-1 rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 hover:bg-blue-100"
                >
                  Edit
                </button>
              </td>
            </tr>
            <tr class="hover:bg-gray-50">
              <td class="px-4 py-3 font-medium text-gray-900">Tulips (Yellow)</td>
              <td class="px-4 py-3 text-gray-700">Warehouse C</td>
              <td class="px-4 py-3 text-gray-700">Store D</td>
              <td class="px-4 py-3 text-right">15</td>
              <td class="px-4 py-3 text-right">€1.80</td>
              <td class="px-4 py-3 text-right text-gray-700">8</td>
              <td class="px-4 py-3 text-right whitespace-nowrap">
                <button
                  @click="()=>{}"
                  class="inline-flex items-center gap-1 rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-blue-700 hover:bg-blue-100"
                >
                  Edit
                </button>
              </td>
            </tr>
            </tbody>
            <tfoot>
            <tr class=" border-t font-semibold text-gray-900">
              <td class="px-4 py-3 text-lg">
                Total
              </td>
              <td colspan="5"></td>
              <td class="px-4 py-3 text-right text-lg">
                100 €
              </td>
            </tr>
            </tfoot>
          </table>
        </div>

        <!-- Add sale button -->
        <div class="w-full flex justify-end mt-4">
          <button
            @click="()=>{}"
            class="inline-flex items-center gap-2 rounded-lg bg-accent2 px-4 py-2 text-sm font-medium text-white "
          >
            <i class="fa fa-plus"></i>
            Adauga vanzare
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'

const pickerOpen = ref(false)
const viewType = ref<'day' | 'month'>('day')
const selectedDate = ref(new Date())
const inputDate = ref('')

const showfilters = ref(true)


const formattedPeriod = computed(() => {
  if (viewType.value === 'day') {
    return selectedDate.value.toLocaleDateString('ro-RO')
  } else {
    return selectedDate.value.toLocaleString('Ro-RO', { month: 'long', year: 'numeric' }).replace(/^\p{L}/u, c => c.toUpperCase());
  }
})


function goPrevious() {
  const date = new Date(selectedDate.value)
  if (viewType.value === 'day') date.setDate(date.getDate() - 1)
  else date.setMonth(date.getMonth() - 1)
  selectedDate.value = date
  updateInputDate()
}

function goNext() {
  const date = new Date(selectedDate.value)
  if (viewType.value === 'day') date.setDate(date.getDate() + 1)
  else date.setMonth(date.getMonth() + 1)
  selectedDate.value = date
  updateInputDate()
}

function updateSelectedDate() {
  if (!inputDate.value) return
  if (viewType.value === 'day') {
    selectedDate.value = new Date(inputDate.value)
  } else {
    const [year, month] = inputDate.value.split('-').map(Number)
    selectedDate.value = new Date(year, month - 1, 1)
  }
  pickerOpen.value = false
}

function updateInputDate() {
  if (viewType.value === 'day') {
    inputDate.value = selectedDate.value.toISOString().split('T')[0] // yyyy-mm-dd
  } else {
    const y = selectedDate.value.getFullYear()
    const m = (selectedDate.value.getMonth() + 1).toString().padStart(2, '0')
    inputDate.value = `${y}-${m}` // yyyy-mm
  }
}

// Initialize input
updateInputDate()

// When viewType changes, update inputDate
watch(viewType, () => {
  updateInputDate()
})
</script>

