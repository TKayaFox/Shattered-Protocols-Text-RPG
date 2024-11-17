using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// Regex-Based Decryption (Development Labs)
// The user is given a list of data entries and must filter the data using a Python-style regex pattern.
// The user has an unlimited number of attempts to solve the puzzle.
// The puzzle is solved when the user inputs a regex pattern that filters the data to reveal the answer.
// The answer is "Admin" as a role in the data entries. 
// The user input is case-insensitive.
namespace Shattered_Protocols.Puzzles
{
    // Regex-Based Decryption (Development Labs)
    public class PuzzleRegex : Puzzle
    {
        private List<string> dataToFilter;
        private int attemptCount = 0;

        public PuzzleRegex() : base("Decrypt data using Python-style regex patterns.")
        {
            // Sample data that players will filter
            dataToFilter = new List<string>
            {
                "User1: Alice - Role: Admin",
                "User2: Bob - Role: User",
                "User3: Carol - Role: Admin",
                "User4: Dave - Role: User",
                "User5: Eve - Role: Superuser"
            };
        }

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
             The lock heading to the Testing Room requires a profile with Admin rights to get through. 
             In the middle of the table, there seems to be a small computer full of names and passwords, 
             but there are way too many to comb through. Some kind of regular expression would help sort 
             out which profiles have admin passwords.
             ");
            ResetattemptCount();

            GameController.Output(Description);
            GameController.Output("You have the following data to filter (use Python-style regex):");
            foreach (var item in dataToFilter)
            {
                GameController.Output(item);
            }
            GameController.Output("Enter a regex pattern to filter the data to find only admin:");
        }

        public override void ReadCommand(string command)
        {
            attemptCount++;

            // Convert command to lowercase for case-insensitive matching
            command = command.ToLower();

            // Filter data with regex pattern
            List<string> filteredResults = FilterDataWithRegex(command, dataToFilter.ToArray());

            if (filteredResults.Count > 0)
            {
                GameController.Output("Filtered results:");
                foreach (var result in filteredResults)
                {
                    GameController.Output(result);
                }

                // Check if any filtered result contains "admin" as the answer
                // Python regex pattern to match admin role
                // The pattern is case-insensitive
                string adminPattern = @"role:\s*admin";
                if (filteredResults.Any(result => result.ToLower().Contains(adminPattern)))
                {
                    PuzzleSolved(@"
                 Once the profiles were filtered out, picking one and putting it into the door terminal was a piece of cake. 
                 Time to go see what they were testing…
                 ");
                    IsSolved = true;
                }
                else
                {
                    GameController.Output("Pattern not correct. Try again.");
                    if (attemptCount >= 3)
                    {
                        GameController.Output("Hint: Try patterns that match specific user roles. Remember, Python-style regex is used.");
                    }
                }
            }
            else
            {
                GameController.Output("No matches found. Try a different pattern.");
                if (attemptCount >= 4)
                {
                    GameController.Output("Hint: Consider how roles are structured in the data. Python regex style is expected.");
                }
            }
        }

        public static List<string> FilterDataWithRegex(string pattern, string[] data)
        {
            List<string> matchedData = new List<string>();
            foreach (var item in data)
            {
                // Apply Python-style regex pattern for filtering
                if (System.Text.RegularExpressions.Regex.IsMatch(item, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    matchedData.Add(item);
                }
            }
            return matchedData;
        }
    }
}
