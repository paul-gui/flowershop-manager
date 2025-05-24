import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: null,
        roles: [],
        user: null
    }),
    actions: {
        setToken(token) {
            this.token = token;
        },
        setRoles(roles) {
            this.roles = roles;
        },
        logout() {
            this.token = null;
            this.roles = [];
            this.user = null;
        }
    }
});