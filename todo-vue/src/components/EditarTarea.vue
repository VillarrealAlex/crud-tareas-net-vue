<template>
  <!-- Modal de edición -->
  <v-dialog v-model="dialog" max-width="500">
    <v-card>
      <v-card-title>Editar tarea</v-card-title>
      <v-card-text>
        <v-form ref="editForm" v-model="valid">
          <!-- Campo Nombre -->
          <v-text-field
            v-model="tareaLocal.nombre"
            label="Nombre"
            :rules="[rules.required, rules.max200]"
            required
          />
          <!-- Campo Descripción -->
          <v-textarea
            v-model="tareaLocal.descripcion"
            label="Descripción"
            
          />
          <!-- Checkbox Completada -->
          <v-checkbox
            v-model="tareaLocal.completada"
            label="Completada"
          />
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="guardar">Guardar</v-btn>
        <v-btn color="secondary" @click="cerrar">Cancelar</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import {ref, watch} from 'vue';

//props el padre le pasa la tarea a editar y el estado del modal
const props = defineProps({
  modelValue: {
    type: Boolean,
    required: true
  }, // controla modal abierto/cerrado
  tarea: {
    type: Object,
    required: true
  } // tarea seleccionada para editar
});

// emit para enviar eventos al padre
const emit = defineEmits(['update:modelValue', 'tarea-editada']);

const dialog = ref(props.modelValue); // estado del modal
const valid = ref(false); // validación del formulario

// copia local de la tarea para editar sin modificar la original hasta guardar
const tareaLocal = ref({...props.tarea});

// reglas de validación
const rules = {
  required: (v) => (!!v ? true : "Campo obligatorio"),
  max200: v => (v && v.length <= 200) || 'Máximo 200 caracteres.',
}

// sincronizar apertura/cierre del modal con el padre
watch(() => props.modelValue, (newVal) => {
  dialog.value = newVal;
  if (newVal) {
    // cuando se abre el modal, cargar la tarea a editar
    tareaLocal.value = {...props.tarea};
  }
});
// watch(() => dialog.value, (newVal) => {
//   emit('update:modelValue', newVal);
// });

function cerrar() {
  // dialog.value = false;
  emit('update:modelValue', false); // informar al padre que se cerró el modal
} 

async function guardar() {
  try {
    const response = await fetch(`http://localhost:5166/api/tareas/${tareaLocal.value.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(tareaLocal.value)
    });

    if (response.ok) {
      // tarea actualizada correctamente, y notifcar al padre
      emit('tarea-editada', tareaLocal.value);
      cerrar();
    } else {
      alert('Error al actualizar la tarea');
    }

  } catch (error) {
    console.error('Error al actualizar la tarea:', error);
    alert('Error al actualizar la tarea');
  }

}

</script>