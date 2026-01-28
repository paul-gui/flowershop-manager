import { defineStore } from 'pinia';

type authState = {
    token: string | null;
    roles: string[];
    name: string | null;
}

export const useAuthStore = defineStore('auth', {
    state: () :authState => ({
        token: null,
        roles: [''],
        name: null
    }),
    actions: {
        setToken(token: string) {
            this.token = token;
        },
        setRoles(roles: string[]) {
            this.roles = roles;
        },
        setName(name: string) {
          this.name = name;
        },
        logout() {
            this.token = null;
            this.roles = [];
            this.$reset()
            localStorage.removeItem('auth')
        }
    },
    getters: {
        isAuthenticated: state => !!state.token,
        hasRole: (state) => (role: string) => state.roles?.includes(role),
    },
    persist: true,
});