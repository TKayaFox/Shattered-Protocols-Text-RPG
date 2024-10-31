using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `ItemRequired` for clarity if needed.
        public bool IsSolved { get; protected set; } = false;

        // Constructor to initialize description and required item
        protected Puzzle(string puzzleDescription, string requiredItem)
        {
            this.Description = puzzleDescription;
            this.ItemRequired = requiredItem;
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

    // Code Injection Puzzle (Server Room)
    public class PuzzleCodeInjection : Puzzle
    {
        private int attempts = 0;

        public PuzzleCodeInjection() : base("Bypass the firewall using a terminal command.", "Terminal command") { }

        public override void Start()
        {
            Console.WriteLine(Description);
            Console.WriteLine("Enter the correct terminal command to bypass the firewall:");
        }

        public override void ReadCommand(string command, string remainder)
        {
            attempts++;
            string correctCommand = "sudo firewall-bypass";

            if ($"{command} {remainder}" == correctCommand)
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
}
