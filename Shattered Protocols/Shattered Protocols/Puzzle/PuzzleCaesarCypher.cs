using System;
using System.Text;

namespace Shattered_Protocols
{
    // reset attempt count when player first encounters the puzzle
    public void ResetattemptCount()
    {
        int attemptCount = 0;
    }
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; }
        public bool IsSolved { get; protected set; } = false;

        protected Puzzle(string puzzleDescription, string itemRequired)
        {
            Description = puzzleDescription;
            ItemRequired = itemRequired;
        }

        public abstract void Start();
        public abstract void ReadCommand(string command, string remainder);
    }

    // Caesar Cipher Puzzle
    public class PuzzleCaesarCipher : Puzzle
    {
        private readonly string encryptedMessage = "Khoor Zruog"; // "Hello World" shifted by 3
        private readonly int shiftAmount = 3;
        private int attemptCount = 0;

        public PuzzleCaesarCipher() : base("Decrypt the Caesar ciphered message.", "Decryption input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            attemptCount++;
            string correctDecryption = DecryptCaesar(encryptedMessage, shiftAmount);

            if (remainder.Trim().Equals(correctDecryption, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");

                if (attemptCount >= 4)
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
                    decrypted.Append((char)((((c - d - shift) + 26) % 26) + d));
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
