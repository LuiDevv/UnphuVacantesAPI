using System.ComponentModel.DataAnnotations;

namespace UnphuVacantesAPI.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string RNC { get; set; }

        [Required]
        public string Direccion { get; set; }

        public string? SitioWeb { get; set; }
    }
}
