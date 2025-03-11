using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El comentario no puede superar los 500 caracteres.")]
        public required string Content { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El ID del trabajo es obligatorio.")]
        public Guid JobId { get; set; }
    }

}
