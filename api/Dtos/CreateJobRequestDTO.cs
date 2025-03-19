using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
     public class CreateJobRequestDTO
    {
        [Required(ErrorMessage = "El título del trabajo es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "La descripción del trabajo es obligatoria.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El ID de la empresa es obligatorio.")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "El salario mínimo es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario mínimo debe ser un número positivo.")]
        public decimal MinSalary { get; set; }

        [Required(ErrorMessage = "El salario máximo es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario máximo debe ser un número positivo.")]
        public decimal MaxSalary { get; set; }
    }
}