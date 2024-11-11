using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinaryLock : Puzzle
    {
        public PuzzleBinaryLock() : base("Solve the binary lock puzzle.", "Binary input") { }

        public override void Start()
        {
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter the binary representation of the number 42:");
        }

        private void CheckInput(string input)
        {
            if (CheckBinaryInput(input, 42))
            {
                GameController.Output("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                AttemptCount ++;
                GameController.Output("Incorrect, try again.");
                ProvideHint();
            }
        }

        public override void ReadCommand(string command)
        {
            CheckInput(command);
        }

        private void ProvideHint()
        {
            if (AttemptCount == 2)
            {
                GameController.Output("Hint: The number 42 in binary is a 6-digit number.");
            }
            else if (AttemptCount == 4)
            {
                GameController.Output("Hint: 42 in binary is made of alternating 1s and 0s.");
            }
        }

        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }
    }
}
