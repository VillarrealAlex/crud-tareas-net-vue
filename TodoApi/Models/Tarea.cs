namespace TodoApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

// Modelo de datos para una tarea
public class Tarea
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Nombre { get; set; }
        public bool Completada { get; set; } = false;

        // columnas nuevas para la migración
        [AllowNull]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; set; }
    }
