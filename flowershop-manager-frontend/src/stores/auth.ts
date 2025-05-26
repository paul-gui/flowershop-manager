import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: null,
        roles: [],
        user: null
    }),
    actions: {
        setToken(token: string) {
            this.token = token;
        },
        setRoles(roles: string[]) {
            this.roles = roles;
        },
        logout() {
            this.token = null;
            this.roles = [];
            this.user = null;
        }
    },
    getters: {
        isAuthenticated: state => !!state.token,
        hasRole: (state) => (role: string) => state.roles?.includes(role),
    },
    persist: true,
});