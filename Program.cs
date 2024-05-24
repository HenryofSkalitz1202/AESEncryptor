using System;
using System.Text;

namespace AESExample
{
public class Program
    {
        private static byte[]? key;
        private static byte[]? iv;
        public static void Main(string[] args)
        {
            key =  GenerateRandomBytes(32);
            iv = GenerateRandomBytes(16);

            Console.WriteLine("Key: " + Encoding.Default.GetString(key));
            Console.WriteLine("IV: " + Encoding.Default.GetString(iv));

            while(true){
                Console.WriteLine("Choose an option: (1) Send, (2) Receive, (q) Quit");
                string option = Console.ReadLine() ?? string.Empty;

                if (option == "1")
                {
                    Console.WriteLine("Enter plaintext:");
                    string plaintext = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine($"Plaintext: {plaintext}");

                    // Encrypt the plaintext (you need to implement encryption similar to your decryption)
                    // 
                    CustomAes aes = new(key, iv);
                    byte[] plaintextBytes = Padding(plaintext);
                    byte[] encrypted = aes.Encrypt(plaintextBytes);
                    string ciphertext = Convert.ToBase64String(encrypted);

                    Console.WriteLine($"Ciphertext: {ciphertext}");
                }
                else if (option == "2")
                {
                    Console.WriteLine("Enter ciphertext:");
                    string inputCiphertext = Console.ReadLine() ?? string.Empty;
                    byte[] ciphertext = Convert.FromBase64String(inputCiphertext);

                    // Decrypt the ciphertext
                    Receiver receiver = new Receiver();
                    byte[] decrypted = receiver.Decrypt(ciphertext, key, iv);

                    string plaintext = Encoding.UTF8.GetString(decrypted);
                    Console.WriteLine($"Decrypted text: {plaintext}");
                }else if (option == "q"){
                    break;
                }
            }
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            new Random().NextBytes(bytes);
            return bytes;
        }

        private static byte[] Padding(string input)
        {
            int paddingSize = 16 - (input.Length % 16);
            byte[] paddedInput = new byte[input.Length + paddingSize];
            Array.Copy(Encoding.UTF8.GetBytes(input), paddedInput, input.Length);
            for (int i = input.Length; i < paddedInput.Length; i++)
            {
                paddedInput[i] = (byte)paddingSize;
            }
            return paddedInput;
        }
    }
}
