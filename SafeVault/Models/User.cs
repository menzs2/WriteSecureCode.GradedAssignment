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
}
