namespace UNPHU_Vacantes.DTOs
{
    public class VacantDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Salary { get; set; }
        public required string Area { get; set; }  // Nueva propiedad
        public required string Modality { get; set; }
        public required bool Status { get; set; }
         public int CompanyId {get; set;}
         public string CompanyName { get; set; }
        
    }
}
