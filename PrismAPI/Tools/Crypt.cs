namespace PrismAPI.Tools;

/// <summary>
/// The <see cref="Crypt"/> class, used for binary security.
/// This class is not verified secure!
/// </summary>
public static class Crypt
{
    /// <summary>
    /// Encrypts data using an char array as a key.
    /// Chars in C# can use up to 2 bytes per character.
    /// </summary>
    /// <param name="Key">The Key to encrypt with.</param>
    /// <param name="Input">The Input data to encrypt.</param>
    /// <returns>Encrypted data.</returns>
    public static byte[] Encrypt(char[] Key, byte[] Input)
    {
        byte[] Encrypted = new byte[Input.Length];

        // Loop over all bytes to cypher/encrypt.
        for (int I = 0; I < Input.Length; I++)
        {
            // Offset the byte by the value of the key, and modulate the key.
            Encrypted[I] = (byte)(Input[I] + Key[I % Key.Length] & 255);
        }

        return Encrypted;
    }

    /// <summary>
    /// Decrypts data using an int array as a key.
    /// Chars in C# can use up to 2 bytes per character.
    /// </summary>
    /// <param name="Key">The Key to decrypt with.</param>
    /// <param name="Input">The Input data to decrypt.</param>
    /// <returns>decrypted data.</returns>
    public static byte[] Decrypt(char[] Key, byte[] Input)
    {
        byte[] Decrypted = new byte[Input.Length];

        // Loop over all bytes to decypher/decrypt.
        for (int I = 0; I < Input.Length; I++)
        {
            // Offset the byte by the value of the key, and modulate the key.
            Decrypted[I] = (byte)(Input[I] - Key[I % Key.Length] & 255);
        }

        return Decrypted;
    }

    /// <summary>
    /// Encrypts data using a string as a key.
    /// </summary>
    /// <param name="Key">The Key to encrypt with.</param>
    /// <param name="Input">The Input data to encrypt.</param>
    /// <returns>Encrypted data.</returns>
    public static byte[] Encrypt(string Key, byte[] Input)
    {
        return Encrypt(Key.ToCharArray(), Input);
    }

    /// <summary>
    /// Decrypts data using a string as a key.
    /// </summary>
    /// <param name="Key">The Key to decrypt with.</param>
    /// <param name="Input">The Input data to decrypt.</param>
    /// <returns>decrypted data.</returns>
    public static byte[] Decrypt(string Key, byte[] Input)
    {
        return Decrypt(Key.ToCharArray(), Input);
    }

    /// <summary>
    /// Hashes a key into non-reversable output.
    /// </summary>
    /// <param name="Key">The Key to hash.</param>
    /// <returns>The input Key as a number hash.</returns>
    public static string Hash(string Key)
    {
        string Hashed = string.Empty;

        if (Key.Length % 2 == 1)
        {
            Key += '\0';
        }

        for (int I = 0; I < Key.Length; I += 2)
        {
            Hashed += ((byte)Key[I] + (byte)Key[I + 1]) % 255;
        }

        return Hashed;
    }
}