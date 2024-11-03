using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    public class PuzzleBinaryLock : Puzzle
    {
        private int failedAttempts = 0;

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
                failedAttempts++;
                Console.WriteLine("Incorrect, try again.");
                ProvideHint();
            }
        }

        public override void ReadCommand(string command, string remainder)
        {
            CheckInput(remainder);
        }

        private void ProvideHint()
        {
            if (failedAttempts == 2)
            {
                Console.WriteLine("Hint: The number 42 in binary is a 6-digit number.");
            }
            else if (failedAttempts == 4)
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
