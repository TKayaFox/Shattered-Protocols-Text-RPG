using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    // Caesar Cipher Puzzle
    public class PuzzleCaesarCipher : Puzzle
    {
        private readonly string encryptedMessage = "Khoor Zruog"; // "Hello World" shifted by 3
        private readonly int shiftAmount = 3;
        private int attemptCount = 0;

        public PuzzleCaesarCipher() : base("Decrypt the Caesar ciphered message.", "Decryption input") { }
        public override void Start()
        {
            ResetattemptCount();
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            string correctDecryption = DecryptCaesar(encryptedMessage, shiftAmount);

            if (command.Trim().Equals(correctDecryption, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");

                //Hint
                if (AttemptCount >= 4)
                {
                    Console.WriteLine("Hint: The original message is a common greeting that is shifted 3 times. Not gonna tell you which way...");
                }
            }
        }

        private string DecryptCaesar(string input, int shift)
        {
            StringBuilder decrypted = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    decrypted.Append((char)((c - d - shift + 26) % 26 + d));
                }
                else
                {
                    decrypted.Append(c);
                }
            }
            return decrypted.ToString();
        }
    }
}
