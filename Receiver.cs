using System;
using System.Security.Cryptography;

namespace AESExample
{
    public class Receiver
    {
        public string Decrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                byte[] decryptedBytes;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                }

                return Utf8Decoder.Decode(decryptedBytes);
            }
        }
    }
}


