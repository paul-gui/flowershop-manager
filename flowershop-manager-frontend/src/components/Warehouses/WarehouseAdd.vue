<template>
  <div class="h-screen bg-background content-center text-text_primary p-4 py-12 space-y-4">
    <div class="max-w-md mx-auto space-y-2">
      <h1 class="text-h1 text-text_primary">Adaugare locatie</h1>
      <form @submit.prevent="handleSubmit" class="flex flex-col justify-center items-center space-y-6 w-full">
        <div class="w-full">
          <label for="name" class="block text-sm mb-1">Denumire locatie</label>
          <input
            v-model="warehouse.name"
            id="name"
            type="text"
            class="bg-[#2a2a40] w-full text-lg text-text_secondary placeholder-gray-400 rounded-xl px-4 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div class="grid grid-cols-2 gap-2">
          <button @click="goBack" type="button" class="bg-cards hover:bg-[#3c3860] text-gray-50 py-3 px-8 rounded-lg">Anuleaza</button>
          <button type="submit" class="bg-accent2 text-text_accents hover:bg-green-600 py-3 px-8 rounded-lg">Salveaza</button>
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

async function handleSubmit() {
  try{
    await createWarehouse(warehouse.value)
    toast.success('Locatie creata cu succes')
    await router.replace({ path: "/warehouses" })
  }
  catch (error){
    toast.error('A aparut o eroare')
  }
}

async function goBack(){
  await router.replace({ path: "/warehouses" })
}
</script>