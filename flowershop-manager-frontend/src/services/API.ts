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

api.interceptors.response.use(
  response => response, // just pass successful responses
  (err: unknown) => {
    const authStore = useAuthStore();

    if (axios.isAxiosError(err) && err.response?.status === 401) {
      authStore.logout();
    }

    return Promise.reject(err);
  }
);

export default api