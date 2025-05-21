using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SafeVault;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace MyApp.Namespace
{
    public class AdminModel : PageModel
    {
        public string? ErrorMessage { get; set; }
        public void OnPost()
        {
            // Get username and email from the form
            var username = Request.Form["username"];
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                ErrorMessage = "Username and email are required.";
                return;
            }
            if (!Validator.IsValidXSSInput(username) || !Validator.IsValidXSSInput(email))
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
            if (!Validator.IsValidPassword(password))
            {
                ErrorMessage = "Invalid password.";
                return;
            }

            // Hash the password before storing it
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(null, password);

            var newUser = new User
            {
                Username = username,
                Email = email,
                Password = hashedPassword // Store the hashed password
            };
            SaveUserToDatabase(newUser);
        }

        private void SaveUserToDatabase(User user)
        {
            // Save the user to the database
            var options = new DbContextOptionsBuilder<VaultContext>()
                .UseSqlite("Data Source=safevault.db")
                .Options;
            using (var context = new VaultContext(options))
            {
                context.Database.EnsureCreated();
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
