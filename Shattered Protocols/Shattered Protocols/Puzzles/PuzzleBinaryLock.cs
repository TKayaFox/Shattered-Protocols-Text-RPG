using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// PuzzleBinaryLock is a puzzle that requires the user to input the binary representation of the number 42. 
// The puzzle is solved when the user inputs the correct binary number.
// Hints are provided after the 2nd and 4th incorrect attempts.
// The user has an unlimited number of attempts to solve the puzzle.
// the answer is hard coded to 42, but can be changed to any number/ even random number if needed.
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

        // Check the user input to see if it is correct.
        // If the input is correct, the puzzle is solved.
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

        // Read command method to check the user input.
        public override void ReadCommand(string command)
        {
            CheckInput(command);
        }

        // Hints are provided after the 2nd and 4th incorrect attempts.
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
