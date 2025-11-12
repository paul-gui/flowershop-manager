import api from "@/services/API";
import type { LoginRequest, RegisterRequest } from '@/types/dtos/authentication/authentication.dto.ts'

const baseUrl = "Auth";

export async function register(registerRequest: RegisterRequest) {
    const res = await api.post(baseUrl + "/register", registerRequest);
    return res.data;
}
export async function login(loginRequest: LoginRequest) {
    const res = await api.post(baseUrl + "/login", loginRequest);
    return res.data;
}