import { createRouter, createWebHistory } from 'vue-router'

import Warehouses from "@/components/Warehouses/Warehouses.vue";
import AuthPage from "@/views/AuthPage.vue";
import WarehouseDetails from "@/components/Warehouses/WarehouseDetails.vue";
import {useAuthStore} from "@/stores/auth";

const routes = [
    {
        path: '/warehouses',
        component: Warehouses,
        meta: { requiresAuth: true },
    },
    {
        path: '/warehouse-details',
        component: WarehouseDetails,
        meta: { requiresAuth: true, role: 'Admin' },
    },
    {
        path: '/',
        component: AuthPage,
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

router.beforeEach((to, from, next) => {
    const auth = useAuthStore()

    const isAuthenticated = auth.isAuthenticated
    if (to.meta.requiresAuth && !isAuthenticated) {
        return next('/')
    }

    const hasRole = auth.hasRole(to.meta.role)
    if (to.meta.role && !hasRole) {
        return next('/unauthorized')
    }

    next()
})

export default router