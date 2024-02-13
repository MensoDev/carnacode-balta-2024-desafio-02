using Blazored.LocalStorage;
using Imc.Domain;
using Imc.Models;

namespace Imc.Services;

public interface IUserManagerService
{
    Task<bool> RegisterUser(CreateUserAccountCommand command);
    Task<User?> GetCurrentUserAsync();
    Task<User?> GetUserAsync(string email);
    Task SetCurrentUserAsync(User user);
    Task RemoveCurrentUserAsync();
}

public class UserManagerService(ILocalStorageService localStorage, IHashService hashService) : IUserManagerService
{
    private List<User> _users = [];
    private const string UsersKey = "users";
    private const string CurrentUserKey = "currentUser";
    
    public async Task<bool> RegisterUser(CreateUserAccountCommand command)
    {
        await ReadUsersAsync();
        var passwordHash = hashService.CreateHash(command.Password!);
        var user = new User(command.Name!, command.Email!, passwordHash.hash, passwordHash.salt);
        _users.Add(user);
        await WriteUsersAsync();
        return true;
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var user = await localStorage.GetItemAsync<User>(CurrentUserKey);
        return user;
    }

    public async Task<User?> GetUserAsync(string email)
    {
        await ReadUsersAsync();
        return _users.Find(user => user.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task SetCurrentUserAsync(User user)
    {
        await localStorage.SetItemAsync(CurrentUserKey, user);
    }

    public async Task RemoveCurrentUserAsync()
    {
        await localStorage.RemoveItemAsync(CurrentUserKey);
    }

    private async Task WriteUsersAsync()
    {
        await localStorage.SetItemAsync(UsersKey, _users);
    }
    
    private async Task ReadUsersAsync()
    {
        _users = await localStorage.GetItemAsync<List<User>>(UsersKey) ?? [];
    }
}