using Microsoft.AspNetCore.Identity;

namespace Likr.Identity.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}