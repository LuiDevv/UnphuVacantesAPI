using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public bool IsApprovedByUNPHU { get; set; } = false;
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}
