using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateNotificationRequest
    {
       [Required(ErrorMessage = "El mensaje de la notificación es obligatorio.")]
        public required string Message { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public Guid UserId { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
