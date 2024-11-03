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

        // Constructor to initialize description and required item
        protected Puzzle(string description, string itemRequired)
        {
            Description = description;
            ItemRequired = itemRequired;
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
}
