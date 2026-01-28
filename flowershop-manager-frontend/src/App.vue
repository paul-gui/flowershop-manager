<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { Toaster } from 'vue-sonner'
import NavBar from '@/components/Common/NavBar.vue'
</script>

<template>
  <div class="h-screen flex flex-col">
    <NavBar/>
    <main class="flex-1">
      <router-view />
      <Toaster/>
    </main>
  </div>
</template>

<style scoped>

</style>
