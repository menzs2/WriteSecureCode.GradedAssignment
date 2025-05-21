using System.Text.RegularExpressions;

namespace SafeVault;

public class Validator
{
    public static bool IsUserValid(string username, string email, string password)
    {
        return IsValidUsername(username) && IsValidEmail(email) && IsValidPassword(password);
    }

    public static bool IsValidUsername(string username)
    {
        // Username must be alphanumeric and between 3 and 20 characters
        var usernameRegex = new Regex(@"^[a-zA-Z0-9]{3,20}$");
        return usernameRegex.IsMatch(username);
    }
    public static bool IsValidEmail(string email)
    {
        // Simple regex for email validation
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

    public static bool IsValidPassword(string password)
    {
        // Password must be at least 8 characters long and contain at least one number and one letter
        var passwordRegex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
        return passwordRegex.IsMatch(password);
    }

    public static bool IsValidXSSInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return true;
        }
        if (input.Contains("<script>") || input.Contains("</script>"))
        {
            return false;
        }
        if(input.Contains("<iframe") || input.Contains("</iframe>"))
        {
            return false;
        }
        return true;
    }
}
