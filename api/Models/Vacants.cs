using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace UNPHU_Vacantes.Models
{
    [Table("Vacants")] 
    public class Vacant
    {
        // Atributos de la vacante
        public int Id { get; set; }  // Id único para cada vacante

        [Required]
        [StringLength(100)] // Título con una longitud máxima de 100 caracteres
        public string? Title { get; set; }

        [Required]
        [StringLength(500)] // Descripción con una longitud máxima de 500 caracteres
        public string? Description { get; set; }

        [Required]
        [StringLength(100)] // Salario con una longitud máxima de 100 caracteres
        public int Salary { get; set; }
        [Required]
        public required string Area { get; set; }  // Nueva propiedad
        public required string Modality { get; set; }

        [Required]
        [StringLength(50)] // Estado de la vacante, por ejemplo: Activa/Inactiva
        public bool Status { get; set; } = true;
        public int CompanyId { get; set; }

        public Company Company { get; set; } 
    

        public List<Application> Applications { get; set; } = new();
    }
}
