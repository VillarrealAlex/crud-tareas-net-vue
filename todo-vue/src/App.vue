<script setup>
import {ref, onMounted} from 'vue'
import ListaTarea from './components/ListaTarea.vue';
import FormTarea from './components/FormTarea.vue';

// estado para almacenar las tareas
const tareas = ref([]);

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

// funcion pra agrear tarea(recibe evento de FormTarea)
function agregarTarea(nuevaTarea) {
  console.log("la nueva tarea desde App.vue: ", nuevaTarea);
  
  tareas.value.push(nuevaTarea);
}

// función para eliminar una tarea(recibe el veneto de ListaTarea)
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

// cargar tareas al iniciar la app 
onMounted(async() =>{
  obtenerTareas();
})

</script>

<template>
 <div>
  <h1>Lista de Tareas</h1>
  <!-- pasar funciones y estados como props -->
  <FormTarea @agregar-tarea="agregarTarea" />
  <ListaTarea 
    :tareas="tareas" 
    
    @eliminar-tarea="eliminarTarea" 
    />
 </div>

</template>

<!-- <style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style> -->
