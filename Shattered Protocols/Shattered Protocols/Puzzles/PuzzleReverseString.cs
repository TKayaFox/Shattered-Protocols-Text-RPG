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
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output($"Encrypted Message: {encryptedMessage}");
            GameController.Output("Enter the correct decryption:");
        }

        public override void ReadCommand(string command)
        {
            if (command == ReverseString(encryptedMessage))
            {
                GameController.Output("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                GameController.Output("Incorrect. Try again.");
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
