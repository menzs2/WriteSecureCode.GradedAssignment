using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeVault;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Namespace
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnPostAddUserAsync(string username, string email, string password, string role)
        {
            // Check if role exists, if not, create it
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                ErrorMessage = "Username and email are required.";
                return Page();
            }
            if (!Validator.IsValidXSSInput(username) || !Validator.IsValidXSSInput(email))
            {
                ErrorMessage = "Possible XXS Input.";
                return Page();
            }
            if (!Validator.IsValidUsername(username))
            {
                ErrorMessage = "Invalid username.";
                return Page();
            }
            if (!Validator.IsValidEmail(email))
            {
                ErrorMessage = "Invalid email address.";
                return Page();
            }
            if (!Validator.IsValidPassword(password))
            {
                ErrorMessage = "Invalid password.";
                return Page();
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                PasswordHash = password // Note: Password will be hashed by UserManager.CreateAsync
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                SuccessMessage = "User created and role assigned.";
            }
            else
            {
                ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
            }

            return Page();
        }
    }
}
