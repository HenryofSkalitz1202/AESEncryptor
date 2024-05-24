using System;
using System.Text;

namespace AESExample
{
    public class Receiver
    {
        public byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            // Decrypt the ciphertext (you can use your CustomAes class for this)
            CustomAes aes = new CustomAes(key, iv);
            byte[] decrypted = aes.Decrypt(ciphertext);

            // Unpad the decrypted data
            byte[] unpadded = Unpadding(decrypted);
            return unpadded;
        }

        public byte[] Unpadding(byte[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentException("Input cannot be null or empty.");

            // Get the value of the last byte
            int paddingLength = input[input.Length - 1];

            Console.WriteLine("Padding: " + paddingLength);
            Console.WriteLine("Input: " + input.Length);
            if (paddingLength < 1 || paddingLength > input.Length)
                throw new ArgumentException("Invalid padding length.");

            // Validate the padding bytes
            for (int i = input.Length - paddingLength; i < input.Length; i++)
            {
                if (input[i] != paddingLength)
                    throw new ArgumentException("Invalid padding.");
            }

            // Create a new array without the padding bytes
            byte[] output = new byte[input.Length - paddingLength];
            Array.Copy(input, 0, output, 0, output.Length);
            return output;
        }
    }
}

