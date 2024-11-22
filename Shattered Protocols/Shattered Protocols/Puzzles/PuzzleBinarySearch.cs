using Shattered_Protocols.Enumerations;
using System;

namespace Shattered_Protocols.Puzzles
{
    public class BinarySearchPuzzle : Puzzle
    {
        private int lowerBound = 0;
        private int upperBound = 100;
        private int targetAddress;
        private int attemptCount;

        public BinarySearchPuzzle() : base("\tFind the vulnerable server by guessing its address.")
        {
            var random = new Random();
            targetAddress = random.Next(lowerBound, upperBound + 1); // Random target between 0-100.
        }

        public override void Start()
        {
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            ResetAttemptCount();

            GameController.Output(Description);
            GameController.Output($"The target server is somewhere between {lowerBound} and {upperBound}.");
            GameController.Output("Guess the number to locate the server.");
        }

        public override void ReadCommand(string command)
        {
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved.");
                return;
            }

            if (int.TryParse(command, out int guess))
            {
                attemptCount++;

                if (guess == targetAddress)
                {
                    PuzzleSolved($"Correct! The vulnerable server is at {guess}.");
                }
                else if (guess < targetAddress)
                {
                    GameController.Output("\tToo low! Try again.");
                }
                else if (guess > targetAddress)
                {
                    GameController.Output("\tToo high! Try again.");
                }

                // Provide hints after multiple attempts.
                if (attemptCount >= 5 && !IsSolved)
                {
                    GameController.Output($"\tHint: The target server is closer to {targetAddress} than {guess}.");
                }
            }
            else
            {
                GameController.Output("\tInvalid input. Please enter a number.");
            }
        }

        private void PuzzleSolved(string successMessage)
        {
            GameController.Output(successMessage);
            IsSolved = true;
            ResetAttemptCount();
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }
    }
}
