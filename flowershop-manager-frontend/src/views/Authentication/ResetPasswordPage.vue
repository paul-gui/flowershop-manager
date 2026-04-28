<template>
  <div class="h-full flex flex-col items-center justify-center">
    <div class="max-w-sm p-6 w-full">
      <form @submit.prevent="handleResetPassword" class="space-y-6 p-8 bg-cards rounded-lg shadow-md">
        <h2 class="text-2xl font-bold text-center text-text_primary">Resetare parolă</h2>
        <div>
          <label class="block mb-1 font-medium text-text_secondary">Parolă nouă</label>
          <input v-model="password" type="password" class="w-full p-2 rounded-md border border-cards bg-divider focus:outline-none focus:ring-2 focus:ring-indigo-500 text-text_secondary"/>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['password']">
            {{ validationErrors['password'] }}
          </p>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['pLowercase']">
            {{ validationErrors['pLowercase'] }}
          </p>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['pUppercase']">
            {{ validationErrors['pUppercase'] }}
          </p>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['pNumber']">
            {{ validationErrors['pNumber'] }}
          </p>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['pSpecial']">
            {{ validationErrors['pSpecial'] }}
          </p>
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['pMinLength']">
            {{ validationErrors['pMinLength'] }}
          </p>
        </div>
        <div>
          <label class="block mb-1 font-medium text-text_secondary">Confirmare Parolă</label>
          <input
            v-model="passwordConfirmation"
            type="password"
            class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
          />
          <p class="ms-2 text-xs text-red-500" v-if="validationErrors['passwordConfirmation']">
            {{ validationErrors['passwordConfirmation'] }}
          </p>
        </div>
        <button type="submit" :disabled="loading" class="w-full bg-accent3 text-text_accents py-2 rounded-md hover:bg-[rgb(128,100,128)]">
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
import { resetPassword } from '@/services/AuthenticationService.ts'
import type { ResetPasswordRequest } from '@/types/dtos/authentication/authenticationRequests.dto.ts'
import { toast } from 'vue-sonner'
import router from '@/router'

const props = defineProps(['email', 'token'])

const password = ref('')
const passwordConfirmation = ref('')

const hasLowercase = /[a-z]/
const hasUppercase = /[A-Z]/
const hasNumber = /\d/
const hasSpecial = /[^A-Za-z0-9]/
const hasMinLength = /^.{6,}$/

const validationErrors = ref<Record<string, string>>({})
const loading = ref<boolean>(false)

async function handleResetPassword() {
  validationErrors.value = {}

  if (password.value !== '') {
    if (!hasLowercase.test(password.value)) {
      validationErrors.value['pLowercase'] = 'Parola trebuie să conțină măcar o literă mică'
    }
    if (!hasUppercase.test(password.value)) {
      validationErrors.value['pUppercase'] = 'Parola trebuie să conțină măcar o literă mare'
    }
    if (!hasNumber.test(password.value)) {
      validationErrors.value['pNumber'] = 'Parola trebuie să conțină măcar o cifră'
    }
    if (!hasSpecial.test(password.value)) {
      validationErrors.value['pSpecial'] = 'Parola trebuie să conțină măcar un caracter special'
    }
    if (!hasMinLength.test(password.value)) {
      validationErrors.value['pMinLength'] = 'Parola trebuie să fie de minim 6 caractere'
    }
  } else {
    validationErrors.value['password'] = 'Introduceți o parolă'
  }
  if (password.value !== passwordConfirmation.value) {
    validationErrors.value['passwordConfirmation'] = 'Parolele nu se potrivesc'
  }

  if (Object.values(validationErrors.value).length > 0) {
    return
  }

  if (!props.token || !props.email){
    toast.error("Link-ul de resetare a parolei nu este valid")
    return
  }

  try{
    loading.value = true

    const resetPasswordRequest: ResetPasswordRequest = {
      token: props.token,
      email: props.email,
      password: password.value,
      passwordConfirmation: passwordConfirmation.value,
    }

    await resetPassword(resetPasswordRequest)
    toast.success('Parola a fost resetată cu succes')
    await router.push({ path: '/' })
  }
  catch (error: any) {
    const status = error.response?.status
    if (status === 429) {
      toast.error('Prea multe încercări, reveniți mai târziu')
    }
    else {
      toast.error('A apărut o eroare la resetarea parolei')
    }
  }
  finally {
    loading.value = false
  }
}
</script>
