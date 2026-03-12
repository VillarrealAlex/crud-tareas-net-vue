<template>
  <v-container>
    <!-- Tarjeta que contiene la lista de tareas -->
    <v-card class="pa-4" elevation="2">
      <v-card-title>Lista de Tareas</v-card-title>
      <v-card-text>
        <!-- Lista de tareas -->
        <v-list>
          <v-list-item
            v-for="tarea in tareas"
            :key="tarea.id"
            class="d-flex justify-space-between align-center"
          >
            <!-- Nombre y descripción -->
            <div>
              <v-list-item-title>{{ tarea.nombre }}</v-list-item-title>
              <v-list-item-subtitle>{{ tarea.descripcion }}</v-list-item-subtitle>
            </div>

            <!-- Estado de completada -->
            <v-chip :color="tarea.completada ? 'green' : 'grey'" dark>
              {{ tarea.completada ? 'Completada' : 'Pendiente' }}
            </v-chip>

            <!-- Botones de acción -->
            <div>
              <v-btn color="primary" @click="editar(tarea)">Editar</v-btn>
              <v-btn color="error" @click="eliminarTarea(tarea.id)">Eliminar</v-btn>
            </div>
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>

const props = defineProps({
  tareas: { type: Array, required: true }
});

const emit = defineEmits(["eliminar-tarea", "editar"]);

// función para eliminar tarea (recibe el id de la tarea a eliminar y emite evento al padre) 
function eliminarTarea(idTarea) {
  emit("eliminar-tarea", idTarea);
}

// Función para editar una tarea (por ahora solo muestra un mensaje)
function editar(tarea) {
  emit("editar", tarea);
}

</script>
