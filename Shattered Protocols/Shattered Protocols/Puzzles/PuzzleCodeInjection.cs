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
            //Puzzle intro
            GameController.Output(@"
             The servers have firewalls in place to repel intruders from entering the Heart of Operations. 
             You are seen as a virus (and rightfully so). Find a way to bypass this terminal and become one 
             step closer to your goal.
             ");
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter the correct terminal command to bypass the firewall in Linux:"); ;
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            // correct command to bypass the firewall; can be changed to any command
            string correctCommand = "sudo firewall-bypass";

            if (command == correctCommand)
            {
                GameController.Output("Firewall bypassed! Puzzle solved.");
                //Puzzle Outro
                GameController.Output(@"
             The Firewalls are now down, and you may pass… Didn't even need a fire extinguisher.
             ");
                IsSolved = true;
            }
            else
            {
                GameController.Output("Incorrect command.");
                GiveHint();
            }
        }

        // Hints are provided after the 2nd and 4th incorrect attempts.
        private void GiveHint()
        {
            if (AttemptCount == 2)
            {
                GameController.Output("Hint: The command requires elevated privileges.");
            }
            else if (AttemptCount == 4)
            {
                GameController.Output("Hint: Try using the 'sudo' command.");
            }
            else if (AttemptCount >= 6)
            {
                GameController.Output("You’ve tried multiple times. Think about how you would gain root access.");
            }
        }
    }
}
