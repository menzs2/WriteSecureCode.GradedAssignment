using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SafeVault;

public class User : IdentityUser
{
    
    public required string UserName { get; set; }
}
