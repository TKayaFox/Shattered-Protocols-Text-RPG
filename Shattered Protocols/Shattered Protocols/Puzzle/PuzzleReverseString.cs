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

    // Reverse String Puzzle
    public class PuzzleReverseString : Puzzle
    {
        private string encryptedMessage = "edoc terces";

        public PuzzleReverseString() : base("Decrypt the reversed message.", "Decryption input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (remainder == ReverseString(encryptedMessage))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");
            }
        }

        private string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
