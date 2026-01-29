import { defineStore } from 'pinia'
import type { LoginRequest } from '@/types/dtos/authentication/authenticationRequests.dto.ts'
import { login } from '@/services/AuthenticationService.ts'
import { jwtDecode } from 'jwt-decode'

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
        async login(form: LoginRequest) {
          const data = await login(form)
          this.token = data.token
          this.name = data.name
          this.setRolesFromToken(data.token)
        },
        setRolesFromToken(token: string) {
          const decoded : any = jwtDecode(token);
          const roles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          this.roles = Array.isArray(roles) ? roles : [roles];
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