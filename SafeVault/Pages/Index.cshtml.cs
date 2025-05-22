using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SafeVault.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        VaultItems = new List<VaultItem>();
    }

    public List<VaultItem> VaultItems { get; set; }
    public void OnGet()
    {
        var options = new DbContextOptionsBuilder<VaultContext>()
                .UseSqlite("Data Source=safevault.db")
                .Options;
        using (var context = new VaultContext(options))
        {
            context.Database.EnsureCreated();
            VaultItems = context.VaultItems.ToList();
            foreach (var item in VaultItems)
            {
                item.Secret = EncryptionHelper.Decrypt(item.Secret);
            }
        }
    }
    public IActionResult OnPostAddVaultItem(string Title, string Secret)
    {
        if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Secret))
        {
            ModelState.AddModelError(string.Empty, "Title and Secret are required.");
            return Page();
        }
        if (!Validator.IsValidXSSInput(Title) || !Validator.IsValidXSSInput(Secret))
        {
            ModelState.AddModelError(string.Empty, "Possible XXS Input.");
            return Page();
        }

        AddVaultItem(new VaultItem
        {
            Title = Title,
            Secret = EncryptionHelper.Encrypt(Secret)
        });
        return RedirectToPage();
    }

    public void AddVaultItem(VaultItem item)
    {
        var options = new DbContextOptionsBuilder<VaultContext>()
                .UseSqlite("Data Source=safevault.db")
                .Options;
        using (var context = new VaultContext(options))
        {
            context.Database.EnsureCreated();
            context.VaultItems.Add(item);
            context.SaveChanges();
        }
    }
}
