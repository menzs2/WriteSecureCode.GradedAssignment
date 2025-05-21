using System.ComponentModel.DataAnnotations;

namespace SafeVault;

public class User
{
    public string Id { get; set; }
    [Required]
    public string Username { get; set; } // Fixed typo
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
