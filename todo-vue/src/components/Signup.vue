<template>
  <div style="display: flex; justify-content: center; align-items: center; height: 100vh;">
    <div style="border: 1px solid #ccc; padding: 20px; border-radius: 8px; max-width: 400px; width: 100%;">
      <h2>Registro</h2>
      <form @submit.prevent="register">
        <div style="margin-bottom: 10px;">
          <label for="email">Email:</label>
          <input
            id="email"
            v-model="email"
            type="email"
            required
            style="width: 100%; padding: 8px; margin-top: 5px;"
          />
        </div>
        <div style="margin-bottom: 10px;">
          <label for="password">Contraseña:</label>
          <input
            id="password"
            v-model="password"
            type="password"
            required
            style="width: 100%; padding: 8px; margin-top: 5px;"
          />
        </div>
        <button type="submit" style="width: 100%; padding: 10px; background: #4caf50; color: white; border: none; border-radius: 4px;">Registrarse</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Signup',
  data() {
    return {
      email: '',
      password: ''
    }
  },
  methods: {
    async register() {
      try {
        const response = await fetch("http://localhost:5166/api/Auth/register?email=" + this.email + "&password=" + this.password, {
          method: "POST"
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText || "Error en el registro");
        }

        alert("Usuario registrado exitosamente ✅");
        this.$router.push('/login');
      } catch (error) {
        alert("Error: " + error.message);
      }
    }
  }
}
</script>