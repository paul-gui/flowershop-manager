<script setup lang="ts">
import { onMounted } from 'vue'
import { useProductFormLogic } from '@/components/Products/productFormLogic.ts'
import router from '@/router'

const props = defineProps<{ id: string }>()
const upsert = useProductFormLogic({ warehouseId: props.id })

onMounted(async () => {
  await upsert.hydrate()
})

async function onSubmit() {
  await upsert.submitProduct()
  goBack()
}

function onCancel() {
  goBack()
}

function goBack() {
  router.replace({ path: '/warehouse-details/' + props.id })
}
</script>

<template>
  <div class="h-full bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-6">
      <h1 class="text-h1 text-text_primary">Adaugare produs</h1>
      <div>
        <label class="block text-sm  mb-1">Denumire produs</label>
        <input
          v-model="upsert.form.value.name"
          type="text"
          class="w-full bg-cards text-xl text-gray-50 px-4 py-2 rounded-xl focus:outline-none"
        />
      </div>

      <div>
        <label class="block mb-2">Pret</label>
        <div class="space-y-3">
          <div
            v-for="p in upsert.form.value.prices"
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
