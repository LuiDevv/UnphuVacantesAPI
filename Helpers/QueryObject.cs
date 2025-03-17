using System.ComponentModel.DataAnnotations;

namespace api.Helpers
{
    public class QueryObject
    {
        
       public string? Name { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public bool? IsApprovedByUNPHU { get; set; }
    }
}
