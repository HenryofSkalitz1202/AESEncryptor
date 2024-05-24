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
            return decrypted;
        }

        private byte[] Unpadding(byte[] input){
            int paddingSize = input[input.Length - 1];
            paddingSize = paddingSize % 16;
            Console.WriteLine();
            Console.WriteLine("inputLength: " + input.Length);
            Console.WriteLine("paddingSize: " + paddingSize);
            byte[] unpaddedInput = new byte[input.Length - paddingSize];
            Array.Copy(input, unpaddedInput, unpaddedInput.Length);
            return unpaddedInput;
        }
    }
}

