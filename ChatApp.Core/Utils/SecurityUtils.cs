using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Core.Utils
{
    public class Security
    {
        private static readonly string EncryptionKey = "YourSecretEncryptionKey123!"; // Replace with your own key

        // Ensure the key is exactly 32 bytes for AES-256 encryption
        private static byte[] GetValidKey(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // If the key is not 32 bytes, pad or truncate to make it 32 bytes (for AES-256)
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
            }
            else if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32);
            }

            return keyBytes;
        }

        // Encrypt a string using AES algorithm
        public static string Encrypt(string plainText)
        {
            byte[] keyBytes = GetValidKey(EncryptionKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = keyBytes.Take(16).ToArray();  // Use the first 16 bytes for IV

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        // Decrypt a string using AES algorithm
        public static string Decrypt(string cipherText)
        {
            byte[] keyBytes = GetValidKey(EncryptionKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = keyBytes.Take(16).ToArray();  // Use the first 16 bytes for IV

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
