using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNPHU_Vacantes.Models;


namespace api.Models
{
    public class SavedVacant
    {
         public int Id { get; set; }
        public string AppUserId { get; set; } 
        public int VacantId { get; set; } 

        public AppUser AppUser { get; set; } 
        public Vacant Vacant { get; set; }
        }
}