using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
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

        public override void ReadCommand(string command)
        {
            if (command == ReverseString(encryptedMessage))
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
