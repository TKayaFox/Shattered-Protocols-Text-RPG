using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shattered_Protocols.Tests
{
    public class PuzzleRegexTest
    {
        public static void RunTests()
        {
            Console.WriteLine("Running tests for PuzzleRegex...");

            // Test Data
            var puzzle = new Puzzles.PuzzleRegex();

            // Test 1: Check if filtering works with correct regex
            string correctRegex = @"role:\s*admin";
            List<string> filteredData = puzzle.TestFilterDataWithRegex(correctRegex);
            Assert(filteredData.Count == 2, "Test 1 failed: Incorrect number of filtered results for correct regex.");
            Assert(filteredData.Contains("User1: Alice - Role: Admin"), "Test 1 failed: Missing User1 in filtered results.");
            Assert(filteredData.Contains("User3: Carol - Role: Admin"), "Test 1 failed: Missing User3 in filtered results.");

            // Test 2: Check if filtering works with incorrect regex
            string incorrectRegex = @"role:\s*user";
            filteredData = puzzle.TestFilterDataWithRegex(incorrectRegex);
            Assert(filteredData.Count == 2, "Test 2 failed: Incorrect number of filtered results for incorrect regex.");
            Assert(filteredData.Contains("User2: Bob - Role: User"), "Test 2 failed: Missing User2 in filtered results.");
            Assert(filteredData.Contains("User4: Dave - Role: User"), "Test 2 failed: Missing User4 in filtered results.");

            // Test 3: Check if no results are returned with a bad regex
            string badRegex = @"role:\s*manager";
            filteredData = puzzle.TestFilterDataWithRegex(badRegex);
            Assert(filteredData.Count == 0, "Test 3 failed: Results should be empty for unmatched regex.");

            // Test 4: Ensure PuzzleSolved is triggered correctly
            puzzle.Start(); // Reset the puzzle state
            puzzle.ReadCommand(correctRegex);
            Assert(puzzle.IsSolved, "Test 4 failed: Puzzle should be solved with correct regex input.");

            // Test 5: Ensure Puzzle remains solved when restarted
            puzzle.Start(); // Restart the puzzle
            Assert(puzzle.IsSolved, "Test 5 failed: Puzzle should remain solved after restarting.");

            Console.WriteLine("All tests passed for PuzzleRegex!");
        }

        // Helper assertion method
        private static void Assert(bool condition, string errorMessage)
        {
            if (!condition)
            {
                Console.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }

    // Extending PuzzleRegex for testing purposes
    public static class PuzzleRegexExtensions
    {
        public static List<string> TestFilterDataWithRegex(this Puzzles.PuzzleRegex puzzle, string pattern)
        {
            return Puzzles.PuzzleRegex.FilterDataWithRegex(pattern, new[]
            {
                "User1: Alice - Role: Admin",
                "User2: Bob - Role: User",
                "User3: Carol - Role: Admin",
                "User4: Dave - Role: User",
                "User5: Eve - Role: Superuser"
            });
        }
    }
}
