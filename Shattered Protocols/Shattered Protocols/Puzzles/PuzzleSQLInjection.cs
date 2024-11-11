using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// SQL Injection Puzzle 
// The user is required to bypass the SQL login check by entering a specific SQL statement. 
// The puzzle is solved when the user inputs the correct SQL statement.
// Hints are provided after the 2nd incorrect attempt.
// The user has an unlimited number of attempts to solve the puzzle.
// The answer is "1'='1" or "' OR '1'='1" to bypass the login check.
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
            Console.WriteLine(Description);
            Console.WriteLine("Enter SQL statement to access restricted information:");
        }

        public override void ReadCommand(string command)
        {
            if (command.Contains("1'='1") || command.Contains("' OR '1'='1"))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");
                if (attemptCount >= 2)
                {
                    Console.WriteLine("Hint: SQL injections are often used to force conditions to be true.");
                }
            }
        }
    }
}
