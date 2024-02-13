using System.Security.Claims;
using Imc.Domain;
using Imc.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Imc;

public class LocalStorageAuthenticationStateProvider(IUserManagerService userManager) : AuthenticationStateProvider 
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userAccount = await userManager.GetCurrentUserAsync();
        return Create(userAccount);
    }
    
    public async Task NotifyUserAuthenticationAsync()
    {
        var userAccount = await userManager.GetCurrentUserAsync();
        var authState = Create(userAccount);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    private static AuthenticationState Create(User? userAccount)
    {
        if (userAccount is null) return new AuthenticationState(new ClaimsPrincipal());
        
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userAccount.Name),
            new Claim(ClaimTypes.Email, userAccount.Email),
            new Claim(ClaimTypes.Role, "UserCommon")
        }, "localStorageAuthentication");
            
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }
}