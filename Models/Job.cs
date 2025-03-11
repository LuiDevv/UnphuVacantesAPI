using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models;
public class Job
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Requirements { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

    [Required]
    public int JobTypeId { get; set; }
    public JobType JobType { get; set; } = null!;

    [Required]
    public int JobCategoryId { get; set; }
    public JobCategory JobCategory { get; set; } = null!;

    public DateTime PostedDate { get; set; } = DateTime.UtcNow;

    public DateTime ExpirationDate { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}
