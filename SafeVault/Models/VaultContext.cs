using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SafeVault;

public class VaultContext : IdentityDbContext<User>
{
    public VaultContext(DbContextOptions<VaultContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<VaultItem> VaultItems { get; set; }
}
