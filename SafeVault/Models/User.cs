using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SafeVault;

public class User : IdentityUser
{
    // Inherits from IdentityUser to use built-in identity features
    [Key]
    public int Id { get; set; } // Auto-incrementing primary key

    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } // Store hashed password
}
