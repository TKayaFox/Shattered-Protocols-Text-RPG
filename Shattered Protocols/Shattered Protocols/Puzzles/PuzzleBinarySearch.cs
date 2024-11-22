using Shattered_Protocols.Enumerations;
using System;

// Binary Search Puzzle
// The user is required to locate the vulnerable server using binary search.
// The puzzle is solved when the user inputs the correct IP address of the vulnerable server.
// Hints are provided after the 2nd incorrect attempt.
// The user has an unlimited number of attempts to solve the puzzle.
// The target address is randomly generated within the range
// The answer is the exact IP address of the vulnerable server.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinarySearch : Puzzle
    {
        private int lowerBound = 0;
        private int upperBound = 255;
        private int targetAddress;
        private int attemptCount;

        public PuzzleBinarySearch() : base("Locate the vulnerable server using binary search.")
        {
            // Randomly generate the target address within the range
            var random = new Random();
            targetAddress = random.Next(lowerBound, upperBound + 1);
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
            GameController.Output($"The target server is somewhere between 192.168.1.{lowerBound} and 192.168.1.{upperBound}.");
            GameController.Output("Use binary search commands to find it (e.g., 'scan 192.168.1.[start]-192.168.1.[end]').");
        }

        public override void ReadCommand(string command)
        {
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. No need to input anything further.");
                return;
            }

            attemptCount++;

            // Check if the command is a scan command
            // The command format should be 'scan 192.168.1.[start]-192.168.1.[end]'
            // The target address is within the specified range
            if (command.StartsWith("scan"))
            {
                try
                {
                    var parts = command.Replace("scan", "").Trim().Split('-');

                    // Check if the input contains both start and end of range
                    if (parts.Length != 2)
                    {
                        GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                        return;
                    }

                    // Parse start and end ranges
                    int startRange = int.Parse(parts[0].Split('.')[3]);
                    int endRange = int.Parse(parts[1].Split('.')[3]);

                    // Validate that the range is within bounds
                    if (startRange < lowerBound || endRange > upperBound || startRange > endRange)
                    {
                        GameController.Output("\tInvalid range. Ensure the range is between 192.168.1.0 and 192.168.1.255, and that the start is less than or equal to the end.");
                        return;
                    }

                    // Check if the target address is within the specified range
                    if (startRange <= targetAddress && endRange >= targetAddress)
                    {
                        if (startRange == endRange)
                        {
                            PuzzleSolved($"Target found! The vulnerable server is at 192.168.1.{startRange}.");
                        }
                        else
                        {
                            int midPoint = (startRange + endRange) / 2;

                            if (targetAddress <= midPoint)
                            {
                                GameController.Output("\tThe target is in the **lower range**.");
                            }
                            else
                            {
                                GameController.Output("\tThe target is in the **upper range**.");
                            }

                            ProvideHint();
                        }
                    }
                    else
                    {
                        GameController.Output("\tInvalid range. The target server is not within this range.");
                    }
                }
                catch (Exception)
                {
                    GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                }
            }
        }


        private void ProvideHint()
        {
            // Provide progressively detailed hints
            switch (attemptCount)
            {
                case 2:
                    GameController.Output("\tHint: Use binary search logic. Divide the range into two halves.");
                    break;
                case 4:
                    GameController.Output("\tHint: Narrow your search by scanning only one half of the range.");
                    break;
                case 6:
                    GameController.Output("\tHint: Continue halving the range until you reach the exact address.");
                    break;
                case 8:
                    GameController.Output("\tHint: The command format is 'scan 192.168.1.[start]-192.168.1.[end]'. Double-check your inputs.");
                    break;
                default:
                    if (attemptCount > 8)
                    {
                        GameController.Output("\tHint: Focus on finding the midpoint of the range.");
                    }
                    break;
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
