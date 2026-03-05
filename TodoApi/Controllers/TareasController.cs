using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    // indica que es un controlador de API 
    [ApiController]
    // define la rta base para las solicitudes a este controlador
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
         private readonly TodoContext _context;

        // Inyectamos el contexto en el controlador
        public TareasController(TodoContext context)
        {
            _context = context;
        }

        // GET /api/tareas → devuelve todas las tareas
        [HttpGet]
        public async Task<IEnumerable<Tarea>> Get() =>
            await _context.Tareas.ToListAsync();

         // GET: api/Tareas/id → devuelve una tarea por su id
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();
            return tarea;
        }

        // POST /api/tareas → crea una nueva tarea
        [HttpPost]
        public async Task<IActionResult> Post(Tarea tarea)
        {
            _context.Tareas.Add(tarea);          // Agregamos la tarea a la BD
            await _context.SaveChangesAsync();   // Guardamos cambios
            return Ok(tarea);                    // Devolvemos la tarea creada
        }

        // PUT /api/tareas/{id} → actualiza una tarea existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tarea tarea)
        {
            var existente = await _context.Tareas.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = tarea.Nombre;
            existente.Completada = tarea.Completada;
            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // DELETE /api/tareas/{id} → elimina una tarea
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}