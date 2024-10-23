using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `item` for clarity.
        public bool IsSolved { get; private set; } = false;

        // Constructor to initialize description and required item
        protected Puzzle(string description, string itemRequired)
        {
            Description = description;
            ItemRequired = itemRequired;
        }

        /// <summary>
        /// Starts the puzzle logic.
        /// </summary>
        public abstract void Start(); // Abstract method

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

            string input = Console.ReadLine();
            CheckInput(input);
        }

        private void CheckInput(string input)
        {
            if (CheckBinaryInput(input, 42))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                MarkAsSolved();
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
            // Handle specific commands, if necessary
        }

        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }
    }

    // Network Simulation Puzzle (Server Room)
    public class CodeInjectionPuzzle : Puzzle
    {
        private int attempts;

        // Constructor with a description and itemRequired
        public CodeInjectionPuzzle() : base("Bypass the firewall using a terminal command.", "Terminal command") 
        {
            attempts = 0; // Track the number of attempts
        }

        // Override Start to begin the puzzle
        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter the correct terminal command to bypass the firewall:");
        }

        // Read user input and compare it with the expected command
        public override void ReadCommand(string command, string remainder)
        {
            attempts++; // Increment attempts on every input

            // Simulated "correct" terminal command (this can be more complex based on the story)
            string correctCommand = "sudo firewall-bypass";

            // Check if user input matches the correct command
            if (command + " " + remainder == correctCommand)
            {
                Console.WriteLine("Firewall bypassed! Puzzle solved.");
                IsSolved = true; // Set the puzzle as solved
            }
            else
            {
                Console.WriteLine("Incorrect command.");
                GiveHint(); // Provide hints based on attempts
            }
        }

        // Provide a hint depending on the number of failed attempts
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
}

    // Regex-Based Decryption (Development Labs)
    public class RegexPuzzle : Puzzle
    {
        public RegexPuzzle() : base("Decrypt data using regex patterns.", "Regex pattern") { }

        public override void Start()
        {
            // Logic for filtering data with regex
            Console.WriteLine(Description);
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle regex commands
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

    // Hash Collision Puzzle (Testing Lab)
    public class HashCollisionPuzzle : Puzzle
    {
        public HashCollisionPuzzle() : base("Find hash collisions.", "Hash input") { }

        public override void Start()
        {
            // Logic for checking hash collisions
            Console.WriteLine(Description);
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle hash-related commands
        }

        public static string GetSHA256Hash(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool CheckHashCollision(string input1, string input2)
        {
            return GetSHA256Hash(input1) == GetSHA256Hash(input2);
        }
    }

    // Algorithm Optimization Challenge (Meeting Room)
    public class AlgorithmOptimizationPuzzle : Puzzle
    {
        public AlgorithmOptimizationPuzzle() : base("Optimize the algorithm using QuickSort.", "Sorting data") { }

        public override void Start()
        {
            // Logic for sorting data
            Console.WriteLine(Description);
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle sorting-related commands
        }

        public static int[] QuickSort(int[] data)
        {
            QuickSort(data, 0, data.Length - 1);
            return data;
        }

        private static void QuickSort(int[] data, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(data, left, right);
                QuickSort(data, left, pivot - 1);
                QuickSort(data, pivot + 1, right);
            }
        }

        private static int Partition(int[] data, int left, int right)
        {
            int pivot = data[right];
            int low = left - 1;

            for (int j = left; j < right; j++)
            {
                if (data[j] <= pivot)
                {
                    low++;
                    int temp = data[low];
                    data[low] = data[j];
                    data[j] = temp;
                }
            }

            int temp1 = data[low + 1];
            data[low + 1] = data[right];
            data[right] = temp1;

            return low + 1;
        }
    }

    // Turing Test (Testing Lab)
    public class TuringTestPuzzle : Puzzle
    {
        public TuringTestPuzzle() : base("Determine if the AI is sentient.", "User input") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            // Logic for Turing Test
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle Turing Test commands
        }

        public static string GetAIResponse(string userInput)
        {
            if (userInput.ToLower().Contains("logic"))
            {
                return "I am a logical being.";
            }
            else if (userInput.ToLower().Contains("human"))
            {
                return "What is it to be human?";
            }
            return "I cannot compute.";
        }

        public static bool IsSentient(string response)
        {
            return response.Contains("human") || response.Contains("being");
        }
    }

    // Memory Management Puzzle (Break Room)
    public class MemoryManagementPuzzle : Puzzle
    {
        public MemoryManagementPuzzle() : base("Manage memory allocation.", "Memory input") { }

        public override void Start()
        {
            // Logic for memory management
            Console.WriteLine(Description);
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle memory management commands
        }

        public static bool FreeMemory(string[] allocatedMemory, string memoryToFree)
        {
            for (int i = 0; i < allocatedMemory.Length; i++)
            {
                if (allocatedMemory[i] == memoryToFree)
                {
                    allocatedMemory[i] = null;
                    return true;
                }
            }
            return false;
        }
    }

    // Encryption Key in Source Code (Front Desks)
    public class SourceCodePuzzle : Puzzle
    {
        public SourceCodePuzzle() : base("Extract the encryption key from the source code.", "Source code") { }

        public override void Start()
        {
            // Logic for extracting the key
            Console.WriteLine(Description);
        }

        public override void ReadCommand(string command, string remainder)
        {
            // Handle extraction commands
        }

        public static string ExtractKeyFromSourceCode(string[] sourceCode)
        {
            foreach (string line in sourceCode)
            {
                if (line.Contains("encryption_key"))
                {
                    return line.Split('=')[1].Trim(); // Simple extraction logic
                }
            }
            return null; // Key not found
        }
    }
}
