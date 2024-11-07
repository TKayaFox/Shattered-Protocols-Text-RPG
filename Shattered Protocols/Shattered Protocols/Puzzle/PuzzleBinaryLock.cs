using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    // set a reset for when the player first encounters the puzzle
    public void ResetattemptCount()
    {
        int attemptCount = 0;
    }


    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `ItemRequired` for clarity if needed.
        public bool IsSolved { get; protected set; } = false;

        // Constructor to initialize description and required item
        protected Puzzle(string puzzleDescription, string itemRequired)
        {
            Description = puzzleDescription;
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

    public class PuzzleBinaryLock : Puzzle
    {
        private int attemptCount = 0;

        public PuzzleBinaryLock() : base("Solve the binary lock puzzle.", "Binary input") { }

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
                attemptCount++;
                Console.WriteLine("Incorrect, try again.");
                ProvideHint();
            }
        }

        // Reads the player's input and processes it
        public override void ReadCommand(string command, string remainder)
        {
            CheckInput(remainder);
        }

        // Hints starts here
        private void ProvideHint()
        {
            if (attemptCount == 2)
            {
                Console.WriteLine("Hint: The number 42 in binary is a 6-digit number.");
            }
            else if (attemptCount == 4)
            {
                Console.WriteLine("Hint: 42 in binary is made of alternating 1s and 0s.");
            }
        }

        private static bool CheckBinaryInput(string userInput, int correctNumber)
        {
            string correctBinary = Convert.ToString(correctNumber, 2);
            return userInput == correctBinary;
        }
    }
}
