using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    // Caesar Cipher Puzzle
    // The user is given an encrypted message and must decrypt it using a Caesar cipher.
    // The user has an unlimited number of attempts to solve the puzzle.
    // The puzzle is solved when the user inputs the correct decryption.
    // The encrypted message is "Khoor Zruog" which is "Hello World" shifted by 3.
    // The user must decrypt the message by shifting it back by 3.
    // the string is hard coded, but can be changed to any string if needed.
    public class PuzzleCaesarCipher : Puzzle
    {
        private readonly string encryptedMessage = "Khoor Zruog"; // "Hello World" shifted by 3
        // the shit amount is also hard coded here, but we can fix it to a random number. If we do, we must also change the clue to give the correct hint.
        private readonly int shiftAmount = 3;
        private int attemptCount = 0;

        public PuzzleCaesarCipher() : base("Decrypt the Caesar ciphered message.", "Decryption input") { }
        public override void Start()
        {
            ResetattemptCount();
            Console.WriteLine(Description);
            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
            Console.WriteLine("Enter the correct decryption:");
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            string correctDecryption = DecryptCaesar(encryptedMessage, shiftAmount);

            if (command.Trim().Equals(correctDecryption, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                Console.WriteLine("Incorrect. Try again.");

                //Hint
                if (AttemptCount >= 4)
                {
                    Console.WriteLine("Hint: The original message is a common greeting that is shifted 3 times. Not gonna tell you which way...");
                }
            }
        }

        // cypher decryption method
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
