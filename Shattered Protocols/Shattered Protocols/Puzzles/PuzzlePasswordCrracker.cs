using System;
using System.Linq;
using System.Text;
using Shattered_Protocols.Puzzles;

// Password Cracker Puzzle
public class PuzzlePasswordCrracker : Puzzle
{
    private string hashedPassword = "5e88489da4b7..."; // SHA256 of "password123"
    private int attemptCount = 0;

    public PuzzlePasswordCrracker() : base("Crack the system password.", "Password attempt") { }

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