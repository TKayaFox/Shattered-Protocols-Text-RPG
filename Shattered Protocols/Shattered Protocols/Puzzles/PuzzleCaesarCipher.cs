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
            //Puzzle intro
            GameController.Output(@"
             While the AI were learning object detection, they were also learning about password mechanisms. 
             The Ceaser Cypher is used on the lock on the door to the Break Room. This was a way for the AI 
             to crack the Ceaser Cypher with minimal documentation/information.
             ");
            ResetattemptCount();
            GameController.Output(Description);
            GameController.Output($"Encrypted Message: {encryptedMessage}");
            GameController.Output("Enter the correct decryption:");
        }

        public override void ReadCommand(string command)
        {
            AttemptCount ++;
            string correctDecryption = DecryptCaesar(encryptedMessage, shiftAmount);

            if (command.Trim().Equals(correctDecryption, StringComparison.OrdinalIgnoreCase))
            {
                GameController.Output("Correct! Puzzle solved.");
                //Puzzle Outro
                GameController.Output(@"
             You remember the good ol' days where you had the luxury to learn to make simple programs like “Hello World” and Ceaser Cyphers without robots trying to kill you non-stop. 
             A luxury you hope to reobtain after all this is over… Time to go to the Break Room.
             ");
                IsSolved = true;
            }
            else
            {
                GameController.Output("Incorrect. Try again.");

                if (AttemptCount >= 4)
                {
                    GameController.Output("Hint: The original message is a common greeting that is shifted 3 times. Not gonna tell you which way...");
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
