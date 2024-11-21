using Shattered_Protocols.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// Binary search puzzle to locate a vulnerable server
// The puzzle is solved when the user identifies the correct IP address of the server
// Hints are provided after the 4th incorrect attempt
// The user has an unlimited number of attempts to solve the puzzle
namespace Shattered_Protocols.Puzzles
{
    public class PuzzleBinarySearch : Puzzle
    {
        // The range of IP addresses to search
        private int lowerBound = 0; 
        private int upperBound = 1; // the range is 0-1 for testing
        // private int upperBound = 255; uncomment this line post testing
        // The "vulnerable server" to find
        private int targetAddress; 
        private int attemptCount = 0;

        public PuzzleBinarySearch() : base("Locate the vulnerable server using binary search.")
        {
            var random = new Random();
            // Randomly select target IP address
            targetAddress = random.Next(lowerBound, upperBound + 1); 
        }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("This puzzle has already been solved. You can proceed further.");
                return;
            }
            ResetAttemptCount();
            Console.WriteLine(Description);
            Console.WriteLine($"The target server is somewhere between 192.168.1.{lowerBound} and 192.168.1.{upperBound}.");
            Console.WriteLine("Use binary search commands to find it (e.g., 'scan 192.168.1.[start]-192.168.1.[end]').");
        }

        public override void ReadCommand(string command)
        {
            attemptCount++;

            // Parse the input range from the command
            if (command.StartsWith("scan"))
            {
                try
                {
                    var parts = command.Replace("scan", "").Trim().Split('-');
                    // Extract start of range
                    int startRange = int.Parse(parts[0].Split('.')[3]); 
                    // Extract end of range
                    int endRange = int.Parse(parts[1].Split('.')[3]);  

                    if (startRange <= targetAddress && endRange >= targetAddress)
                    {
                        if (startRange == endRange)
                        {
                            Console.WriteLine($"Target found! The vulnerable server is at 192.168.1.{startRange}.");
                            IsSolved = true;
                        }
                        else
                        {
                            int midPoint = (startRange + endRange) / 2;
                            if (targetAddress <= midPoint)
                            {
                                Console.WriteLine("Target is in the lower range.");
                                upperBound = midPoint;
                            }
                            else
                            {
                                Console.WriteLine("Target is in the upper range.");
                                lowerBound = midPoint + 1;
                            }

                            Console.WriteLine($"New range: 192.168.1.{lowerBound} - 192.168.1.{upperBound}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid range. The target server is not within this range.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid command format. Use 'scan 192.168.1.[start]-192.168.1.[end]'.");
                }
            }
            else
            {
                Console.WriteLine("Unknown command. Use 'scan' to narrow the range.");
            }

            // Provide hints after certain attempts
            if (attemptCount >= 4 && !IsSolved)
            {
                Console.WriteLine("Hint: Use a binary search strategy to minimize the range each time.");
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }
    }
}
