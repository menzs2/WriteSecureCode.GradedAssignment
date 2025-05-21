using System.ComponentModel.DataAnnotations;

namespace SafeVault;

public class User
{
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
