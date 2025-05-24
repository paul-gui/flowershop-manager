import { createApp } from 'vue'
import { createPinia } from "pinia";
import App from './App.vue'
import './styles.css'
import '@fortawesome/fontawesome-free/css/all.min.css';
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
