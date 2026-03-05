import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'

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

// se monta la aplicación con Vuetify   
createApp(App).use(vuetify).mount('#app')
