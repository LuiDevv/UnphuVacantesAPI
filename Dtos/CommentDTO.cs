using System;

namespace api.Dtos
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }  // Usuario que hizo el comentario
        public Guid JobId { get; set; }   // Trabajo al que pertenece el comentario
        public string Content { get; set; } = string.Empty; // Contenido del comentario
        public DateTime CreatedAt { get; set; } // Fecha de creación
    }
}
