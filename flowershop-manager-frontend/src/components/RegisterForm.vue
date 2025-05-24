<template>
    <form @submit.prevent="handleRegistration" class="space-y-6 bg-cards p-8 rounded-lg shadow-md">
      <h2 class="text-2xl font-bold text-center text-text_primary">Register</h2>
      <div>
        <label class="block mb-1 font-medium text-text_secondary">Name</label>
        <input v-model="user.name" type="text" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary" required />
      </div>
      <div>
        <label class="block mb-1 font-medium text-text_secondary">Email</label>
        <input v-model="user.email" type="email" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary" required />
      </div>
      <div>
        <label class="block mb-1 font-medium text-text_secondary">Password</label>
        <input v-model="user.password" type="password" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary" required />
      </div>
      <div>
        <label class="block mb-1 font-medium text-text_secondary">Confirm Password</label>
        <input v-model="user.passwordConfirmation" type="password" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:border-gray-500 text-text_secondary" required />
      </div>
      <button type="submit" class="w-full bg-accent2 text-text_accents py-2 rounded-md hover:bg-green-400">
        Register
      </button>
      <ul v-if="errors.length" class="text-error mt-3 text-caption max-w-48">
        <li class="mt-2" v-for="(err, index) in errors" :key="index">- {{ err }}</li>
      </ul>
    </form>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { register } from '@/services/AuthenticationService.js'

const user = ref({
  email: "",
  password: "",
  passwordConfirmation: "",
  name: "",
})

const errors = ref<string[]>([])
const successMessage = ref("")

async function handleRegistration() {
  errors.value = []
  //Attempt user registration
  try{
    //Check if passwords match
    if (user.value.password !== user.value.passwordConfirmation) {
      throw {
        response:{
          data:[{
            description: 'Passwords do not match',
          }]
        }
      };
    }
    const res = await register(user.value)
    successMessage.value = 'Account created!'
  } catch (err) {
    if (err.response?.data && Array.isArray(err.response.data)) {
      errors.value = err.response.data.map((e:any) => e.description)
    }
    else {
      errors.value = ['An unexpected error occurred.']
    }
  }
}
</script>