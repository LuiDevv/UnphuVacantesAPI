using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("ApplicationDocuments")]
public class ApplicationDocument
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int JobApplicationId { get; set; }
    public JobApplication? JobApplication { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
}
