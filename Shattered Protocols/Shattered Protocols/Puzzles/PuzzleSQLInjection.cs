using Shattered_Protocols.Enumerations;
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

        public PuzzleSQLInjection() : base("Bypass the SQL login check.") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("This puzzle has already been solved. You can proceed further.");
                return;
            }
            //Puzzle intro
            GameController.Output(@"
             Finally, after all these puzzles… it's time to crack open this casing and end the tyranny 
             of the machines. You insert the key into the slot and a window pops up on the terminal asking 
             for the password. You really don't want to guess the password for hours, so it's time to apply 
             some hacks with your extensive knowledge in SQL. The inputs might not have been properly sanitized, 
             might as well as try smarter and not harder at first…
             ");
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter SQL statement to access restricted information:");
        }

        public override void ReadCommand(string command)
        {
            if (command.Contains("1'='1") || command.Contains("' OR '1'='1"))
            {
                PuzzleSolved("Access Granted!");
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

        //When problem solved, end the game
        public override void PuzzleSolved(string message)
        {
            GameController.Output(message);

            // End Game
            GameController.Publish(EventType.GameEnd, new EventArgs());
        }
    }
}
