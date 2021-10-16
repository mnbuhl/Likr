using Microsoft.AspNetCore.Identity;

namespace Likr.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
    }
}