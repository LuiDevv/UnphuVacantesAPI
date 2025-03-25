using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinShark.Dtos
{
    public class GetCompanyDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string RNC { get; set; }
        public string Symbol { get; set; }
        public bool IsApprovedByUNPHU { get; set; }
    }
}