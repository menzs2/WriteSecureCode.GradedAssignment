using Xunit;

namespace SafeVault.Tests
{
    public class ValidatorTests
    {
        [Theory]
        [InlineData("user123", true)]
        [InlineData("ab", false)]
        [InlineData("user_name", false)]
        [InlineData("user123456789012345678", false)]
        public void IsValidUsername_WorksAsExpected(string username, bool expected)
        {
            Assert.Equal(expected, Validator.IsValidUsername(username));
        }

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("test.email@domain.co", true)]
        [InlineData("invalidemail@", false)]
        [InlineData("user@.com", false)]
        public void IsValidEmail_WorksAsExpected(string email, bool expected)
        {
            Assert.Equal(expected, Validator.IsValidEmail(email));
        }

        [Theory]
        [InlineData("Password1", true)]
        [InlineData("pass1234", true)]
        [InlineData("12345678", false)]
        [InlineData("password", false)]
        [InlineData("short1", false)]
        public void IsValidPassword_WorksAsExpected(string password, bool expected)
        {
            Assert.Equal(expected, Validator.IsValidPassword(password));
        }
    }
}