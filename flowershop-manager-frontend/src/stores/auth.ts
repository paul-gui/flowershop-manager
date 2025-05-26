import { defineStore } from 'pinia';

type authState = {
    token: string | null;
    roles: string[];
}

export const useAuthStore = defineStore('auth', {
    state: () :authState => ({
        token: null,
        roles: [''],
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
        }
    },
    getters: {
        isAuthenticated: state => !!state.token,
        hasRole: (state) => (role: string) => state.roles?.includes(role),
    },
    persist: true,
});