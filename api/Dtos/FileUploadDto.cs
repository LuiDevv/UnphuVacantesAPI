using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }
}