using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shattered_Protocols
{
    public abstract class Puzzle
    {
        #region Setters and Getters
        public int description
        {
            get => default;
            set
            {
            }
        }

        public int item
        {
            get => default;
            set
            {
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Puzzle()
        { }

        /// <summary>
        /// Starts Puzzle Logic
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Reads player input and determines how best to handle it
        /// </summary>
        /// <param name="command"></param>
        public void ReadCommand(string command, string remainder)
        {
            switch (command)
            {
                // Any invalid commands or not yet programmed commands
                default:
                    Console.WriteLine("Command not recognized. Type Help for a list of commands!");
                    break;
            }
        }

    }
}