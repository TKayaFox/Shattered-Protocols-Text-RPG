using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    // Code Injection Puzzle (Server Room)
    public class PuzzleCodeInjection : Puzzle
    {
        private int attempts = 0;
        private readonly string correctCommand; // Randomized command to bypass the firewall

        public PuzzleCodeInjection() : base("\tBypass the firewall using a terminal command.")
        {
            // Randomly select a command from a set of possible commands
            var random = new Random();
            int commandIndex = random.Next(1, 4); // Random index to choose from the list of commands

            // Define a set of possible commands
            switch (commandIndex)
            {
                case 1:
                    correctCommand = "sudo firewall-bypass";
                    break;
                case 2:
                    correctCommand = "sudo ufw disable";
                    break;
                case 3:
                    correctCommand = "sudo systemctl stop firewall";
                    break;
                default:
                    correctCommand = "sudo firewall-bypass";
                    break;
            }
        }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            ResetAttemptCount();

            // Puzzle intro
            GameController.Output(@"
    The servers have firewalls in place to repel intruders from entering the Heart of Operations. 
    You are seen as a virus (and rightfully so). Find a way to bypass this terminal and become one 
    step closer to your goal.
             ");
            GameController.Output(Description);
            GameController.Output("\tEnter the correct terminal command to bypass the firewall in Linux:");
        }

        public override void ReadCommand(string command)
        {
            // If the puzzle has been solved, exit early
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved.");
                return;
            }

            attempts++;

            if (command.Trim() == correctCommand)
            {
                PuzzleSolved(@"
    The Firewalls are now down, and you may pass… Didn't even need a fire extinguisher.
                 ");
            }
            else
            {
                GameController.Output("\tIncorrect command.");
                GiveHint();
            }
        }

        private void ResetAttemptCount()
        {
            attempts = 0;
        }

        // Hints are provided after the 2nd and 4th incorrect attempts.
        private void GiveHint()
        {
            if (attempts == 2)
            {
                GameController.Output("\tHint: The command requires elevated privileges.");
            }
            else if (attempts == 4)
            {
                GameController.Output("\tHint: Try using the 'sudo' command.");
            }
            else if (attempts >= 6)
            {
                GameController.Output("\tYou've tried multiple times. Think about how you would gain root access.");
            }
        }
    }
}
