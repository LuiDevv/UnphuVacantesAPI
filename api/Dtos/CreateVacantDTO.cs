using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateVacantDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Area is required.")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Modality is required.")]
        public string Modality { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public decimal Salary { get; set; }

        public string Description { get; set; } = ""; // Valor por defecto para Description

        public bool Status { get; set; } = true; // Siempre es true
    }


}