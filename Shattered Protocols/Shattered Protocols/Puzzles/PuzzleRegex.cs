using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    // Regex-Based Decryption (Development Labs)
    public class PuzzleRegex : Puzzle
    {
        private List<string> dataToFilter;
        private int attemptCount = 0;

        public PuzzleRegex() : base("Decrypt data using regex patterns.", "Regex pattern") 
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
            ResetattemptCount();

            Console.WriteLine(Description);
            Console.WriteLine("You have the following data to filter:");
            foreach (var item in dataToFilter)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enter a regex pattern to filter the data:");
        }

        public override void ReadCommand(string command)
        {
            attemptCount++;
            List<string> filteredResults = FilterDataWithRegex(command, dataToFilter.ToArray());

            if (filteredResults.Count > 0)
            {
                Console.WriteLine("Filtered results:");
                foreach (var result in filteredResults)
                {
                    Console.WriteLine(result);
                }

                // Check if the player guessed the correct pattern (this can be modified)
                if (command == "Admin") // Example correct pattern
                {
                    Console.WriteLine("Correct! Puzzle solved.");
                    IsSolved = true;
                }
                else
                {
                    Console.WriteLine("Pattern not correct. Try again.");
                    if (attemptCount >= 3)
                    {
                        Console.WriteLine("Hint: Try patterns that match specific user roles.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No matches found. Try a different pattern.");
                if (attemptCount >= 4)
                {
                    Console.WriteLine("Hint: Consider how roles are structured in the data.");
                }
            }
        }

        public static List<string> FilterDataWithRegex(string pattern, string[] data)
        {
            List<string> matchedData = new List<string>();
            foreach (var item in data)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(item, pattern))
                {
                    matchedData.Add(item);
                }
            }
            return matchedData;
        }
    }
}
