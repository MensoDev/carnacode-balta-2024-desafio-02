using System.Text.Json.Serialization;

namespace Imc.Domain;

public class User : Entity
{
    public User(string name, string email, string passwordHash, string passwordSalt)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    [JsonInclude]
    public string Name { get; private set; }
    
    [JsonInclude]
    public string Email { get; private set; }
    
    [JsonInclude]
    public string PasswordHash { get; private set; }
    
    [JsonInclude]
    public string PasswordSalt { get; private set; }
}