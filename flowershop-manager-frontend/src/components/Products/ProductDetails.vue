<script setup lang="ts">
import { ref } from 'vue'
import router from "@/router";

const name = ref('Muscata tufa')
const florariePrice = ref('')
const enGrosPrice = ref('')

function cancel() {
  router.back();
}

function save() {
  console.log('Saving:', {
    name: name.value,
    florariePrice: florariePrice.value,
    enGrosPrice: enGrosPrice.value,
  })
}
</script>

<template>
  <div class="min-h-screen bg-[#1A1A2E] text-white flex items-center justify-center p-6">
    <div class="w-full max-w-sm space-y-6">
      <div>
        <label class="block text-sm mb-1">Denumire</label>
        <input
            v-model="name"
            type="text"
            class="w-full bg-cards text-xl text-text_secondary px-4 py-2 rounded-xl focus:outline-none"
        />
      </div>

      <div>
        <label class="block text-gray-300 mb-2">Pret</label>
        <div class="space-y-3">
          <div class="flex items-center justify-between">
            <span class="text-h3 text-gray-300">Florarie</span>
            <input
                v-model="florariePrice"
                type="number"
                class="w-2/3 px-4 py-2 rounded-xl bg-[#2E2E4D] text-white focus:outline-none"
            />
          </div>
          <div class="flex items-center justify-between">
            <span class="text-h3 text-gray-300">En gros</span>
            <input
                v-model="enGrosPrice"
                type="number"
                class="w-2/3 px-4 py-2 rounded-xl bg-[#2E2E4D] text-white focus:outline-none"
            />
          </div>
        </div>
      </div>

      <div class="fixed bottom-8 left-0 flex justify-center mt-6 space-x-4 w-full">
        <button @click="cancel" class="bg-error hover:bg-red-600 text-text_accents py-3 px-10 rounded-xl">Anuleaza</button>
        <button @click="save" class="bg-accent2 hover:bg-green-600 text-text_accents py-3 px-10 rounded-xl">Salveaza</button>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>