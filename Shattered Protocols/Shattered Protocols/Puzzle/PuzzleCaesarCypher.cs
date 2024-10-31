using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `ItemRequired` for clarity if needed.
        public bool IsSolved { get; protected set; } = false;

        // Constructor to initialize description and required item
        protected Puzzle(string puzzleDescription, string itemRequired)
        {
            Description = puzzleDescription;
            ItemRequired = itemRequired;
        }

        /// <summary>
        /// Starts the puzzle logic.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Reads player input and determines how best to handle it.
        /// </summary>
        public abstract void ReadCommand(string command, string remainder);
    }

    // Caesar Cipher Puzzle
    public class PuzzleCaesarCypher : Puzzle
    {
        private string encryptedMessage = "Khoor Zruog"; // "Hello World" shifted by 3
        private int shiftAmount = 3;

        public PuzzleCaesarCypher() : base("Decrypt the Caesar ciphered message.", "Decryption input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (remainder == DecryptCaesar(encryptedMessage, shiftAmount))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");
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
