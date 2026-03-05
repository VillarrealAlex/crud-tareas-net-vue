# TodoApp

Aplicación de tareas construida con **.NET 8**, **EF Core** y **SQLite**.  
Permite crear, listar, actualizar y eliminar tareas, con persistencia en `todo.db`.

---

## Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/usuario/TodoApp.git
   cd TodoApp
   ```

2. **Restaurar dependencias:**
   ```bash
   dotnet restore
   ```

3. **Configurar cadena de conexión en `appsettings.json`:**
   ```json
   "ConnectionStrings": {
     "TodoConnection": "Data Source=Data/todo.db"
   }
   ```

4. **Generar migración inicial:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Ejemplo para cambios posteriores:**
   ```bash
   dotnet ef migrations add AddExtraColumnsToTareas
   dotnet ef database update
   ```