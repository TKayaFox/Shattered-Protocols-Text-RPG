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

        public PuzzleCodeInjection() : base("\tBypass the firewall using a terminal command.") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }
            //Puzzle intro
            GameController.Output(@"
    The servers have firewalls in place to repel intruders from entering the Heart of Operations. 
    You are seen as a virus (and rightfully so). Find a way to bypass this terminal and become one 
    step closer to your goal.
             ");
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("\tEnter the correct terminal command to bypass the firewall in Linux:"); ;
        }

        public override void ReadCommand(string command)
        {
            AttemptCount++;
            // correct command to bypass the firewall; can be changed to any command
            string correctCommand = "sudo firewall-bypass";

            if (command == correctCommand)
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

        // Hints are provided after the 2nd and 4th incorrect attempts.
        private void GiveHint()
        {
            if (AttemptCount == 2)
            {
                GameController.Output("\tHint: The command requires elevated privileges.");
            }
            else if (AttemptCount == 4)
            {
                GameController.Output("\tHint: Try using the 'sudo' command.");
            }
            else if (AttemptCount >= 6)
            {
                GameController.Output("\tYou've tried multiple times. Think about how you would gain root access.");
            }
        }
    }
}
