using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// PuzzleCodeInjection is a puzzle that requires the user to input the correct terminal command to bypass a firewall.
// The puzzle is solved when the user inputs the correct terminal command.
// Hints are provided after the 2nd and 4th incorrect attempts.
// The user has an unlimited number of attempts to solve the puzzle.
// the answer is hard coded to "sudo firewall-bypass", but can be changed to any command if needed.
namespace Shattered_Protocols.Puzzles
{
    // Code Injection Puzzle (Server Room)
    public class PuzzleCodeInjection : Puzzle
    {
        private int attempts = 0;

        public PuzzleCodeInjection() : base("Bypass the firewall using a terminal command.", "Terminal command") { }

        public override void Start()
        {
            ResetattemptCount();
            Console.WriteLine(Description);
            Console.WriteLine("Enter the correct terminal command to bypass the firewall in Linux:"); ;
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            // correct command to bypass the firewall; can be changed to any command
            string correctCommand = "sudo firewall-bypass";

            if (command == correctCommand)
            {
                Console.WriteLine("Firewall bypassed! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect command.");
                GiveHint();
            }
        }

        // Hints are provided after the 2nd and 4th incorrect attempts.
        private void GiveHint()
        {
            if (AttemptCount == 2)
            {
                Console.WriteLine("Hint: The command requires elevated privileges.");
            }
            else if (AttemptCount == 4)
            {
                Console.WriteLine("Hint: Try using the 'sudo' command.");
            }
            else if (AttemptCount >= 6)
            {
                Console.WriteLine("You’ve tried multiple times. Think about how you would gain root access.");
            }
        }
    }
}
