using Shattered_Protocols.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Binary search puzzle to locate a vulnerable server.
// The puzzle is solved when the user identifies the correct IP address of the server.
// Hints are provided after the 4th incorrect attempt.
// The user has an unlimited number of attempts to solve the puzzle.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinarySearch : Puzzle
    {
        // The range of IP addresses to search.
        private int lowerBound = 0;
        private int upperBound = 1; // The range is 0-1 for testing.
        // private int upperBound = 255; // Uncomment this line post-testing.

        // The "vulnerable server" to find.
        private int targetAddress;
        private int attemptCount;

        public PuzzleBinarySearch() : base("\tLocate the vulnerable server using binary search.")
        {
            var random = new Random();
            // Randomly select target IP address within the specified range.
            targetAddress = random.Next(lowerBound, upperBound + 1);
        }

        public override void Start()
        {
            // Check if the puzzle is already solved.
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            // Reset the attempt count when the puzzle starts.
            ResetAttemptCount();

            // Puzzle intro message.
            GameController.Output(Description);
            GameController.Output($"The target server is somewhere between 192.168.1.{lowerBound} and 192.168.1.{upperBound}.");
            GameController.Output("Use binary search commands to find it (e.g., 'scan 192.168.1.[start]-192.168.1.[end]').");
        }

        public override void ReadCommand(string command)
        {
            // If the puzzle is already solved, do not process further input.
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. No need to input anything further.");
                return;
            }

            attemptCount++;

            // Parse the input range from the command.
            if (command.StartsWith("scan"))
            {
                try
                {
                    var parts = command.Replace("scan", "").Trim().Split('-');
                    // Extract start of range.
                    int startRange = int.Parse(parts[0].Split('.')[3]);
                    // Extract end of range.
                    int endRange = int.Parse(parts[1].Split('.')[3]);

                    // Check if the target address is within the specified range.
                    if (startRange <= targetAddress && endRange >= targetAddress)
                    {
                        if (startRange == endRange)
                        {
                            // Puzzle is solved when the exact address is identified.
                            PuzzleSolved($"Target found! The vulnerable server is at 192.168.1.{startRange}.");
                        }
                        else
                        {
                            // Perform binary search and adjust the range.
                            int midPoint = (startRange + endRange) / 2;
                            if (targetAddress <= midPoint)
                            {
                                GameController.Output("\tTarget is in the lower range.");
                                upperBound = midPoint;
                            }
                            else
                            {
                                GameController.Output("\tTarget is in the upper range.");
                                lowerBound = midPoint + 1;
                            }

                            GameController.Output($"\tNew range: 192.168.1.{lowerBound} - 192.168.1.{upperBound}");
                        }
                    }
                    else
                    {
                        GameController.Output("\tInvalid range. The target server is not within this range.");
                    }
                }
                catch (Exception)
                {
                    GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]' to narrow the range.");
                }
            }
            else
            {
                GameController.Output("\tUnknown command. Use 'scan' to narrow the range.");
            }

            // Provide hints after the 4th incorrect attempt.
            if (attemptCount >= 4 && !IsSolved)
            {
                GameController.Output("\tHint: Use a binary search strategy to minimize the range each time.");
            }
        }

        private void PuzzleSolved(string successMessage)
        {
            GameController.Output(successMessage);
            IsSolved = true; // Mark the puzzle as solved.
            ResetAttemptCount(); // Reset attempt count when solved.
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }
    }
}
