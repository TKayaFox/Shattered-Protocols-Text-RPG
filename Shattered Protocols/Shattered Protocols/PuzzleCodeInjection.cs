using System;

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
