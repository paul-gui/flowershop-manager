import { createRouter, createWebHistory } from 'vue-router'

import Locations from "@/components/Locations.vue";
import LocationEdit from "@/components/LocationEdit.vue";
import AuthPage from "@/views/AuthPage.vue";

const routes = [
    {
        path: '/locations',
        component: Locations,
    },
    {
        path: '/location-edit',
        component: LocationEdit,
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

export default router