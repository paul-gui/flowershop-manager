import { createApp } from 'vue'
import { createPinia } from "pinia";
import piniaPersistedState from 'pinia-plugin-persistedstate';
import App from './App.vue'
import './styles.css'
import '@fortawesome/fontawesome-free/css/all.min.css';
import router from './router'

const app = createApp(App)

const pinia = createPinia()
pinia.use(piniaPersistedState)

app.use(pinia)
app.use(router)

app.mount('#app')
