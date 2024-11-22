using Shattered_Protocols.Enumerations;
using System;

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinarySearch : Puzzle
    {
        private int lowerBound = 0;
        private int upperBound = 255; // Full range for the puzzle.
        private int targetAddress;
        private int attemptCount;

        public PuzzleBinarySearch() : base("\tLocate the vulnerable server using binary search.")
        {
            var random = new Random();
            targetAddress = random.Next(lowerBound, upperBound + 1); // Random target between 0-255.
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
                GameController.Output("\tThis puzzle has already been solved.");
                return;
            }

            attemptCount++;

            if (command.StartsWith("scan", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var range = command.Replace("scan", "").Trim();
                    var parts = range.Split('-');

                    if (parts.Length != 2)
                        throw new ArgumentException("Invalid range format.");

                    // Parse the start and end of the range
                    int startRange = ParseIPAddress(parts[0]);
                    int endRange = ParseIPAddress(parts[1]);

                    // Ensure the range is within bounds
                    if (startRange < lowerBound || endRange > upperBound || startRange > endRange)
                    {
                        GameController.Output("\tInvalid range. Ensure it is within the current bounds and correctly formatted.");
                        return;
                    }

                    // Check if the target is within the range
                    if (startRange <= targetAddress && endRange >= targetAddress)
                    {
                        if (startRange == endRange)
                        {
                            // Puzzle solved
                            PuzzleSolved($"Target found! The vulnerable server is at 192.168.1.{startRange}.");
                        }
                        else
                        {
                            // Provide hints based on the proximity to the target
                            int midPoint = (startRange + endRange) / 2;
                            GiveHint(midPoint);

                            // Perform binary search
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
                catch (Exception ex)
                {
                    GameController.Output($"\tError: {ex.Message}. Use 'scan 192.168.1.[start]-192.168.1.[end]' to narrow the range.");
                }
            }
            else
            {
                GameController.Output("\tUnknown command. Use 'scan' to narrow the range.");
            }

            // Provide hints after multiple attempts
            if (attemptCount >= 4 && !IsSolved)
            {
                GameController.Output("\tHint: Use binary search strategy. Divide the range into two halves each time.");
            }
        }

        private int ParseIPAddress(string ipAddress)
        {
            var parts = ipAddress.Trim().Split('.');
            if (parts.Length != 4 || !int.TryParse(parts[3], out int lastOctet))
                throw new FormatException("Invalid IP address format. Use '192.168.1.[number]'.");
            return lastOctet;
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

        private void GiveHint(int midPoint)
        {
            int distance = Math.Abs(midPoint - targetAddress);

            if (distance < 5)
            {
                GameController.Output("\tHint: You’re getting hotter!");
            }
            else if (distance <= 20)
            {
                GameController.Output("\tHint: You’re getting warmer.");
            }
            else
            {
                GameController.Output("\tHint: You’re getting colder.");
            }
        }
    }
}
