using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;

namespace api.Dtos
{
    public class JobApplicationDTO
    {
         public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid JobId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string CvUrl { get; set;}
        public NewUserDTO AppUser { get; set; } // Valores posibles: "Pending", "Accepted", "Rejected"
    }
}