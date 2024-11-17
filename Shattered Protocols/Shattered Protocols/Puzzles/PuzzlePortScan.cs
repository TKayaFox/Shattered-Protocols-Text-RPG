using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    public class PortScanPuzzle : Puzzle
    {
        private int attemptCount = 0;

        public PortScanPuzzle() : base("Conduct a port scan") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                Console.WriteLine("This puzzle has already been solved. You don't need to solve it again.");
                return;
            }

            // Reset attempt count if the puzzle is being attempted again
            ResetAttemptCount();
            Console.WriteLine(Description);
            Console.WriteLine("Enter the command to conduct a thorough port scan at IP address 192.126.98.10:");
        }

        public override void ReadCommand(string command)
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                Console.WriteLine("This puzzle has already been solved.");
                return;
            }

            if (command.Contains("nmap -n -v -p- -A 192.126.98.10"))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true; // Mark the puzzle as solved
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");

                if (attemptCount >= 1)
                {
                    Console.WriteLine("The command needs to have these specifications:");
                    Console.WriteLine("- No domain resolution");
                    Console.WriteLine("- Verbose mode");
                    Console.WriteLine("- Scan ports 1-65535");
                    Console.WriteLine("- Conduct service enumeration, OS detection, and traceroute");
                }
                if (attemptCount >= 3)
                {
                    Console.WriteLine("Hint: -n");
                }
                if (attemptCount >= 4)
                {
                    Console.WriteLine("Hint: -v");
                }
                if (attemptCount >= 5)
                {
                    Console.WriteLine("Hint: -p-");
                }
                if (attemptCount >= 6)
                {
                    Console.WriteLine("Hint: -A");
                }
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }
    }
}
