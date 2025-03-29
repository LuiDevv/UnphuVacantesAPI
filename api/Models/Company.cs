using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UNPHU_Vacantes.Models;

namespace api.Models;
[Table("Companies")]
public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } =  string.Empty;

    [Required, EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    [Required]
    public string RNC { get; set; } = string.Empty;

    public int? PortfolioCompanyId { get; set; }

    public string Password { get; set; } = string.Empty;

    // Nueva columna para 'Symbol'
    public string Symbol { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public bool IsApprovedByUNPHU { get; set; } = false;
    public ICollection<Vacant> Vacants { get; set; }
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    
}
