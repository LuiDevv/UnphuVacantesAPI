using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("JobApplications")]  // Table name
public class JobApplication
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int JobId { get; set; }
    public Job Job { get; set; } =  null!;

    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string Status { get; set; } = string.Empty; // Pending, Accepted, Rejected
}
