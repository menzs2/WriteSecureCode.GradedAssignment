using System.Net.Http.Headers;
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
    }

    public string? ErrorMessage { get; set; }

    public void OnPost()
    {
        // Get username and email from the form
        var username = Request.Form["username"];
        var email = Request.Form["email"];
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
        {
            ErrorMessage = "Username and email are required.";
            return;
        }
        if(!Validator.IsValidXXSInput(username) || !Validator.IsValidXXSInput(email))
        {
            ErrorMessage = "Possible XXS Input.";
            return;
        }

        if (!Validator.IsValidUsername(username))
        {
            ErrorMessage = "Invalid username.";
            return;
        }
        if (!Validator.IsValidEmail(email))
        {
            ErrorMessage = "Invalid email address.";
            return;
        }
        var newUser = new User
        {
            Username = username,
            Email = email
        };
        // Save the new user to the database
        var options = new DbContextOptionsBuilder<VaultContext>()
            .UseSqlite("Data Source=safevault.db")
            .Options;
        using (var context = new VaultContext(options))
        {
            context.Database.EnsureCreated();
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}
