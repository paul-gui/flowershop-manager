<template>
  <form @submit.prevent="handleLogin" class="space-y-6 bg-cards p-8 rounded-lg shadow-md max-w-md w-full">
    <h2 class="text-2xl font-bold text-center text-text_primary">Login</h2>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Email</label>
      <input v-model="loginForm.email" type="email" class="w-full p-2 rounded-md border border-cards bg-divider focus:outline-none focus:ring-2 focus:ring-indigo-500 text-text_secondary" required />
    </div>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Password</label>
      <input v-model="loginForm.password" type="password" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:ring-2 focus:ring-indigo-500 text-text_secondary" required />
    </div>
    <button type="submit" :disabled="loading" class="w-full bg-accent3 text-text_accents py-2 rounded-md hover:bg-[rgb(128,100,128)]">
      <!-- Show spinner when loading -->
      <i v-if="loading" class="fas fa-spinner fa-spin"></i>

      <!-- Show text only when not loading -->
      <span v-else>Login</span>
    </button>
    <p class="text-center text-error" v-if="error">{{error}}</p>
  </form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { login } from "@/services/AuthenticationService";
import { jwtDecode } from "jwt-decode";
import { useAuthStore } from "@/stores/auth";

const error = ref('');
const loading = ref(false);
const router = useRouter();
const auth = useAuthStore();

const loginForm = ref({
  email: '',
  password: '',
})

watch(loginForm, () => {
  error.value = '';
}, { deep: true });


async function handleLogin() {
  loading.value = true;
  error.value = '';

  try {
    await auth.login(loginForm.value)

    // Redirect after login
   await router.push('/warehouses'); // or whatever your home page is

  } catch (err: any) {
    if (err.response && err.response.data?.message) {
      error.value = err.response.data.message; // from backend
    } else {
      error.value = 'Login failed. Please try again.'; // fallback
    }
  } finally {
    loading.value = false;
  }
}
</script>