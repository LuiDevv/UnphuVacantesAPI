using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CreateCompanyRequestDTO
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

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "La aprobación de la UNPHU es obligatoria.")]
        public required bool IsApprovedByUNPHU { get; set; }

        [Required(ErrorMessage = "El simbolo de la empresa es oblgatorio.")]
        public required string Symbol { get; set; }
    }
}

