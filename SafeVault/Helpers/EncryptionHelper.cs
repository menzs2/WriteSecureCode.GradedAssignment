namespace SafeVault;

public class EncryptionHelper
{
    // Key and IV will be loaded from configuration
    private static byte[] Key;
    private static byte[] IV;

    // Initialize the encryption helper with the key and IV from the configuration
    public static void Initialize(IConfiguration configuration)
    {

        var key = configuration["Encryption:Key"].Trim();
        var iv = configuration["Encryption:IV"].Trim();

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
        Key = Convert.FromBase64String(configuration["Encryption:Key"]);
        IV = Convert.FromBase64String(configuration["Encryption:IV"]);
    }

    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var ms = new MemoryStream())
            using (var cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            var buffer = Convert.FromBase64String(cipherText);
            using (var ms = new MemoryStream(buffer))
            using (var cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
