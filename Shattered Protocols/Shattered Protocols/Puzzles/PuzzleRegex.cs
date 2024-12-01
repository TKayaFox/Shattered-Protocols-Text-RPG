using System;
using System.Collections.Generic;
using System.Data;
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

        // Multiple Correct regex pattern required to solve the puzzle one being .*admin and the other being admin$
        private string correctRegexPattern1 = ".*admin";
        private string correctRegexPattern2 = "admin$";



        public PuzzleRegex() : base("\tDecrypt data using Python-style regex patterns.")
        {
            // Sample data that players will filter
            dataToFilter = new List<string>
            {
                "\tUser1: Alice - Role: admin",
                "\tUser2: Bob - Role: user",
                "\tUser3: Carol - Role: admin",
                "\tUser4: Dave - Role: user",
                "\tUser5: Eve - Role: superuser"
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
    The lock heading to the Testing Lab (north) requires a profile with Admin rights to get through. 
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
            GameController.Output("\tExample: Pattern = r(Your pattern here)");
        }

        public override void ReadCommand(string command)
        {
            attemptCount++;

            // Check if the entered regex matches the required pattern
            // Check if the entered regex matches one of the valid patterns
            if (command.Trim().Equals(correctRegexPattern1, StringComparison.OrdinalIgnoreCase) ||
                command.Trim().Equals(correctRegexPattern2, StringComparison.OrdinalIgnoreCase))
            { }
            // Use the pattern to filter the data
            List<string> filteredResults = FilterDataWithRegex(command, dataToFilter.ToArray());

            // Display the filtered results
            GameController.Output("\tFiltered results:");
            foreach (var result in filteredResults)
            {
                GameController.Output(result);
            }

            // Ensure the filtered results contain only admin roles
            // Check if the entered regex matches one of the valid patterns
            if (command.Trim().Equals(correctRegexPattern1, StringComparison.OrdinalIgnoreCase) ||
                command.Trim().Equals(correctRegexPattern2, StringComparison.OrdinalIgnoreCase))

            {
                PuzzleSolved(@"
    Once the profiles were filtered out, picking one and putting it into the door terminal was a piece of cake. 
    Time to go see what they were testing…
                    ");
                IsSolved = true; // Mark the puzzle as solved
                return;
            }
            else
            {
                // If the pattern is incorrect or filtered results don't match
                GameController.Output("\tPattern not correct or did not filter correctly. Try again.");
                ProvideHint();
            }
        }





        private void ProvideHint(string command = "")
        {
            // Check if the user entered "admin" literally
            if (command.Trim().Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                GameController.Output("\tHint: While 'admin' works as a literal match, remember to use regex-specific characters like .* or $ to make it more versatile.");
                return; // Skip other hints for this specific case
            }

            // Progressive hint system based on the attempt count
            if (attemptCount == 3)
            {
                GameController.Output("\tHint 1: Focus on removing characters.");
            }
            else if (attemptCount == 4)
            {
                GameController.Output("\tHint 2: Try removing characters except for what you are searching for. Consider spaces.");
            }
            else if (attemptCount == 5)
            {
                GameController.Output("\tHint 3: Remember what * means in regex. It means any number of the previous character.");
            }
            else if (attemptCount == 6)
            {
                GameController.Output("\tHint 4: Remember what . means in regex. It can match any character except a newline.");
            }
            else if (attemptCount >= 7)
            {
                GameController.Output("\tHint 5: Think carefully about where to put . and * in the pattern.");
            }
        }

        // Filter data using the provided regex pattern
        // Using the built in Regex class in C#
        // Returns a list of matched data
        public static List<string> FilterDataWithRegex(string pattern, string[] data)
        {
            List<string> matchedData = new List<string>();
            foreach (var item in data)
            {
                // Apply the regex pattern for filtering
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
