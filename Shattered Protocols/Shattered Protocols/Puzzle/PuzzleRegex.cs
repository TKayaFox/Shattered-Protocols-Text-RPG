using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    // set a reset for when the player first encounters the puzzle
    public void ResetattemptCount()
    {
        attemptCount = 0;
    }

    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; }
        public bool IsSolved { get; protected set; } = false;

        protected Puzzle(string puzzleDescription, string itemRequired)
        {
            Description = puzzleDescription;
            ItemRequired = itemRequired;
        }

        public abstract void Start();
        public abstract void ReadCommand(string command, string remainder);
    }

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
            Console.WriteLine(Description);
            Console.WriteLine("You have the following data to filter:");
            foreach (var item in dataToFilter)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enter a regex pattern to filter the data:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            attemptCount++;
            List<string> filteredResults = FilterDataWithRegex(remainder, dataToFilter.ToArray());

            if (filteredResults.Count > 0)
            {
                Console.WriteLine("Filtered results:");
                foreach (var result in filteredResults)
                {
                    Console.WriteLine(result);
                }

                // Check if the player guessed the correct pattern (this can be modified)
                if (remainder == "Admin") // Example correct pattern
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
