<template>
  <form @submit.prevent="handleRegistration" class="space-y-6 bg-cards p-8 rounded-lg shadow-md">
    <h2 class="text-2xl font-bold text-center text-text_primary">Inregistrare</h2>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Nume</label>
      <input
        v-model="user.name"
        type="text"
        class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
      />
      <p class="ms-2 text-xs text-red-500" v-if="validationErrors['name']">
        {{ validationErrors['name'] }}
      </p>
    </div>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Email</label>
      <input
        v-model="user.email"
        type="text"
        class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
      />
      <p class="ms-2 text-xs text-red-500" v-if="validationErrors['email']">
        {{ validationErrors['email'] }}
      </p>
    </div>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Parola</label>
      <input
        v-model="user.password"
        type="password"
        class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
      />
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
      <label class="block mb-1 font-medium text-text_secondary">Confirmare Parola</label>
      <input
        v-model="user.passwordConfirmation"
        type="password"
        class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary"
      />
      <p class="ms-2 text-xs text-red-500" v-if="validationErrors['passwordConfirmation']">
        {{ validationErrors['passwordConfirmation'] }}
      </p>
    </div>
    <button
      type="submit"
      class="w-full bg-accent3 text-text_accents py-2 rounded-md hover:bg-[rgb(128,100,128)]"
    >
      Inregistrare
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { register } from '@/services/AuthenticationService.js'
import { toast } from 'vue-sonner'
import { useAuthStore } from '@/stores/auth.ts'
import type { LoginRequest } from '@/types/dtos/authentication/authenticationRequests.dto.ts'
import router from '@/router'

const auth = useAuthStore()

const user = ref({
  email: '',
  password: '',
  passwordConfirmation: '',
  name: '',
})

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

const hasLowercase = /[a-z]/
const hasUppercase = /[A-Z]/
const hasNumber = /\d/
const hasSpecial = /[^A-Za-z0-9]/
const hasMinLength = /^.{6,}$/

const validationErrors = ref<Record<string, string>>({})

async function handleRegistration() {
  validationErrors.value = {}

  const loginCredentials: LoginRequest = {
    email: user.value.email,
    password: user.value.password,
  }

  //Perform validations
  if (user.value.name === '') {
    validationErrors.value['name'] = 'Introduceti un nume'
  }
  if (user.value.email === '' || !emailRegex.test(user.value.email)) {
    validationErrors.value['email'] = 'Introduceti un email valid'
  }
  if (user.value.password !== '') {
    if (!hasLowercase.test(user.value.password)) {
      validationErrors.value['pLowercase'] = 'Parola trebuie sa contina macar o litera mica'
    }
    if (!hasUppercase.test(user.value.password)) {
      validationErrors.value['pUppercase'] = 'Parola trebuie sa contina macar o litera mare'
    }
    if (!hasNumber.test(user.value.password)) {
      validationErrors.value['pNumber'] = 'Parola trebuie sa contina macar o cifra'
    }
    if (!hasSpecial.test(user.value.password)) {
      validationErrors.value['pSpecial'] = 'Parola trebuie sa contina macar un caracter special'
    }
    if (!hasMinLength.test(user.value.password)) {
      validationErrors.value['pMinLength'] = 'Parola trebuie sa fie de minim 6 caractere'
    }
  } else {
    validationErrors.value['password'] = 'Introduceti o parola'
  }
  if (user.value.password !== user.value.passwordConfirmation) {
    validationErrors.value['passwordConfirmation'] = 'Parolele nu se potrivesc'
  }

  if (Object.values(validationErrors.value).length > 0) {
    return
  }

  try {
    await register(user.value)
    await auth.login(loginCredentials)

    toast.success('V-ati inregistrat cu succes')
    await router.replace({ path: '/warehouses'})
  }
  catch (error) {
    toast.error('A avut loc o eroare in timpul inregistrarii')
  }
}
</script>