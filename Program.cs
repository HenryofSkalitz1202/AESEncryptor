﻿using System.Text;

namespace AESExample
{
    class Program
    {
        static void Main()
        {
            // Example key (128-bit key)
            byte[] key = new byte[16] {
                0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6,
                0xab, 0xf7, 0x4e, 0x35, 0x0b, 0x34, 0x78, 0x55
            };

            // Example IV (128-bit)
            byte[] iv = new byte[16] {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f
            };

            // Example plaintext (32 bytes)
            byte[] plaintext = new byte[32] {
                0x32, 0x43, 0xf6, 0xa8, 0x88, 0x5a, 0x30, 0x8d,
                0x31, 0x31, 0x98, 0xa2, 0xe0, 0x37, 0x07, 0x34,
                0x4a, 0x49, 0x6d, 0x4c, 0x2e, 0xa1, 0xe4, 0x9f,
                0xb0, 0x3e, 0xcb, 0x3b, 0xe4, 0x57, 0x1e, 0x2d
            };

            CustomAes aes = new CustomAes(key, iv);

            //string plaintextString = Encoding.UTF8.GetString(plaintext);
            Console.WriteLine("Plaintext: " + BitConverter.ToString(plaintext));

            byte[] ciphertext = aes.Encrypt(plaintext);
            Console.WriteLine("Ciphertext: " + BitConverter.ToString(ciphertext));

            byte[] decrypted = aes.Decrypt(ciphertext);
            Console.WriteLine("Decrypted: " + BitConverter.ToString(decrypted));

            string normalString = "Hello, World!"; // Your normal string
            byte[] byteArray = new byte[32];

            // Convert the string to a byte array using UTF-8 encoding
            byte[] stringBytes = Encoding.UTF8.GetBytes(normalString);

            // Copy the string bytes to the byte array
            Array.Copy(stringBytes, byteArray, Math.Min(stringBytes.Length, byteArray.Length));

            // Calculate padding size
            int paddingSize = byteArray.Length - stringBytes.Length;
            byte padValue = (byte)paddingSize;

            // Apply PKCS#7 padding
            for (int i = stringBytes.Length; i < byteArray.Length; i++)
            {
                byteArray[i] = padValue;
            }

            Console.WriteLine("\nInput: " + BitConverter.ToString(byteArray));

            ciphertext = aes.Encrypt(byteArray);
            Console.WriteLine("Ciphertext: " + BitConverter.ToString(ciphertext));

            decrypted = aes.Decrypt(ciphertext);
            Console.WriteLine("Decrypted: " + BitConverter.ToString(decrypted));
        }
    }
}
