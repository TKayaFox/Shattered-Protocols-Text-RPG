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

        public PuzzlePasswordCracker() : base("Crack the system password.", "Password attempt") { }

        public override void Start()
        {
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output("Enter the password that matches the given SHA256 hash.");
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;

            if (CheckPassword(command.Trim()))
            {
                GameController.Output("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                AttemptCount ++;
                GameController.Output("Access denied. Try again.");
            }

            // Display a hint after 3 failed attemptCount
            if (AttemptCount >= 3)
            {
                GameController.Output("Hint: The password is a commonly used weak password.");
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
