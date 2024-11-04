using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Shattered_Protocols.Puzzles
{
    public class PuzzlePasswordCracker : Puzzle
    {
        private readonly string hashedPassword = "5e88489da4b7..."; // SHA256 of "password123"
        private int attemptCount = 0;

        public PuzzlePasswordCracker() : base("Crack the system password.", "Password attempt") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Hint: The password is commonly used and matches the SHA256 hash.");
        }

        public override void ReadCommand(string command)
        {
            if (attemptCount >= 3)
            {
                Console.WriteLine("Hint: Try a common password.");
            }

            if (CheckPassword(command))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");
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
