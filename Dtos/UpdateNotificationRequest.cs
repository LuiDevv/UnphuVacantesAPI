using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateNotificationRequest
    {
        public string Message { get; set; } = string.Empty;
        [Required(ErrorMessage = "El estado de la notificación es obligatorio.")]
        public bool IsRead { get; set; }
    }
}