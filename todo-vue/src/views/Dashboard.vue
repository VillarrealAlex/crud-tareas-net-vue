<template>
  <v-app>
    <v-main>
      <h1 class="text-center">Lista de Tareas</h1>
      <v-container>
        <!-- barra de ordenamiento y filtros -->
        <v-row class="mb-4 align-center">
          <v-col cols="4">
            <v-select v-model="filtro" :items="['Todas', 'Pendientes', 'Completadas']" label="Filtrar tareas" />
          </v-col>

          <v-col cols="4" class="text-right">
            <v-chip color="primary" dark>
               {{ mensajeContador }}
            </v-chip>
          </v-col>
        </v-row>

        <div>
          <!-- Botones de acción -->
          <v-row class="mb-4">
            <v-col cols="12" class="text-right">
              <v-btn color="warning" @click="clearAllData" class="mr-2">🗑️ Limpiar Datos</v-btn>
              <v-btn color="error" @click="logout">🚪 Cerrar Sesión</v-btn>
            </v-col>
          </v-row>

          <!-- pasar funciones y estados como props -->
          <FormTarea @agregar-tarea="agregarTarea" /> <!-- formulario de creacion de tareas -->
          <!-- lista de tareas -->
          <ListaTarea :tareas="tareasFiltradas" @eliminar-tarea="eliminarTarea" @editar="abrirModalEdicion" />

          <!-- Modal de edición como componente independiente -->
          <EditarTarea v-model="dialog" :tarea="tareaEdit" @tarea-editada="actualizarTarea" />

        </div>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import { ref, onMounted, computed } from 'vue'
import ListaTarea from '../components/ListaTarea.vue';
import FormTarea from '../components/FormTarea.vue';
import EditarTarea from '../components/EditarTarea.vue';

export default {
  name: 'Dashboard',
  components: {
    ListaTarea,
    FormTarea,
    EditarTarea
  },
  setup() {
    // estado para almacenar las tareas
    const tareas = ref([]);
    const filtro = ref('Todas'); // estado para el filtro de tareas

    const dialog = ref(false); // estado para controlar el modal de edición
    const tareaEdit = ref({}); // tarea seleccionada para editar

    // función para obtener las tareas
    async function obtenerTareas() {
      try {
        const response = await fetch('http://localhost:5166/api/tareas');
        if (response.ok) {
          tareas.value = await response.json();
        } else {
          alert('Error al obtener las tareas');
        }
      } catch (error) {
        console.error(error);
        alert('Error de conexión con el servidor');
      }
    }

    // funcion para agregar tarea
    function agregarTarea(nuevaTarea) {
      console.log("la nueva tarea desde Dashboard.vue: ", nuevaTarea);
      tareas.value.push(nuevaTarea);
    }

    // función para eliminar una tarea
    async function eliminarTarea(idTarea) {
      try {
        const response = await fetch(`http://localhost:5166/api/tareas/${idTarea}`,{
          method: "DELETE",
        });
        if (response.ok) {
          tareas.value = tareas.value.filter((t) => t.id !== idTarea);
          alert("Tarea eliminada con éxito");
        } else {
          alert("Error al eliminar la tarea");
        }
      } catch (error) {
        console.log("Error: ", error);
        alert("Error de conexion con el servidor");
      }
    }

    // función para abrir el modal de edición con la tarea seleccionada
    function abrirModalEdicion(tarea) {
      if (!tarea || typeof tarea !== 'object') {
        console.error('Tarea inválida para editar:', tarea);
        alert('Error: No se pudo abrir el modal de edición. Tarea inválida.');
        return;
      }
      tareaEdit.value = {...tarea}; // crear una copia para editar sin modificar la original
      dialog.value = true; // abrir el modal
    }

    // función para actualizar la tarea editada
    function actualizarTarea(tareaActualizada) {
      const index = tareas.value.findIndex(t => t.id === tareaActualizada.id);
      if (index !== -1) {
        tareas.value[index] = tareaActualizada; // actualizar la tarea en la lista
      }
    }

    // computed para aplicar filtros a las tareas
    const tareasFiltradas = computed(() => {
      let resultado = [...tareas.value];

      // aplicar filtro
      if (filtro.value === 'Pendientes') {
        resultado = resultado.filter(t => !t.completada);
      } else if (filtro.value === 'Completadas') {
        resultado = resultado.filter(t => t.completada);
      }

      return resultado;
    });

    // computed para contar tareas pendientes y completadas
    const pendientes = computed(() =>
      tareas.value.filter((t) => !t.completada).length
    );

    const completadas = computed(() =>
      tareas.value.filter((t) => t.completada).length
    );

    const total = computed(() => tareas.value.length);

    const mensajeContador = computed(() => {
      if (filtro.value === "Pendientes") {
        return `${pendientes.value} pendientes de ${total.value}`;
      } else if (filtro.value === "Completadas") {
        return `${completadas.value} completadas de ${total.value}`;
      } else {
        return `${total.value} tareas totales`;
      }
    });

    // cargar tareas al iniciar
    onMounted(async() =>{
      obtenerTareas();
    });

    return {
      tareas,
      filtro,
      dialog,
      tareaEdit,
      tareasFiltradas,
      mensajeContador,
      agregarTarea,
      eliminarTarea,
      abrirModalEdicion,
      actualizarTarea
    }
  },
  methods: {
    logout() {
      localStorage.removeItem('token')
      this.$router.push('/login')
    },
    clearAllData() {
      localStorage.clear()
      alert('Todos los datos del localStorage han sido eliminados')
      this.$router.push('/login')
    }
  }
}
</script>