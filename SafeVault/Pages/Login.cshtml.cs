using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace SafeVault.Pages
{
    public class LoginModel : PageModel
    {
        private readonly VaultContext _context;

        public LoginModel(VaultContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == Username);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // Use PasswordHasher to verify the hashed password
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, Password);

            if (result != PasswordVerificationResult.Success)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // TODO: Set authentication cookie/session here

            return RedirectToPage("/Index");
        }
    }
}
