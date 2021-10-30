using System.Security.Claims;

namespace Likr.Client.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(x => x.Type.Equals("sub"))?.Value ?? "";
        }

        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            return user.FindFirst(x => x.Type == "DisplayName")?.Value!;
        }
    }
}