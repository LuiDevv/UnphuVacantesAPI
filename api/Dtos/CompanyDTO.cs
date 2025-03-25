using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CompanyDTO
    {
         public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string RNC { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture {get; set;} = string.Empty;
        public bool IsApprovedByUNPHU { get; set; }


    }
}