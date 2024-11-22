using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Port Scan Puzzle
// The user is required to conduct a port scan using the nmap command.
// The puzzle is solved when the user inputs the correct nmap command.
// Hints are provided after the 1st, 2nd, 3rd, and 4th incorrect attempts.
// The user has an unlimited number of attempts to solve the puzzle.
// The correct command is "nmap -n -v -p- -A "not really an IP address"".
// each part of the command is checked individually before verifying the full command.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzlePortScan : Puzzle
    {
        private int attemptCount = 0;
        private List<string> userInputs = new List<string>(); // Track user's inputs for each part of the command
        private readonly string[] correctParts = { "-n", "-v", "-p-", "-a" }; // Correct parts of the command
        private readonly string fullCommand = "nmap -n -v -p- -a 192.126.98.10"; // Full correct command

        public PuzzlePortScan() : base("Conduct a port scan") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                Console.WriteLine("\tThis puzzle has already been solved. You don't need to solve it again.");
                return;
            }

            // Reset attempt count and user inputs when starting the puzzle for the first time
            ResetAttemptCount();
            userInputs.Clear();

            Console.WriteLine(Description);
            Console.WriteLine("\tEnter the first part of the command to conduct a thorough port scan at IP address 192.126.98.10:");
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

            // Trim and store the user input
            string userInput = command.Trim().ToLower();

            // Check if the user input matches the next part of the command
            if (userInput == correctParts[userInputs.Count])
            {
                // Correct input, add to the list
                userInputs.Add(userInput);
                Console.WriteLine("Correct part entered!");

                // If all parts have been entered, check the full command
                if (userInputs.Count == correctParts.Length)
                {
                    // Check if the full command is correct
                    if (string.Equals(string.Join(" ", userInputs) + " 192.126.98.10", fullCommand, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Access granted! Puzzle solved.");
                        IsSolved = true; // Mark the puzzle as solved
                    }
                    else
                    {
                        Console.WriteLine("Something is wrong with the command. Try again.");
                        ResetAttemptCount();
                        userInputs.Clear(); // Reset the inputs for the next attempt
                    }
                }
                else
                {
                    // Continue prompting for the next part of the command
                    Console.WriteLine($"Enter the next part of the command:");
                }
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Incorrect part of the command. Try again.");

                // Provide hints after specific incorrect attempts
                if (attemptCount >= 1)
                {
                    Console.WriteLine("\tHint: The first part of the command is for (no domain resolution).");
                }

                if (attemptCount >= 2)
                {
                    Console.WriteLine("\tHint: The second part is for (verbose mode).");
                }

                if (attemptCount >= 3)
                {
                    Console.WriteLine("\tHint: The third part is for (scan all ports).");
                }

                if (attemptCount >= 4)
                {
                    Console.WriteLine("\tHint: The last part is for (for OS and service detection).");
                }
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0; // Reset the attempt count when starting or re-attempting the puzzle
        }
    }
}
