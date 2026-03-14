import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'
import router from './router/index' // se importa el router

// se importa Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// se crea la instancia de Vuetify
const vuetify = createVuetify({
  components,
  directives,
})

const app = createApp(App)
app.use(router) // se usa el router
app.use(vuetify) // se usa Vuetify

// se monta la aplicación
app.mount('#app')
