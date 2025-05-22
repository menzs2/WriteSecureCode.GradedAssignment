using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SafeVault;

public class User : IdentityUser
{
    
    [Key]
    public new int Id { get; set; } // Auto-incrementing primary key

    [Required]
    public required string Username { get; set; }

    
    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; } // Store hashed password
}
