using System;
using System.Text;
using System.Security.Cryptography;

namespace Shattered_Protocols
{
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

    public class PuzzlePasswordCracker : Puzzle
    {
        private readonly string hashedPassword = "ef92b778bafe771e89245b89ecbcfdaf24ecff4b6b28f2c23403e6e85c70f3a2"; // SHA256 of "password123"
        private int attemptCount = 0;

        public PuzzlePasswordCracker() : base("Crack the system password.", "Password attempt") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter the password that matches the given SHA256 hash.");
        }

        public override void ReadCommand(string command, string remainder)
        {
            attemptCount++;

            if (CheckPassword(remainder.Trim()))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Access denied. Try again.");

                // Display a hint after 3 failed attemptCount
                if (attemptCount >= 3)
                {
                    Console.WriteLine("Hint: The password is a commonly used weak password.");
                }
            }
        }

        private bool CheckPassword(string input)
        {
            return GetSHA256Hash(input) == hashedPassword;
        }

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
