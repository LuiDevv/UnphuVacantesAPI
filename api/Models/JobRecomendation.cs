using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api.Models;

[Table("JobRecommendations")]
public class JobRecommendation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int JobId { get; set; }
    public Job Job { get; set; } = null!;

    public DateTime RecommendedDate { get; set; } = DateTime.UtcNow;
}
