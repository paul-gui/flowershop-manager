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
    <button type="submit" class="bg-accent2 py-3 px-8 rounded-lg">Adauga</button>
  </form>
</template>
<script setup lang="ts">
import {ref} from "vue";
import { addWarehouse } from "@/services/WarehousesService";

const warehouse = ref({
  name: ""
})

async function handleSubmit() {
  try{
    const result = await addWarehouse(warehouse.value)
    console.log(result)
  }
  catch (error){
    console.log(error)
  }
}
</script>