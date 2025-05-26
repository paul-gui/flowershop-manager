import { createRouter, createWebHistory } from 'vue-router'
import AuthPage from '@/views/AuthPage.vue'
import Warehouses from '@/components/Warehouses/Warehouses.vue'
import { useAuthStore } from '@/stores/auth.ts'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/auth',
      name: 'auth',
      component: AuthPage,
    },
    {
      path: '/warehouses',
      name: 'warehouses',
      component: Warehouses,
      meta: { requiresAuth: true },
    },
    {
      path: '/warehouse-details',
      name: 'warehouseDetails',
      component: () => import('../components/Warehouses/WarehouseDetails.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
  ],
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  const requiresAuth = to.meta.requiresAuth;
  const requiredRoles = to.meta.roles as string[] | null;

  if (requiresAuth && !authStore.isAuthenticated) {
    return next({ path: '/auth' })
  }

  if (requiredRoles && !requiredRoles.every(role => authStore.roles.includes(role))){
    return next({ path: '/unauthorized' })
  }

  return next()
})

export default router
