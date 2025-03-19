using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateJobRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int JobTypeId { get; set; }
        public int JobCategoryId { get; set; }
        public int CompanyId { get; set; }
    }
}