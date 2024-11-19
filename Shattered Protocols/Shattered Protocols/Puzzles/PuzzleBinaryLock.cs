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
        public PuzzleBinaryLock() : base("Solve the binary lock puzzle.") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("This puzzle has already been solved. You can proceed further.");
                return;
            }
            
            //Puzzle intro
            GameController.Output(@"
             The receptionists here thought that since this is the front desk, they would be cheeky and implement 
             a Binary Code as the front locking mechanism to get into the rest of the building. A Binary Code 
             seemed apt as people regularly enter and exit the front desks, kinda like 1's and 0's.
             ");
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
                PuzzleSolved(@"
             The receptionists thought it was so clever to have this as the code… 
             Too bad it was not clever enough to keep you from getting in… Time to head inside…
             ");
            }
            else
            {
                AttemptCount++;
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

        // check if the user input is the correct binary representation of the number 42
        // binary number can be changed to random number if needed.
        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }
    }
}
