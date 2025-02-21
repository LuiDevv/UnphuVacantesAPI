using System;
using System.ComponentModel.DataAnnotations;

namespace UnphuVacantesAPI.Models
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string Mensaje { get; set; }

        public bool Leida { get; set; } = false;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
