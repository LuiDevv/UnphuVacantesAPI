using System.ComponentModel.DataAnnotations;

namespace UnphuVacantesAPI.Models
{
    public class Postulante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string? Telefono { get; set; }

        public string? CVUrl { get; set; } // URL al archivo del CV
    }
}
