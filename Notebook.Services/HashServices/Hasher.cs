using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Notebook.Services.HashServices;

public static class Hasher
{
    private const int ITERATIONS = 10_000;

    public static (string hashed, byte[] salt) Hash(string unhashed)
    {
        byte[] salt = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return Hash(unhashed, salt);
    }

    public static bool Verify(string hashed, string unhashed, byte[] salt)
    {
        var result = Hash(unhashed, salt);
        return hashed == result.hashed;
    }

    public static (string hashed, byte[] salt) Hash(string unhashed, byte[] salt)
    {
        byte[] hash = KeyDerivation.Pbkdf2(unhashed, salt, KeyDerivationPrf.HMACSHA256, ITERATIONS, 64);
        string hashed = Convert.ToBase64String(hash);

        return (hashed, salt);
    }
}