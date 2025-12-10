import axios from "axios";
import { useAuthStore } from '@/stores/auth.ts'

const api = axios.create({
    baseURL: "http://localhost:5116/api",
    withCredentials: true,
})

api.interceptors.request.use(config => {
    const auth = useAuthStore()
    const token = auth.token;
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api