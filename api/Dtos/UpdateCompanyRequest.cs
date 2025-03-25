using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace api.Dtos
{
    public class UpdateCompanyRequest
    {
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? ContactEmail { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        public string? Phone { get; set; }

        public string? Location { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "El RNC debe contener exactamente 9 dígitos.")]
        public string? RNC { get; set; }

        public IFormFile? ProfilePicture { get; set; } // Campo opcional
    }
}
