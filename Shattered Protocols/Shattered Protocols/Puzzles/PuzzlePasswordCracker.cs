using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


// PuzzlePasswordCracker is a puzzle that requires the user to input the correct password that matches the given SHA256 hash.
// The puzzle is solved when the user inputs the correct password.
// Hints are provided after the 3rd incorrect attempt.
// The user has an unlimited number of attempts to solve the puzzle.
// the answer is hard coded to "password123", but can be changed to any password if needed.
namespace Shattered_Protocols.Puzzles
{
    public class PuzzlePasswordCracker : Puzzle
    {
        // SHA256 of "password123"
        private readonly string hashedPassword = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f";

        public PuzzlePasswordCracker() : base("\tCrack the system password.") { }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }
            GameController.Output(@"
    There is a door terminal keeping access to the Break Room that says, 
    “In pursuit of deterring the constant snack breaks, we put a simple lock here.” 
    Unfortunately, the password is a hash, so this simple password might be a bit 
    more complicated.
             ");
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("\tEnter the password that matches the given SHA256 hash.");
        }

        // hints are provided after the 3rd incorrect attempt.
        public override void ReadCommand(string command)
        {
            AttemptCount++;

            if (CheckPassword(command.Trim()))
            {
                PuzzleSolved(@"
    Access Granted!
    This hash was no match for the skills of the top computer scientist in the Rebel Alliance. 
    Time to take a break in the break room!
             ");
            }
            else
            {
                AttemptCount++;
                GameController.Output("\tAccess denied. Try again.");
            }

            // Display a hint after 3 failed attemptCount
            if (AttemptCount >= 3)
            {
                GameController.Output("\tHint: The answer is a password is a commonly used weak password.");
            }
        }

        private bool CheckPassword(string input)
        {
            return GetSHA256Hash(input) == hashedPassword;
        }

        // method to hash the input password
        // returns the SHA256 hash of the input string
        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
