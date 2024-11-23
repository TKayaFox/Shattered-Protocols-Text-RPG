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
        private readonly string correctCommand; // Correct command to bypass the firewall

        public PuzzleCodeInjection() : base("\tBypass the firewall using a terminal command.")
        {
            // Set the correct command for the puzzle
            correctCommand = "sudo firewall-bypass";
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

            if (command.Trim().Equals(correctCommand, StringComparison.OrdinalIgnoreCase))
            {
                PuzzleSolved(@"
    The Firewalls are now down, and you may pass… Didn't even need a fire extinguisher.
                ");
                IsSolved = true;
                ResetAttemptCount();
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

        // Hints are provided after the 2nd, 4th, and 6th incorrect attempts.
        private void GiveHint()
        {
            if (attempts == 2)
            {
                GameController.Output("\tHint: The command requires elevated privileges (think root access).");
            }
            else if (attempts == 4)
            {
                GameController.Output("\tHint: Try using 'sudo' at the start of your command.");
            }
            else if (attempts >= 6)
            {
                GameController.Output("\tHint: You're bypassing a firewall. Consider common network-related commands.");
            }
        }
    }
}