import { createRouter, createWebHistory } from 'vue-router'
import AuthPage from '@/views/AuthPage.vue'
import Warehouses from '@/components/Warehouses/Warehouses.vue'
import { useAuthStore } from '@/stores/auth.ts'



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
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
      path: '/warehouse-details/:id',
      name: 'warehouseDetails',
      component: () => import('../components/Warehouses/WarehouseDetails.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/warehouse-details/:id/add-product',
      name: 'warehouseAddProduct',
      component: () => import('../components/Products/ProductCreatePage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/warehouse-add',
      name: 'wareHouseAdd',
      component: () => import('../components/Warehouses/WarehouseAdd.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/product-details/:id',
      name: 'productDetails',
      component: () => import('../components/Products/ProductEditPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/sales/createFromWarehouse/:warehouseId',
      name: 'CreateSaleFromWarehouse',
      component: () => import('../components/Sales/CreateSalePage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/sales/history',
      name: 'salesHistory',
      component: () => import('../components/Sales/HistoryPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    }
  ],
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  const requiresAuth = to.meta.requiresAuth;
  const requiredRoles = to.meta.roles as string[] | null;

  if (requiresAuth && !authStore.isAuthenticated) {
    return next({ path: '/' })
  }

  if (requiredRoles && !requiredRoles.every(role => authStore.roles.includes(role))){
    return next({ path: '/unauthorized' })
  }

  return next()
})

export default router
