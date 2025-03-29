using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class AppliedVacantDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public string CompanyName { get; set; } 
        public DateTime ApplicationDate { get; set; } 
        public string Status { get; set; }
    }
}
