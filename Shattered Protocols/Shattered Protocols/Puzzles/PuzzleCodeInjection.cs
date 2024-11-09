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
            Console.WriteLine(Description);
            Console.WriteLine("Enter the correct terminal command to bypass the firewall in Linux:"); ;
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
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
