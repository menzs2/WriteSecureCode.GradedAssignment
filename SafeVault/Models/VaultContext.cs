

using Microsoft.EntityFrameworkCore;

namespace SafeVault;

public class VaultContext : DbContext
{
    public VaultContext(DbContextOptions<VaultContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
