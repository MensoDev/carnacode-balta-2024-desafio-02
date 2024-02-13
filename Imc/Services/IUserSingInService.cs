using Microsoft.AspNetCore.Components.Authorization;

namespace Imc.Services;

public interface IUserSingInService
{
    Task<bool> SignInAsync(string email, string password);
    Task SignOutAsync();
    Task<bool> IsSignedInAsync();
}

public class UserSingInService(
    IUserManagerService userManager, 
    IHashService hashService, 
    AuthenticationStateProvider authenticationStateProvider) : IUserSingInService
{
    public async Task<bool> SignInAsync(string email, string password)
    {
        var user = await userManager.GetUserAsync(email);
        if (user is null) { return false; }
        
        var isVerified = hashService.Verify(user.PasswordHash, password, user.PasswordSalt);
        
        if (!isVerified) { return false; }
        
        await userManager.SetCurrentUserAsync(user);
        await ((LocalStorageAuthenticationStateProvider)authenticationStateProvider).NotifyUserAuthenticationAsync();
        return true;
    }

    public async Task SignOutAsync()
    {
        await userManager.RemoveCurrentUserAsync();
        await ((LocalStorageAuthenticationStateProvider)authenticationStateProvider).NotifyUserAuthenticationAsync();
    }

    public async Task<bool> IsSignedInAsync()
    {
        return await userManager.GetCurrentUserAsync() is not null;
    }
}