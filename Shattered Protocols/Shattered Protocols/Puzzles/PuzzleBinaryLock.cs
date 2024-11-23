using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// PuzzleBinaryLock is a puzzle that requires the user to input the binary representation of the number 42. 
// The puzzle is solved when the user inputs the correct binary number.
// Hints are provided after the 2nd and 4th incorrect attempts.
// The user has an unlimited number of attempts to solve the puzzle.
// The answer is hard-coded to 42 but can be changed to any number or even a random number if needed.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinaryLock : Puzzle
    {
        // Constructor for PuzzleBinaryLock
        public PuzzleBinaryLock() : base("\tSolve the binary lock puzzle.") { }

        // Start method is invoked when the puzzle is triggered
        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            // Debugging: Output the current state of IsSolved for testing purposes
            GameController.Output($"\tPuzzle solved state: {IsSolved}");

            // Puzzle intro message
            GameController.Output(@"
    The receptionists here thought that since this is the front desk, they would be cheeky and implement 
    a Binary Code as the front locking mechanism to get into the rest of the building. A Binary Code 
    seemed apt as people regularly enter and exit the front desks, kinda like 1's and 0's.
    ");

            // Reset the attempt count when the puzzle starts
            ResetAttemptCount();

            // Prompt the user to start solving the puzzle
            GameController.Output(Description);
            GameController.Output("\tEnter the binary representation of the number 42:");
        }
        
        // Check the user input to see if it is correct.
        // If the input is correct, the puzzle is solved.
        private void CheckInput(string input)
        {
            // Normalize the input to handle potential edge cases (e.g., extra spaces)
            string trimmedInput = input.Trim();

            // Check if the input matches the correct binary representation of the target number
            if (CheckBinaryInput(trimmedInput, 42))
            {
                PuzzleSolved(@"
    The receptionists thought it was so clever to have this as the code… 
    Too bad it was not clever enough to keep you from getting in… Time to head inside… 
                ");
                IsSolved = true;
                ResetAttemptCount(); // Reset attempt count when solved
            }
            else
            {
                AttemptCount++;
                GameController.Output("\tIncorrect, try again.");
                ProvideHint();
            }
        }

        // ReadCommand method is invoked when the user inputs a command
        public override void ReadCommand(string command)
        {
            // If the puzzle is already solved, do not process further input
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. No need to input anything further.");
                return;
            }

            // Otherwise, check the user's input
            CheckInput(command);
        }

        // Hints are provided after the 2nd and 4th incorrect attempts.
        private void ProvideHint()
        {
            if (AttemptCount == 2)
            {
                GameController.Output("\tHint: The number 42 in binary is a 6-digit number.");
            }
            else if (AttemptCount == 4)
            {
                GameController.Output("\tHint: 42 in binary is made of alternating 1s and 0s.");
            }
        }

        // Method to check if the user input matches the correct binary representation of the target number
        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }

        // ResetAttemptCount ensures the attempt counter is set to 0
        private void ResetAttemptCount()
        {
            AttemptCount = 0;
            GameController.Output("\t Attempt count reset to 0.");
        }
    }
}
