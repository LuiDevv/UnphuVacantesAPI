using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class LoginRequestDTO
    {
        public string Symbol { get; set; }
        public string Password { get; set; }
    }

}