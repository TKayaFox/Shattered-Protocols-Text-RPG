using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `ItemRequired` for clarity if needed.
        public bool IsSolved { get; protected set; } = false;

        // Constructor to initialize description and required item
        protected Puzzle(string description, string itemRequired)
        {
            Description = description;
            ItemRequired = itemRequired;
        }

        /// <summary>
        /// Starts the puzzle logic.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Reads player input and determines how best to handle it.
        /// </summary>
        public abstract void ReadCommand(string command, string remainder);
    }

    // Binary Lock Puzzle (Heart of Operations)
    public class BinaryLockPuzzle : Puzzle
    {
        private int failedAttempts = 0;

        public BinaryLockPuzzle() : base("Solve the binary lock puzzle.", "Binary input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter the binary representation of the number 42:");
        }

        private void CheckInput(string input)
        {
            if (CheckBinaryInput(input, 42))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                failedAttempts++;
                Console.WriteLine("Incorrect, try again.");
                if (failedAttempts == 2)
                {
                    Console.WriteLine("Hint: The number 42 in binary is a 6-digit number.");
                }
                else if (failedAttempts == 4)
                {
                    Console.WriteLine("Hint: 42 in binary is made of alternating 1s and 0s.");
                }
            }
        }

        public override void ReadCommand(string command, string remainder)
        {
            CheckInput(remainder);
        }

        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }
    }

    // Code Injection Puzzle (Server Room)
    public class CodeInjectionPuzzle : Puzzle
    {
        private int attempts = 0;

        public CodeInjectionPuzzle() : base("Bypass the firewall using a terminal command.", "Terminal command") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter the correct terminal command to bypass the firewall:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            attempts++;
            string correctCommand = "sudo firewall-bypass";

            if (command + " " + remainder == correctCommand)
            {
                Console.WriteLine("Firewall bypassed! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect command.");
                GiveHint();
            }
        }

        private void GiveHint()
        {
            if (attempts == 2)
            {
                Console.WriteLine("Hint: The command requires elevated privileges.");
            }
            else if (attempts == 4)
            {
                Console.WriteLine("Hint: Try using the 'sudo' command.");
            }
            else if (attempts >= 6)
            {
                Console.WriteLine("You’ve tried multiple times. Think about how you would gain root access.");
            }
        }
    }

    // Password Cracker Puzzle
    public class PasswordCrackerPuzzle : Puzzle
    {
        private string hashedPassword = "5e88489da4b7..."; // SHA256 of "password123"
        private int attemptCount = 0;

        public PasswordCrackerPuzzle() : base("Crack the system password.", "Password attempt") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Hint: The password is commonly used and matches the SHA256 hash.");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (attemptCount >= 3)
            {
                Console.WriteLine("Hint: Try a common password.");
            }

            if (CheckPassword(remainder))
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
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }

    // SQL Injection Puzzle
    public class SQLInjectionPuzzle : Puzzle
    {
        private int attemptCount = 0;

        public SQLInjectionPuzzle() : base("Bypass the SQL login check.", "SQL input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter SQL statement to access restricted information:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (remainder.Contains("1'='1") || remainder.Contains("' OR '1'='1"))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");
                if (attemptCount >= 2)
                {
                    Console.WriteLine("Hint: SQL injections are often used to force conditions to be true.");
                }
            }
        }
    }

    // Reverse String Puzzle
    public class ReverseStringPuzzle : Puzzle
    {
        private string encryptedMessage = "edoc terces";

        public ReverseStringPuzzle() : base("Decrypt the reversed message.", "Decryption input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (remainder == ReverseString(encryptedMessage))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");
            }
        }

        private string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    // Caesar Cipher Puzzle
    public class CaesarCipherPuzzle : Puzzle
    {
        private string encryptedMessage = "Khoor Zruog"; // "Hello World" shifted by 3
        private int shiftAmount = 3;

        public CaesarCipherPuzzle() : base("Decrypt the Caesar ciphered message.", "Decryption input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            if (remainder == DecryptCaesar(encryptedMessage, shiftAmount))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");
            }
        }

        private string DecryptCaesar(string input, int shift)
        {
            StringBuilder decrypted = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    decrypted.Append((char)((c - d - shift + 26) % 26 + d));
                }
                else
                {
                    decrypted.Append(c);
                }
            }
            return decrypted.ToString();
        }
    }
}
