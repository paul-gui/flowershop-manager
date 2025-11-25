import api from "@/services/API";
import type { LoginRequest, RegisterAccountRequest } from '@/types/dtos/authentication/authenticationRequests.dto.ts'

const baseUrl = "Authentication";

export async function register(registerRequest: RegisterAccountRequest): Promise<void> {
    const res = await api.post(baseUrl + "/Register", registerRequest);
    return res.data;
}
export async function login(loginRequest: LoginRequest):Promise<void> {
    const res = await api.post(baseUrl + "/Login", loginRequest);
    return res.data;
}