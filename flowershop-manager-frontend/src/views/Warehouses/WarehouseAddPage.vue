<template>
  <div class="h-full bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-2">
      <h1 class="text-h1 text-text_primary">Adăugare locație</h1>
      <form @submit.prevent="handleSubmit" class="flex flex-col justify-center items-center space-y-6 w-full">
        <div class="w-full">
          <label for="name" class="block text-sm mb-1">Denumire locație</label>
          <input
            v-model="warehouse.name"
            id="name"
            type="text"
            class="bg-[#2a2a40] w-full text-lg text-text_secondary placeholder-gray-400 rounded-xl px-4 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
          <p class="text-red-500 text-sm" v-if="errors['name']">{{ errors['name'] }}</p>
        </div>
        <div class="grid grid-cols-2 gap-2">
          <button @click="goBack" type="button" class="bg-cards hover:bg-[#3c3860] text-gray-50 py-3 px-8 rounded-lg">Anulează</button>
          <button type="submit" class="bg-accent2 hover:bg-accent2_hover text-text_accents py-3 px-8 rounded-lg">Salvează</button>
        </div>
      </form>
    </div>
  </div>
</template>
<script setup lang="ts">
import {ref} from "vue";
import { createWarehouse } from "@/services/WarehousesService.ts";
import router from '@/router'
import { toast } from 'vue-sonner'

const warehouse = ref({
  name: ""
})
const errors = ref<Record<string, string>>({})

async function handleSubmit() {
  if(warehouse.value.name === ''){
    errors.value['name'] = 'Introduceți numele locației'
    return
  }

  try{
    await createWarehouse(warehouse.value)
    toast.success('Locație creată cu succes')
    await router.replace({ path: "/warehouses" })
  }
  catch (error){
    toast.error('A apărut o eroare')
  }
}

async function goBack(){
  await router.replace({ path: "/warehouses" })
}
</script>