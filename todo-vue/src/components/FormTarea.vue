<template>
  <v-container>
    <!-- Tarjeta que contiene el formulario -->
    <v-card class="pa-4" elevation="2">
      <v-card-title>Crear nueva tarea</v-card-title>
      <v-card-text>
        <!-- Formulario con validaciones -->
        <v-form v-model="valid" ref="form">
          <!-- Campo Nombre -->
          <v-text-field
            v-model="tarea.nombre"
            label="Nombre"
            :rules="[rules.required, rules.max200]"
            required
          ></v-text-field>

          <!-- Campo Descripción -->
          <v-textarea
            v-model="tarea.descripcion"
            label="Descripción"
            :rules="[rules.max500]"
          ></v-textarea>

          <!-- Checkbox Completada -->
          <v-checkbox
            v-model="tarea.completada"
            label="Completada"
          ></v-checkbox>
        </v-form>
      </v-card-text>

      <!-- Botones de acción -->
      <v-card-actions>
        <v-btn color="primary" @click="crearTarea" :disabled="!valid">
          Guardar
        </v-btn>
        <v-btn color="secondary" @click="resetForm">
          Limpiar
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref } from "vue";

// Declaramos los eventos que este componente puede emitir
const emit = defineEmits(["agregar-tarea"]);

// Estado de validación del formulario
const valid = ref(false);
const form = ref(null);

// Objeto reactivo para la tarea
const tarea = ref({
  nombre: "",
  descripcion: "",
  completada: false,
});

// Reglas de validación
const rules = {
  required: (v) => !!v || "Campo obligatorio",
  max200: (v) => (v && v.length <= 200) || "Máximo 200 caracteres",
  max500: (v) => (!v || v.length <= 500) || "Máximo 500 caracteres",
};

// Función para crear tarea
async function crearTarea() {
  try {
    const response = await fetch("http://localhost:5166/api/tareas", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(tarea.value),
    });

    if (response.ok) {
      const nuevaTarea = await response.json();
      console.log("la neva tarea: ", nuevaTarea)
      alert("Tarea creada con éxito");
      resetForm();
      // Emitimos evento al padre para actualizar la lista
      // (App.vue escuchará este evento)
      emit("agregar-tarea", nuevaTarea);
    } else {
      alert("Error al crear la tarea");
    }
  } catch (error) {
    console.error(error);
    alert("Error de conexión con el servidor");
  }
}

// Función para limpiar el formulario
function resetForm() {
  tarea.value = {
    nombre: "",
    descripcion: "",
    completada: false,
  };
  form.value?.resetValidation();
}

</script>
