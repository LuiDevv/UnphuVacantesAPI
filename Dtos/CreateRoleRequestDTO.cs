using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateRoleRequest
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
        public required string Name { get; set; }
    }
}
