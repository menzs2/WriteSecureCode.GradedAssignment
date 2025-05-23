using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeVault;

public class VaultItem
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Secret { get; set; }

    // Foreign key to User
    [Required]
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
