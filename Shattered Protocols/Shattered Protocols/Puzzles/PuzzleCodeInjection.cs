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

        public PuzzleCodeInjection() : base("Bypass the firewall using a terminal command.", "Terminal command") { }

        public override void Start()
        {
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter the correct terminal command to bypass the firewall in Linux:"); ;
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            string correctCommand = "sudo firewall-bypass";

            if (command == correctCommand)
            {
                GameController.Output("Firewall bypassed! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                GameController.Output("Incorrect command.");
                GiveHint();
            }
        }

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
