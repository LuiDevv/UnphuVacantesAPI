using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class RegisterCompanyDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        [Required]
        public string RNC { get; set; } = string.Empty;

        [Required]
        public string Symbol { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
