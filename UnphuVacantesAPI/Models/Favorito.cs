using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnphuVacantesAPI.Models
{
    public class Favorito
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Postulante")]
        public int PostulanteId { get; set; }

        [Required, ForeignKey("Vacante")]
        public int VacanteId { get; set; }
    }
}
