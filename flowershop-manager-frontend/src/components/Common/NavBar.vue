<script setup lang="ts">
import { useAuthStore } from '@/stores/auth'
import router from '@/router'
import { toast } from 'vue-sonner'
import { ref } from 'vue'

const auth = useAuthStore()
const showMenu = ref(false)

const logout = () => {
  auth.logout()
  showMenu.value = false
  toast.success('V-ați deconectat cu succes')
}
</script>

<template>
  <header
    v-if="auth.token"
    class="sticky w-full shadow-sm top-0 z-50 bg-[#181825] text-gray-50 ">
    <div class="mx-auto px-4 py-3">
      <div
        @click="showMenu = !showMenu"
        class="flex items-center justify-between cursor-pointer"
      >
        <div class="flex items-center gap-2">
          <!-- Brand -->
          <div class="h-8 w-8 rounded-full bg-gray-200 flex items-center justify-center text-sm font-medium text-gray-700 focus:outline-none">
            {{ auth.name?.[0] ?? 'A' }}
          </div>
          <span
            class="text-lg font-semibold "
            v-if="auth.token"
          >
          {{ auth.name }}
          </span>
        </div>

        <i class="fa fa-angle-down"></i>
      </div>
      <div
        v-if="showMenu"
        class="absolute sm:max-w-lg max-w-xs right-1 ml-2 top-14 mt-1 bg-cards shadow-lg border border-gray-600 rounded-xl p-2 flex items-center space-x-2 z-10 w-max"
      >
        <button
          @click="logout"
          class="bg-red-600 text-white p-2 text-sm rounded-lg hover:bg-red-700"
        >
          Deconectare
        </button>
      </div>
    </div>
  </header>
</template>
