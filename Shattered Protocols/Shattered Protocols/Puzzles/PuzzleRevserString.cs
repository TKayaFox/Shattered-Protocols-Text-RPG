using System;

namespace Shattered_Protocols.Puzzles
{
    // Reverse String Puzzle
    public class PuzzleRevserString : Puzzle
    {
        private string encryptedMessage = "edoc terces";

        public PuzzleRevserString() : base("Decrypt the reversed message.", "Decryption input") { }

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
