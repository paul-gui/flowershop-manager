<template>
  <div class="h-full flex flex-col items-center justify-center">
    <div class="max-w-sm p-6 w-full">
      <form @submit.prevent="handleFormSubmit" class="space-y-6 p-8 bg-cards rounded-lg shadow-md">
        <span @click="goBack" class="text-text_secondary cursor-pointer"><i class="fa fa-arrow-left"></i> Înapoi</span>
        <h2 class="text-2xl font-bold text-center text-text_primary">Solicită resetarea parolei</h2>
        <div>
          <label class="block mb-1 font-medium text-text_secondary">Email</label>
          <input
            v-model="email"
            type="text"
            class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
          />
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['email']">
            {{ validationErrors['email'] }}
          </p>
        </div>
        <button
          type="submit"
          :disabled="loading"
          class="w-full bg-accent3 text-text_accents py-2 rounded-md hover:bg-[rgb(128,100,128)]"
        >
          <!-- Show spinner when loading -->
          <i v-if="loading" class="fas fa-spinner fa-spin"></i>

          <!-- Show text only when not loading -->
          <span v-else>Resetare</span>
        </button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { forgotPassword } from '@/services/AuthenticationService.ts'
import type { ForgotPasswordRequest } from '@/types/dtos/authentication/authenticationRequests.dto.ts'
import { toast } from 'vue-sonner'
import router from '@/router'

const email = ref('')

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

const validationErrors = ref<Record<string, string>>({})
const loading = ref<boolean>(false)

async function goBack() {
  await router.replace({ path: '/'})
}

async function handleFormSubmit() {
  validationErrors.value = {}

  if (email.value === '' || !emailRegex.test(email.value)) {
    validationErrors.value['email'] = 'Introduceți un email valid'
  }

  if (Object.values(validationErrors.value).length > 0) {
    return
  }

  try {
    loading.value = true

    const forgotPasswordRequest: ForgotPasswordRequest = {
      email: email.value,
    }
    await forgotPassword(forgotPasswordRequest)

    toast.success('S-a trimis un email de resetare al parolei')
    await router.push({ path: '/' })
  }
  catch (error: any) {
    const status = error.response?.status
    if (status === 429) {
      toast.error('Prea multe încercări, reveniți mai târziu')
    }
    else {
      toast.error('A apărut o eroare la solicitarea de resetare a parolei')
    }
  }
  finally {
    loading.value = false
  }
}
</script>
