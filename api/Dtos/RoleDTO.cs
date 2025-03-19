using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}