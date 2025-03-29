using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class ApplicationResponseDTO
{
    public int Id { get; set; }
    public string ApplicantName { get; set; }
    public string CvUrl { get; set; }
    public DateTime ApplicationDate { get; set; }
}

}