using System.ComponentModel.DataAnnotations;

namespace SafeVault;

public class VaultItem
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public User? Owner { get; set; }
    public string? OwnerId { get; set; }
}
