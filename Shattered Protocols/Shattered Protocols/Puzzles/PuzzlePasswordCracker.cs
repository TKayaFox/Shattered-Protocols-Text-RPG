using System;
using System.Security.Cryptography;
using System.Text;

// Password Cracker Puzzle
// The user must crack a hashed password to gain access to the system.
// The password is a weak, commonly used password.
// Hints are provided after the 3rd incorrect attempt.
// The user has an unlimited number of attempts to solve the puzzle.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzlePasswordCracker : Puzzle
    {
        private readonly string hashedPassword; // Randomized hashed password
        private int attempts = 0;

        public PuzzlePasswordCracker() : base("\tCrack the system password.")
        {
            // Randomly select a password from a list of commonly used weak passwords
            var random = new Random();
            int passwordIndex = random.Next(1, 4); // Random index to choose from the list of passwords

            // Define a set of possible weak passwords
            string password = passwordIndex switch
            {
                1 => "password123",
                2 => "qwerty",
                3 => "123456",
                _ => "password123",
            };

            hashedPassword = GetSHA256Hash(password); // Get the SHA256 hash of the selected password
        }

        public override void Start()
        {
            // Check if the puzzle is already solved
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved. You can proceed further.");
                return;
            }

            ResetAttemptCount();

            // Puzzle intro
            GameController.Output(@"
    There is a door terminal keeping access to the Break Room that says, 
    “In pursuit of deterring the constant snack breaks, we put a simple lock here.” 
    Unfortunately, the password is a hash, so this simple password might be a bit 
    more complicated.
             ");
            GameController.Output(Description);
            GameController.Output("\tEnter the password that matches the given SHA256 hash.");
        }

        public override void ReadCommand(string command)
        {
            attempts++;

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
                GameController.Output("\tAccess denied. Try again.");
            }

            // Display a hint after 3 failed attempts
            if (attempts == 3)
            {
                GameController.Output("\tHint: The answer is a password that is commonly used and considered weak.");
            }
        }

        private bool CheckPassword(string input)
        {
            return GetSHA256Hash(input) == hashedPassword;
        }

        // Method to hash the input password
        // Returns the SHA256 hash of the input string
        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private void ResetAttemptCount()
        {
            attempts = 0;
        }
    }
}
