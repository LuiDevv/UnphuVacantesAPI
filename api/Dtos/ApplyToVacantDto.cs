using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class ApplyToVacantDto
    {
        [Required]
        public string CvUrl { get; set; }
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}