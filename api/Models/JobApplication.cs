// Models/JobApplication.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UNPHU_Vacantes.Models;

namespace api.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VacantId { get; set; }

        [ForeignKey("VacantId")]
        public Vacant Vacant { get; set; } // Propiedad de navegación

        [Required]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; } // Propiedad de navegación

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        // Nueva propiedad para la URL del CV
        [Required]
        public string CvUrl { get; set; }
    }
}
