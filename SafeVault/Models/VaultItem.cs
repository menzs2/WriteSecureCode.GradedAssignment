using System.ComponentModel.DataAnnotations;

namespace SafeVault;

public class VaultItem
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Secret { get; set; }
    public User? Owner { get; set; }
    public int? OwnerId { get; set; }
}
