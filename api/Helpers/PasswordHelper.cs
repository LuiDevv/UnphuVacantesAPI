using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHelper
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int IterationCount = 100000;

    public static string HashPassword(string password)
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: IterationCount,
            numBytesRequested: HashSize
        );

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        try
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                return false; // Formato incorrecto del hash
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            if (salt.Length != SaltSize || hash.Length != HashSize)
            {
                return false; // Longitud incorrecta del salt o del hash
            }

            byte[] enteredHash = KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: HashSize
            );

            return hash.SequenceEqual(enteredHash);
        }
        catch
        {
            // Manejar errores de formato o conversión
            return false;
        }
    }
}
