using System;
using System.Text;

namespace AESExample
{
    public class Program
    {
        public static void Main()
        {
            // Sample key and IV (for AES-128, key size is 16 bytes, IV size is 16 bytes)
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            // Sample plaintext
            string plaintext = "This is a test message.";

            // Ensure the plaintext is padded to a multiple of the block size (16 bytes)
            byte[] paddedPlaintext = Pad(Encoding.UTF8.GetBytes(plaintext));

            Console.WriteLine("Padded Plaintext: " + BitConverter.ToString(paddedPlaintext));

            // Initialize the CustomAes object with the key and IV
            CustomAes aes = new CustomAes(key, iv);

            // Encrypt the plaintext
            byte[] encrypted = aes.Encrypt(paddedPlaintext);

            Console.WriteLine("Encrypted: " + BitConverter.ToString(encrypted));

            // Decrypt the ciphertext
            byte[] decrypted = aes.Decrypt(encrypted);

            Console.WriteLine("Decrypted (before unpadding): " + BitConverter.ToString(decrypted));

            // Remove padding from decrypted plaintext
            byte[] unpaddedDecrypted = Unpad(decrypted);

            // Convert decrypted byte array back to string
            string decryptedText = Encoding.UTF8.GetString(unpaddedDecrypted);

            // Display results
            Console.WriteLine("Original Plaintext: " + plaintext);
            Console.WriteLine("Decrypted: " + decryptedText);

            // Check if the original plaintext matches the decrypted text
            if (plaintext == decryptedText)
            {
                Console.WriteLine("Test passed!");
            }
            else
            {
                Console.WriteLine("Test failed!");
            }
        }

        // Padding to ensure plaintext is a multiple of 16 bytes
        public static byte[] Pad(byte[] data)
        {
            int paddingLength = 16 - (data.Length % 16);
            byte[] paddedData = new byte[data.Length + paddingLength];
            Array.Copy(data, paddedData, data.Length);
            for (int i = data.Length; i < paddedData.Length; i++)
            {
                paddedData[i] = (byte)paddingLength;
            }
            return paddedData;
        }

        // Remove padding from decrypted data
        public static byte[] Unpad(byte[] data)
        {
            int paddingLength = data[data.Length - 1];
            if (paddingLength < 1 || paddingLength > 16)
            {
                throw new ArgumentException("Invalid padding length.");
            }

            for (int i = 0; i < paddingLength; i++)
            {
                if (data[data.Length - 1 - i] != paddingLength)
                {
                    throw new ArgumentException("Invalid padding.");
                }
            }

            byte[] unpaddedData = new byte[data.Length - paddingLength];
            Array.Copy(data, unpaddedData, unpaddedData.Length);
            return unpaddedData;
        }
    }
}
