using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace Shattered_Protocols.Puzzles
{
    public class testpuzzle : Puzzle
    {
        private int attemptCount = 0;

        public PuzzleSQLInjection() : base("Conduct a port scan", "port scan") { }

        public override void Start()
        {
            ResetattemptCount();
            Console.WriteLine(Description);
            Console.WriteLine("Enter the command to conduct a thorough port scan at ip address 192.126.98.10:");
        }

        public override void ReadCommand(string command)
        {
            if (command.Contains("nmap -n -v -p- -A 192.126.98.10"))
            {
                Console.WriteLine("Access granted! Puzzle solved.");
                IsSolved = true;
            }
            else
            {
                attemptCount++;
                Console.WriteLine("Access denied. Try again.");
                if (attemptCount >= 1)
                {
                    Console.WriteLine("the command need to have these specifications:");
                    console.WriteLine("no domain resolution");
                    Console.WriteLine("verbose");
                    Console.WriteLine("modescan ports 1-65535");
                    Console.WriteLine("conduct service enumeration, OS detection, and traceroute");

                }
                if (attemptCount >= 3)
                {
                    Console.WriteLine("Hint: -n");
                }
                if (attemptCount >= 4)
                {
                    Console.WriteLine("Hint: -v");
                }
                if (attemptCount >= 5)
                {
                    Console.WriteLine("Hint: -p-");
                }
                if (attemptCount >= 6)
                {
                    Console.WriteLine("Hint: -A");
                }

            }
        }
    }
}
