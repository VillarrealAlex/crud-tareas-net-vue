using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    // DbContext: clase que representa la conexión con la base de datos
    public class TodoContext : DbContext
    {
        // Constructor que recibe las opciones de configuración
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        // DbSet: representa la tabla "Tareas" en la base de datos
        public DbSet<Tarea> Tareas { get; set; }
    }
}