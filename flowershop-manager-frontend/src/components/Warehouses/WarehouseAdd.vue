<template>
  <form @submit.prevent="handleSubmit" class="flex flex-col justify-center items-center h-screen space-y-10">
    <div class="flex flex-col space-y-1">
      <label for="name" class="text-body text-text_primary">Nume</label>
      <input
          v-model="warehouse.name"
          id="name"
          type="text"
          placeholder="Solar mare"
          class="bg-[#2a2a40] text-lg text-text_secondary placeholder-gray-400 rounded-xl px-4 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-500"
      />
    </div>
    <div class="flex justify-center space-x-2">
      <button @click="goBack" type="button" class="bg-error text-text_accents py-3 px-8 rounded-lg">Anuleaza</button>
      <button type="submit" class="bg-accent2 text-text_accents py-3 px-8 rounded-lg">Adauga</button>
    </div>
  </form>
</template>
<script setup lang="ts">
import {ref} from "vue";
import { addWarehouse } from "@/services/WarehousesService.ts";
import router from '@/router'

const warehouse = ref({
  name: ""
})

async function handleSubmit() {
  try{
    await addWarehouse(warehouse.value)
    await router.push({ path: "/warehouses" })
  }
  catch (error){
    console.log(error)
  }
}

async function goBack(){
  await router.push({ path: "/warehouses" })
}
</script>