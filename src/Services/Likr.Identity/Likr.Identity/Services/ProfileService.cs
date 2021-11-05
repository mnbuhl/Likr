using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Likr.Identity.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace Likr.Identity.Server.Services
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> UserManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim("DisplayName", user.DisplayName)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);
        
            context.IsActive = (user != null);
        }
    }
}