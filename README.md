# TodoApp Monorepo

Este repositorio contiene dos proyectos relacionados:

- **Backend** (`TodoApi/`): una API REST creada con **.NET 8**, **Entity Framework Core** y **SQLite**. Proporciona operaciones CRUD para gestionar tareas.
- **Frontend** (`todo-vue/`): una aplicación de **Vue 3** construida con **Vite** que consume la API del backend para mostrar y manipular las tareas.

Cada carpeta incluye su propio README con instrucciones específicas de instalación y ejecución:

- Consulta `TodoApi/readme.md` para configurar y ejecutar el servidor .NET.
- Consulta `todo-vue/README.md` para levantar el cliente Vue.

Puedes desarrollar y ejecutar ambos proyectos de forma independiente; normalmente el backend se expone en `http://localhost:5000` (o el puerto configurado) y el frontend en `http://localhost:5173`.

---

Para obtener más detalles sobre despliegue, migraciones de base de datos o configuración adicional, revisa los documentos dentro de cada subproyecto.