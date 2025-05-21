using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            // NOTE: For real apps, store password hashes and compare securely!
            if (user == null  || user.Password != Password )
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // TODO: Set authentication cookie/session here

            return RedirectToPage("/Index");
        }
    }
}
