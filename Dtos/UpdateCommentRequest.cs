using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El comentario no puede superar los 500 caracteres.")]
        public string Content { get; set; } = string.Empty;
    }
}