using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateCompanyRequest
    {
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La descripción de la empresa es obligatoria.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El correo de contacto es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public required string ContactEmail { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public required string Location { get; set; }

        [Required(ErrorMessage = "El RNC es obligatorio.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El RNC debe contener exactamente 9 dígitos.")]
        public required string RNC { get; set; }
    }
}