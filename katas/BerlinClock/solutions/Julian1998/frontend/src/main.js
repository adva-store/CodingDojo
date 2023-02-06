import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import 'bootstrap/dist/css/bootstrap.min.css'

const app = createApp(App)
app.use(VueAxios, axios)
app.provide('axios', app.config.globalProperties.axios)  // provide 'axios'
app.mount('#app')