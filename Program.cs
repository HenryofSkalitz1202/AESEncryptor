using System;

namespace AESExample
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose an option: (1) Send, (2) Receive, (q) Quit");
                string choice = Console.ReadLine() ?? string.Empty;

                if (choice == "1")
                {
                    Send();
                }
                else if (choice == "2")
                {
                    Receive();
                }
                else if (choice.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or q to quit.");
                }
            }
        }

        static void Send()
        {
            Sender sender = new Sender();

            Console.WriteLine("Enter plaintext: ");
            string plaintext = Console.ReadLine() ?? string.Empty;

            (byte[] ciphertext, byte[] key, byte[] iv) = sender.Encrypt(plaintext);

            // Display the results
            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine($"Ciphertext: {Convert.ToBase64String(ciphertext)}");

            // Save the key and IV for the receiver
            KeyStorage.Key = key;
            KeyStorage.IV = iv;
        }

        static void Receive()
        {
            Receiver receiver = new Receiver();

            Console.WriteLine("Enter ciphertext: ");
            byte[] ciphertext = Convert.FromBase64String(Console.ReadLine() ?? string.Empty);

            if (KeyStorage.Key == null || KeyStorage.IV == null)
            {
                Console.WriteLine("Key and IV are not set. Please run the sender first.");
                return;
            }

            string decryptedText = receiver.Decrypt(ciphertext, KeyStorage.Key, KeyStorage.IV);

            // Display the results
            Console.WriteLine($"Decrypted text: {decryptedText}");
        }
    }
}


