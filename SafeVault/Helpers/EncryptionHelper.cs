using System.Security.Cryptography;

namespace SafeVault;

public class EncryptionHelper
{
    // Key and IV will be loaded from configuration
    private static byte[] Key;
    private static byte[] IV;

    // Initialize the encryption helper with the key and IV from the configuration
    public static void Initialize(IConfiguration configuration)
    {

        // For your appsettings.json or secrets
        var key = configuration["Encryption:Key"]; // 32 bytes for AES-256
        var iv = configuration["Encryption:IV"];  // 16 bytes for AES IV
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");
        }
        // Check if the configuration contains the required keys
        if (string.IsNullOrEmpty(configuration["Encryption:Key"]) || string.IsNullOrEmpty(configuration["Encryption:IV"]))
        {
            throw new ArgumentException("Encryption key or IV not found in configuration.");
        }
        // Check if the configuration is null
        // Load the key and IV from the configuration file
        Key = Convert.FromBase64String(key);
        IV = Convert.FromBase64String(iv);
    }

    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        using (var aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        using (var aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            var buffer = Convert.FromBase64String(cipherText);
            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
