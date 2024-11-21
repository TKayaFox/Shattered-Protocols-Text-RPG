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
                Console.WriteLine("\tThis puzzle has already been solved. You don't need to solve it again.");
                return;
            }

            // Reset attempt count when starting the puzzle for the first time
            ResetAttemptCount();

            Console.WriteLine(Description);
            Console.WriteLine("\tEnter the command to conduct a thorough port scan at IP address 192.126.98.10:");
        }

        public override void ReadCommand(string command)
        {
            // If the puzzle has been solved, exit early
            if (IsSolved)
            {
                Console.WriteLine("\tThis puzzle has already been solved.");
                return;
            }

            Console.WriteLine($"User input: '{command}'"); // Debugging

            // Correct command to solve the puzzle
            if (string.Equals(command.Trim(), "nmap -n -v -p- -A 192.126.98.10", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true; // Mark the puzzle as solved
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");

                // Provide progressively detailed hints after specific incorrect attempts
                if (attemptCount >= 1)
                {
                    Console.WriteLine("\tThe command needs to have these specifications:");
                    Console.WriteLine("\t- No domain resolution");
                    Console.WriteLine("\t- Verbose mode");
                    Console.WriteLine("\t- Scan ports 1-65535");
                    Console.WriteLine("\t- Conduct service enumeration, OS detection, and traceroute");
                }

                if (attemptCount >= 3)
                {
                    Console.WriteLine("\tHint: -n");
                }

                if (attemptCount >= 4)
                {
                    Console.WriteLine("\tHint: -v");
                }

                if (attemptCount >= 5)
                {
                    Console.WriteLine("\tHint: -p-");
                }

                if (attemptCount >= 6)
                {
                    Console.WriteLine("\tHint: -A");
                }
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0; // Reset the attempt count when starting or re-attempting the puzzle
        }
    }
}
