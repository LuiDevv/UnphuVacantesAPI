// Models/Application.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UNPHU_Vacantes.Models;

namespace api.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VacantId { get; set; }

        [ForeignKey("VacantId")]
        public Vacant Vacant { get; set; }

        [Required]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string CvUrl { get; set; }
    }
}
