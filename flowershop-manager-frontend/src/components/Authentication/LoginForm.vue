<template>
  <form @submit.prevent="handleLogin" class="space-y-6 bg-cards p-8 rounded-lg shadow-md max-w-md w-full">
    <h2 class="text-2xl font-bold text-center text-text_primary">Conectare</h2>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Email</label>
      <input v-model="loginForm.email" type="text" class="w-full p-2 rounded-md border border-cards bg-divider focus:outline-none focus:ring-2 focus:ring-indigo-500 text-text_secondary"/>
      <p class="ms-2 text-xs text-red-500" v-if="validationErrors['email']">
        {{ validationErrors['email'] }}
      </p>
    </div>
    <div>
      <label class="block mb-1 font-medium text-text_secondary">Parolă</label>
      <input v-model="loginForm.password" type="password" class="w-full p-2 border rounded-md border-cards bg-divider focus:outline-none focus:ring-2 focus:ring-indigo-500 text-text_secondary"/>
      <p class="ms-2 text-xs text-red-500" v-if="validationErrors['password']">
        {{ validationErrors['password'] }}
      </p>
      <div class="flex justify-end mt-2">
        <span @click="goToForgotPassword" class="text-text_secondary underline cursor-pointer">
          Ai uitat parola?
        </span>
      </div>
    </div>
    <button type="submit" :disabled="loading" class="w-full bg-accent3 text-text_accents py-2 rounded-md hover:bg-[rgb(128,100,128)]">
      <!-- Show spinner when loading -->
      <i v-if="loading" class="fas fa-spinner fa-spin"></i>

      <!-- Show text only when not loading -->
      <span v-else>Conectare</span>
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from "@/stores/auth";
import { toast } from 'vue-sonner'

const validationErrors = ref<Record<string, string>>({})

const loading = ref(false);
const router = useRouter();
const auth = useAuthStore();

const loginForm = ref({
  email: '',
  password: '',
})

async function goToForgotPassword() {
  await router.push({ path: '/forgot-password' });
}

async function handleLogin() {
  validationErrors.value = {}

  if (loginForm.value.email === ''){
    validationErrors.value['email'] = 'Introduceți un email';
  }
  if (loginForm.value.password === ''){
    validationErrors.value['password'] = 'Introduceți o parolă';
  }
  if (Object.values(validationErrors.value).length > 0){
    return
  }

  try {
    loading.value = true
    await auth.login(loginForm.value)

    toast.success('V-ați conectat cu succes');
    await router.replace({ path: '/warehouses'});
  }
  catch (err: any) {
    toast.error('Conectare eșuată')
  } finally {
    loading.value = false;
  }
}
</script>