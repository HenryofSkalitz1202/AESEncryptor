using System;
using System.Text;

namespace AESExample
{
    public class Sender
    {
        public byte[] Encrypt(string plaintext, byte[] key, byte[] iv)
        {
            Console.WriteLine("Key: " + Encoding.Default.GetString(key));
            Console.WriteLine("IV: " + Encoding.Default.GetString(iv));

            CustomAes aes = new CustomAes(key, iv);
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            plaintextBytes = Padding(plaintextBytes);
            byte[] ciphertext = aes.Encrypt(plaintextBytes);
            return ciphertext;
        }

        private static byte[] Padding(byte[] input)
        {
            Console.WriteLine("inputLength: " + input.Length);
            Console.WriteLine("mod: " + input.Length % 16);
            int paddingSize = 16 - (input.Length % 16);
            byte[] paddedInput = new byte[input.Length + paddingSize];
            Array.Copy(input, paddedInput, input.Length);
            for (int i = input.Length; i < paddedInput.Length; i++)
            {
                paddedInput[i] = (byte)paddingSize;
            }
            return paddedInput;
        }
    }
}
