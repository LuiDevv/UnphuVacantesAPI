using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class VacantUpdateDTO
    {
        public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Salary { get; set; }
    public string Area { get; set; }
    public string Modality { get; set; }
    public bool Status { get; set; }
    }
}