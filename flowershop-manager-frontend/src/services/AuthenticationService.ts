import api from "@/services/API";
import { LoginDto, RegisterDto } from "@/dtos/authDTOs"

const baseUrl = "Auth";

export async function register(registerDto: RegisterDto) {
    const res = await api.post(baseUrl + "/register", registerDto);
    return res.data;
}
export async function login(loginDto: LoginDto) {
    const res = await api.post(baseUrl + "/login", loginDto);
    return res.data;
}