using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Likr.Client.Services;

public class AuthService
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    public async Task<ClaimsPrincipal?> GetCurrentUser()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();

        return authState?.User;
    }
}