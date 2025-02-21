using System.ComponentModel.DataAnnotations;

namespace UnphuVacantesAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContraseñaHash { get; set; }

        [Required]
        public string Rol { get; set; } // Admin, Empleador, Postulante
    }
}
