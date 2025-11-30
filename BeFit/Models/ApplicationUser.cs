using Microsoft.AspNetCore.Identity;

namespace BeFit.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}