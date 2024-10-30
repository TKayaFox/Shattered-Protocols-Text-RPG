using System;

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
                decrypted.Append((char)((((c - d - shift) + 26) % 26) + d));
            }
            else
            {
                decrypted.Append(c);
            }
        }
        return decrypted.ToString();
    }
}
