using System.Security.Cryptography;
using System.Text;

namespace Imc.Services;

public interface IHashService
{
    (string hash, string salt) CreateHash(string password);

    bool Verify(string hash, string password, string saltHex);
}

public class HashService : IHashService
{
    private const int KeySize = 64;
    private const int Iterations = 100;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    public (string hash, string salt) CreateHash(string password) => HashPassword(password);

    private static (string hash, string salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        return (Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public bool Verify(string hash, string password, string saltHex)
    {
        var salt = Convert.FromHexString(saltHex);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}