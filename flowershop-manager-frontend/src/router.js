import { createRouter, createWebHistory } from 'vue-router'

import Locations from "@/components/Locations.vue";
import LocationEdit from "@/components/LocationEdit.vue";

const routes = [
    {
        path: '/',
        component: Locations,
    },
    {
        path: '/location-edit',
        component: LocationEdit,
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router