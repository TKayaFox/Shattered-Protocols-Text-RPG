using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols.Puzzles
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public string ItemRequired { get; set; } // Consider renaming `ItemRequired` for clarity if needed.
        public bool IsSolved { get; protected set; } = false;
        public int AttemptCount { get; set; } = 0;

        // Constructor to initialize description and required item
        protected Puzzle(string description, string itemRequired)
        {
            Description = description;
            ItemRequired = itemRequired;
        }

        /// <summary>
        /// Starts the puzzle logic.
        /// </summary>
        public virtual void Start()
        {
            ResetattemptCount();
        }

        /// <summary>
        /// Reads player input and determines how best to handle it.
        /// </summary>
        public void ReadCommand(string command, string remainder)
        {
            //Unless there is an override for ReadCommand(string,string) then convert to ReadCommand(string) for simplicity
            string stringInput = "";

            if (remainder != null)
            {
                stringInput = $"{command} {remainder}";
            }
            else
            {
                stringInput = command;
            }

            //trim of blankspace after or before string
            stringInput = stringInput.Trim();

            ReadCommand(stringInput);
        }
        public abstract void ReadCommand(string stringInput);


        // set a reset for when the player first encounters the puzzle
        public void ResetattemptCount()
        {
            int attemptCount = 0;
        }
    }
}
