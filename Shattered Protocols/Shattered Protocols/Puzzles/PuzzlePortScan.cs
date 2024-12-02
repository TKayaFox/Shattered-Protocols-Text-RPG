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
        private readonly string[] correctParts = { "-n", "-v", "-p-", "-A" }; // Correct parts of the command
        private readonly string fullCommand = "nmap -n -v -p- -A 192.126.98.10"; // Full correct command
        private bool awaitingFullCommand = false; // Tracks if the puzzle is waiting for the full command

        public PuzzlePortScan() : base("\tConduct a port scan") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                Console.WriteLine("\tThis puzzle has already been solved. You don't need to solve it again.");
                return;
            }

            // Puzzle intro
            GameController.Output(@"
    There is a door terminal keeping access to the Break Room (north) that says, 
    “In pursuit of deterring the constant snack breaks, we put a simple lock here.” 
    Unfortunately, the “password” is actually a port scan, so this simple password 
    might be a bit more complicated. Seems you have to “identify” if the port to 
    the breakroom is “open”… what a bunch of nerds…
            ");

            // Reset state when starting the puzzle for the first time
            ResetAttemptCount();
            userInputs.Clear();
            awaitingFullCommand = false;

            Console.WriteLine(Description);
            // Specific and detailed instruction for the puzzle
            GameController.Output(@"
    We need to perform the following nmap scan:
      1. No DNS resolution
      2. Verbose output
      3. Scan all 65,535 ports
      4. Detect operating system and services

    Enter the first part of the nmap command to conduct this thorough scan.
    The target IP address is: 192.126.98.10

    Example: nmap <your arguments here> 192.126.98.10");
        }

        public override void ReadCommand(string command)
        {
            // If the puzzle has been solved, exit early
            if (IsSolved)
            {
                Console.WriteLine("\tThis puzzle has already been solved.");
                return;
            }

            Console.WriteLine($"\tUser input: '{command}'");

            string userInput = command.Trim();

            if (awaitingFullCommand)
            {
                ValidateFullCommand(userInput);
                return;
            }

            // Check if the user input matches the next part of the command (case-insensitive)
            // If correct, add the input to the list and prompt for the next part
            // If incorrect, provide hints after specific incorrect attempts
            if (string.Equals(userInput, correctParts[userInputs.Count], StringComparison.OrdinalIgnoreCase))
            {
                userInputs.Add(userInput);
                Console.WriteLine("\tCorrect part entered!");

                if (userInputs.Count == correctParts.Length)
                {
                    awaitingFullCommand = true;
                    Console.WriteLine("\tNow, enter the full command in one line:");
                }
                else
                {
                    // Modify the prompt to be more specific after each correct part
                    switch (userInputs.Count)
                    {
                        case 1:
                            Console.WriteLine("\tCorrect! Enter the second part, which is verbose mode:");
                            break;
                        case 2:
                            Console.WriteLine("\tCorrect! Enter the third part, which is scan ALL ports:");
                            break;
                        case 3:
                            Console.WriteLine("\tCorrect! Enter the last part, which is OS AND service detection together:");
                            break;
                    }
                }
            }
            else
            {
                attemptCount++;
                Console.WriteLine("\tIncorrect part of the command. Try again.");

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
                    Console.WriteLine("\tHint: The third part is for (scan ALL ports).");
                }

                if (attemptCount >= 4)
                {
                    Console.WriteLine("\tHint: The last part is for command for (OS AND service detection) together.");
                }
            }
        }


        // Validate the full command entered by the user
        // User must enter the full command in one line
        private void ValidateFullCommand(string command)
        {
            string trimmedCommand = command.Trim();

            if (string.Equals(trimmedCommand, fullCommand, StringComparison.OrdinalIgnoreCase))
            {
                // Puzzle outro
                PuzzleSolved(@"
    PORT     STATE    SERVICE
    21/tcp   open     FTP (File Transfer Protocol is ready for user to be uploaded into Break Room)

    Wow… too much thought was put into this lock. Anyway, this port scan was no match for the skills 
    of the top computer scientist in the Rebel Alliance. Time to take a break in the break room!
                ");
                IsSolved = true; // Mark the puzzle as solved
                ResetAttemptCount();
            }
            else
            {
                Console.WriteLine("\tThe full command is incorrect. Try again.");
                Console.WriteLine("\tHint: The full command starts with 'nmap' and ends with the target IP address(192.126.98.10). The commands you found earlier include -p-, -v, -A, -n, but not necessarily in that order.");
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0; // Reset the attempt count when starting or re-attempting the puzzle
        }
    }
}