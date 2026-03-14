<template>
  <v-container class="d-flex justify-center align-center" style="height: 100vh;">
    <v-card class="pa-6" max-width="400">
      <v-card-title class="text-h5">Iniciar sesión</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="login">
          <!-- Campo de email -->
          <v-text-field
            v-model="email"
            label="Email"
            type="email"
            required
          />
          <!-- Campo de contraseña -->
          <v-text-field
            v-model="password"
            label="Contraseña"
            type="password"
            required
          />
          <!-- Botón de login -->
          <v-btn type="submit" color="primary" block>Login</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref } from "vue";

const email = ref("");
const password = ref("");

async function login() {
  try {
    // Usamos fetch para enviar la petición al backend
    const response = await fetch("http://localhost:5166/api/Auth/login?email=" + email.value + "&password=" + password.value, {
      method: "POST"
    });

    if (!response.ok) {
      throw new Error("Credenciales inválidas");
    }

    const data = await response.json();

    // Guardamos el token en localStorage
    localStorage.setItem("token", data.token);

    alert("Login exitoso ✅");
    // Redirigir al dashboard principal
    window.location.href = "/";
  } catch (error) {
    alert(error.message);
  }
}
</script>