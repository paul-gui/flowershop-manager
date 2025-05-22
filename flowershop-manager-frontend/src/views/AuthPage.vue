<template>
  <div class="min-h-screen flex items-center justify-center">
    <div>
      <component :is="isLogin ? LoginForm : RegisterForm" />
      <p class="mt-4 text-center text-text_secondary">
        <span v-if="isLogin">Don't have an account?</span>
        <span v-else>Already have an account?</span>
        <button @click="isLogin = !isLogin" class="text-accent3 underline ml-2">
          {{ isLogin ? 'Register' : 'Login' }}
        </button>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import LoginForm from '../components/LoginForm.vue'
import RegisterForm from '../components/RegisterForm.vue'

const isLogin = ref(true)
</script>
