using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateJobApplicationRequest
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El ID del trabajo es obligatorio.")]
        public Guid JobId { get; set; }

        [Required(ErrorMessage = "Debe adjuntar un documento.")]
        public required string ResumeUrl { get; set; }
    }
}
