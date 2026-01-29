<template>
  <div class="min-h-screen content-center">
    <div class="max-w-sm mx-auto p-6">
      <component :is="isLogin ? LoginForm : RegisterForm" />
      <p class="mt-4 text-center text-text_secondary">
        <span v-if="isLogin">Nu aveti cont?</span>
        <span v-else>Aveti deja cont?</span>
        <button @click="isLogin = !isLogin" class="text-accent3 underline ml-2">
          {{ isLogin ? 'Inregistrare' : 'Conectare' }}
        </button>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import LoginForm from '@/components/Authentication/LoginForm.vue'
import RegisterForm from '../components/Authentication/RegisterForm.vue'

const isLogin = ref(true)
</script>
