using System;
using System.Security.Cryptography;
using System.Text;

namespace AESExample
{
    public class Sender
    {
        public (byte[] ciphertext, byte[] key, byte[] iv) Encrypt(string plaintext)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.GenerateKey();
                aes.GenerateIV();

                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] ciphertext;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                }

                return (ciphertext, aes.Key, aes.IV);
            }
        }
    }
}
