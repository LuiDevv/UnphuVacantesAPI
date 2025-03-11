using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models;
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}
