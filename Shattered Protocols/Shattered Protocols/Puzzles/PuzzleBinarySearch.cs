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

        public PuzzleBinarySearch() : base("\tLocate the vulnerable server using binary search.")
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

            // Puzzle intro
            GameController.Output(@"
    The door across seems to be locked and can't be unlocked from this side. You thought 
    it odd that exiting the breakroom would also have a lock. The safe has a terminal 
    attached to it that says, “Warm Back Up for Work!” The lock seems to want you to 
    pinpoint a specific randomized IP address within a range. Since you have extensive 
    knowledge of binary search application, this will be a cinch for you. 
    This place is just a puzzle bonanza!
            ");

            ResetAttemptCount();
            GameController.Output(Description);
            GameController.Output($"\tThe target server is somewhere between 192.168.1.{lowerBound} and 192.168.1.{upperBound}.");
            GameController.Output("\tUse binary search commands to find it (e.g., 'scan 192.168.1.[start]-192.168.1.[end]').");
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
                // The puzzle throws an exception if the command is not in the correct format
                // Issue fixed with try/catch block
                try
                {
                    var parts = command.Replace("scan", "").Trim().Split('-');

                    // Check if the input contains both start and end of range
                    if (parts.Length != 2 || string.IsNullOrEmpty(parts[1]))
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
                            PuzzleSolved($"\tTarget found! The vulnerable server is at 192.168.1.{startRange}.");
                            // Puzzle outro
                            GameController.Output(@"
    You pinpointed the IP address and punched it into the terminal. You hear a *click*.  
    As you open the safe you find a flash drive in the shape of a rubber ducky! This 
    must be important… You thought it was perplexing that a safe like this would 
    have such an accessible way of getting in. You certainly feel warmed back up 
    from that puzzle.
                            ");
                            IsSolved = true;
                            ResetAttemptCount();
                        }
                        // Provide hints to guide the user
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
                catch (FormatException)
                {
                    GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                }
                catch (IndexOutOfRangeException)
                {
                    GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                }
                catch (Exception)
                {
                    GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                }
            }
            else{
                GameController.Output("\tInvalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
            }
        }


        private void ProvideHint()
        {
            // Provide progressively detailed hints
            switch (attemptCount)
            {
                case 2:
                    GameController.Output("\tHint 1: Use binary search logic. Divide the range into two halves.");
                    break;
                case 4:
                    GameController.Output("\tHint 2: Narrow your search by scanning only one half of the range.");
                    break;
                case 6:
                    GameController.Output("\tHint 3: Continue halving the range until you reach the exact address.");
                    break;
                case 8:
                    GameController.Output("\tHint 4: The command format is 'scan 192.168.1.[start]-192.168.1.[end]'. Double-check your inputs.");
                    break;
                default:
                    if (attemptCount > 8)
                    {
                        GameController.Output("\tHint 5: Focus on finding the midpoint of the range.");
                    }
                    break;
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }
    }
}
