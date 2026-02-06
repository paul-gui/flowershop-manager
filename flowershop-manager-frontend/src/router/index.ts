import { createRouter, createWebHistory } from 'vue-router'
import AuthPage from '@/views/Authentication/AuthPage.vue'
import WarehousesPage from '@/views/Warehouses/WarehousesPage.vue'
import { useAuthStore } from '@/stores/auth.ts'



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'auth',
      component: AuthPage,
      meta: { guestOnly: true },
    },
    {
      path: '/warehouses',
      name: 'Warehouses',
      component: WarehousesPage,
      meta: { requiresAuth: true },
    },
    {
      path: '/warehouse-details/:id',
      name: 'warehouseDetails',
      component: () => import('@/views/Warehouses/WarehouseDetailsPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/warehouse-details/:id/add-product',
      name: 'warehouseAddProduct',
      component: () => import('@/views/Products/ProductCreatePage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/warehouse-add',
      name: 'wareHouseAdd',
      component: () => import('@/views/Warehouses/WarehouseAddPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/product-details/:id',
      name: 'productDetails',
      component: () => import('@/views/Products/ProductEditPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
      props: true
    },
    {
      path: '/sales/createSale/:warehouseId',
      name: 'CreateSale',
      component: () => import('@/views/Sales/CreateSalePage.vue'),
      meta: { requiresAuth: true },
    },
    {
      path: '/sales/history',
      name: 'SalesHistory',
      component: () => import('@/views/Sales/HistoryPage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/sales/history/createSale/:saleDate',
      name: 'HistoryCreateSale',
      component: () => import('@/views/Sales/HistoryCreateSalePage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/sales/history/editSale/:id',
      name: 'HistoryEditSale',
      component: () => import('@/views/Sales/HistoryEditSalePage.vue'),
      meta: { requiresAuth: true, roles: ['Admin'] },
    },
    {
      path: '/unauthorized',
      name: 'Unauthorized',
      component: () => import('../views/Common/UnauthorizedPage.vue')
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: () => import('../views/Common/NotFoundPage.vue')
    }
  ],
})

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore()

  const guestOnly = to.meta.guestOnly;
  const requiresAuth = to.meta.requiresAuth;
  const requiredRoles = to.meta.roles as string[] | null;

  if (guestOnly && authStore.isAuthenticated) {
    return next('/warehouses')
  }

  if (requiresAuth && !authStore.isAuthenticated) {
    return next({ path: '/' })
  }

  if (requiredRoles && !requiredRoles.every(role => authStore.roles.includes(role))) {
    return next({ path: '/unauthorized' })
  }

  return next()
})

export default router
