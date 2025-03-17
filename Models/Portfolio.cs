using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models;

[Table("Portfolios")]
public class Portfolio

{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? AppUserId { get; set; }  // Relación con el usuario
    public AppUser? AppUser { get; set; }   // Usuario propietario
    public Company? Company { get; set; } 
    public int CompanyId { get; set; } // Relación con la compañía

    public List<Company> Companies { get; set; } = new List<Company>(); // Relación con stocks
}
