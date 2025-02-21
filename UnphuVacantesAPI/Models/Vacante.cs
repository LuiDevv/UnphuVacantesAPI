using System;
using System.ComponentModel.DataAnnotations;

namespace UnphuVacantesAPI.Models
{
    public class Vacante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string TipoPosicion { get; set; } // Junior, Senior, etc.

        [Required]
        public string AreaTrabajo { get; set; } // Soporte técnico, Base de datos, etc.

        [Required]
        public string Modalidad { get; set; } // Presencial, Remoto

        public Empresa? Empresa { get; set; }

        public decimal? Salario { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

        public DateTime FechaExpiracion { get; set; } = DateTime.UtcNow.AddMonths(1);
    }
}
