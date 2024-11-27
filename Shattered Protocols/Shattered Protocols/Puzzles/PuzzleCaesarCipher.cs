using System;
using System.Text;

// Caesar Cipher Puzzle
    // The user is given an encrypted message and must decrypt it using a Caesar cipher.
    // The user has an unlimited number of attempts to solve the puzzle.
    // The puzzle is solved when the user inputs the correct decryption.
    // The encrypted message is "Khoor Zruog" which is "Hello World" shifted by 3.
    // The user must decrypt the message by shifting it back by 3.
    // the string is hard coded, but can be changed to any string if needed.

namespace Shattered_Protocols.Puzzles
{
    public class PuzzleCaesarCipher : Puzzle
    {
        private readonly string encryptedMessage; // Encrypted message
        private readonly int shiftAmount; // Randomized shift amount
        private int attemptCount = 0;

        public PuzzleCaesarCipher() : base("\tDecrypt the Caesar ciphered message.")
        {
            shiftAmount = 3;

            // Encrypt the message dynamically
            string plainMessage = "Hello World";
            encryptedMessage = EncryptCaesar(plainMessage, shiftAmount);
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
    While the AIs were learning object detection, they were also learning about password mechanisms. 
    The Caesar Cipher is used on the lock on the door to the Break Room (west). This was a way for the AIs 
    to crack the Caesar Cipher with minimal documentation/information.
            ");
            GameController.Output(Description);
            GameController.Output($"\tEncrypted Message: {encryptedMessage}");
            GameController.Output("\tEnter the correct decryption:");
        }

        public override void ReadCommand(string command)
        {
            // If the puzzle has already been solved, do not allow further input
            // This is to prevent the user from solving the puzzle multiple times
            if (IsSolved)
            {
                GameController.Output("\tThis puzzle has already been solved.");
                return;
            }

            attemptCount++;
            string correctDecryption = DecryptCaesar(encryptedMessage, shiftAmount);

            if (command.Trim().Equals(correctDecryption, StringComparison.OrdinalIgnoreCase))
            {
                PuzzleSolved(@"
    You remember the good ol' days where you had the luxury to learn to make simple 
    programs like “Hello World” and Caesar Ciphers without robots trying to kill you non-stop. 
    A luxury you hope to reobtain after all this is over… Time to go to the Break Room.
                ");
                IsSolved = true;
                ResetAttemptCount();
            }
            else
            {
                GameController.Output("\tIncorrect. Try again.");

                if (attemptCount == 2){
                    GameController.Output($"\tHint 1: The original message is a common greeting");
                }
                if (attemptCount >= 4)
                {
                    GameController.Output($"\tHint 2: The shift is {shiftAmount}.");
                }
            }
        }

        private void ResetAttemptCount()
        {
            attemptCount = 0;
        }

        private string EncryptCaesar(string input, int shift)
        {
            StringBuilder encrypted = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    encrypted.Append((char)((c - d + shift) % 26 + d));
                }
                else
                {
                    encrypted.Append(c);
                }
            }
            return encrypted.ToString();
        }

        // Decrypts a Caesar ciphered message
        // input: The encrypted message
        // shift: The shift amount used for encryption
        // Returns the decrypted message
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
