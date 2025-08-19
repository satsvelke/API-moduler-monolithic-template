using System.Security.Cryptography;
using System.Text;

namespace Nucleus.Utilities;

public static class AesEncryption
{
    private static readonly string key = "Cx/93t6VWPqJ27V+UhB2x0v6BdvJg";

    private static readonly string iv = "x1n6qDHVQX3XWynt";

    public static string Encrypt(this string plainText)
    {
        ArgumentException.ThrowIfNullOrEmpty(plainText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Key = Encoding.ASCII.GetBytes(key.PadRight(32));
            aesAlg.IV = Encoding.ASCII.GetBytes(iv.PadRight(16));

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Decrypt(this string cipherText)
    {
        ArgumentException.ThrowIfNullOrEmpty(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Key = Encoding.ASCII.GetBytes(key.PadRight(32));
            aesAlg.IV = Encoding.ASCII.GetBytes(iv.PadRight(16));

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }

}
