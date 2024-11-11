using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    // SQL Injection Puzzle
    public class PuzzleSQLInjection : Puzzle
    {
        private int attemptCount = 0;

        public PuzzleSQLInjection() : base("Bypass the SQL login check.", "SQL input") { }

        public override void Start()
        {
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter SQL statement to access restricted information:");
        }

        public override void ReadCommand(string command)
        {
            if (command.Contains("1'='1") || command.Contains("' OR '1'='1"))
            {
                GameController.Output("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                attemptCount++;
                GameController.Output("Access denied. Try again.");
                if (attemptCount >= 2)
                {
                    GameController.Output("Hint: SQL injections are often used to force conditions to be true.");
                }
            }
        }
    }
}
