using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
         public string Role { get; set; } = string.Empty; // Admin, Employer, Applicant
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        
         public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }
}