using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// Regex-Based Decryption (Development Labs)
// The user is required to decrypt data using Python-style regex patterns.
// The puzzle is solved when the user correctly filters the data to find the admin profile.
// Hints are provided after the 3rd and 4th incorrect attempts.
// The user has an unlimited number of attempts to solve the puzzle.

namespace Shattered_Protocols.Puzzles
{
    // Regex-Based Decryption (Development Labs)
    public class PuzzleRegex : Puzzle
    {
        private List<string> dataToFilter;
        private int attemptCount = 0;

        public PuzzleRegex() : base("\tDecrypt data using Python-style regex patterns.")
        {
            // Sample data that players will filter
            dataToFilter = new List<string>
            {
                "\tUser1: Alice - Role: Admin",
                "\tUser2: Bob - Role: User",
                "\tUser3: Carol - Role: Admin",
                "\tUser4: Dave - Role: User",
                "\tUser5: Eve - Role: Superuser"
            };
        }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            // Puzzle introduction
            GameController.Output(@"
    The lock heading to the Testing Room requires a profile with Admin rights to get through. 
    In the middle of the table, there seems to be a small computer full of names and passwords, 
    but there are way too many to comb through. Some kind of regular expression would help sort 
    out which profiles have admin passwords.
             ");
            ResetAttemptCount(); // Reset attempt count when revisiting the puzzle

            GameController.Output(Description);
            GameController.Output("\tYou have the following data to filter (use Python-style regex):\n");
            foreach (var item in dataToFilter)
            {
                GameController.Output(item);
            }
            GameController.Output("\n\tEnter a regex pattern to filter the data to find only admin:");
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
                GameController.Output("\tFiltered results:");
                foreach (var result in filteredResults)
                {
                    GameController.Output(result);
                }

                // Refined regex pattern to match "role: admin" with any amount of space before or after
                string adminPattern = @"role:\s*admin";
                if (filteredResults.Any(result => Regex.IsMatch(result, adminPattern, RegexOptions.IgnoreCase)))
                {
                    PuzzleSolved(@" 
    Once the profiles were filtered out, picking one and putting it into the door terminal was a piece of cake. 
    Time to go see what they were testing…");
                    IsSolved = true; // Mark the puzzle as solved
                }
                else
                {
                    GameController.Output("\tPattern not correct. Try again.");
                    // Provide hints after multiple incorrect attempts
                    if (attemptCount >= 3)
                    {
                        GameController.Output("\tHint: Try patterns that match specific user roles. Remember, Python-style regex is used.");
                    }
                }
            }
            else
            {
                GameController.Output("\tNo matches found. Try a different pattern.");
                // Provide hints after no matches
                if (attemptCount >= 4)
                {
                    GameController.Output("\tHint: Consider how roles are structured in the data. Python regex style is expected.");
                }
            }
        }

        public static List<string> FilterDataWithRegex(string pattern, string[] data)
        {
            List<string> matchedData = new List<string>();
            foreach (var item in data)
            {
                // Apply Python-style regex pattern for filtering
                if (Regex.IsMatch(item, pattern, RegexOptions.IgnoreCase))
                {
                    matchedData.Add(item);
                }
            }
            return matchedData;
        }

        private void ResetAttemptCount()
        {
            if (!IsSolved) // Only reset attempt count if the puzzle is not solved yet
            {
                attemptCount = 0; // Reset attempt count when starting or re-attempting the puzzle
            }
        }
    }
}
