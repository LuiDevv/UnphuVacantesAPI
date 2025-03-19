using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Notifications")]
public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Message { get; set; } = string.Empty;

    public DateTime SentDate { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; } = false;
}
