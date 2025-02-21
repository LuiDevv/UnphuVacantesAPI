using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnphuVacantesAPI.Models
{
    public class Postulacion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("VacanteId")]
        public Vacante Vacante { get; set; }  // Relación con Vacante

        [ForeignKey("PostulanteId")]
        public Postulante? Postulante { get; set; }  // Relación con Postulante

        [Required]
        public int PostulanteId { get; set; }  // Clave foránea de Postulante

        [Required]
        public int VacanteId { get; set; }  // Clave foránea de Vacante

        public DateTime FechaPostulacion { get; set; } = DateTime.UtcNow; // Fecha normal
    }
}
