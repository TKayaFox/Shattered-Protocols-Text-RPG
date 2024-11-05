using Shattered_Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Puzzles;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Development : Room
    {
        public Room_Development()
        {
            Name = "Development Labs";
            Description = @"
             Welcome to the Development Labs. Here, there are computers scattered across desks. The layers
             of dust are only matched by the layers of Cheeto powder across the keyboards. This room was used
             to make many general-purpose AI for the task of gathering information, and generating advice
             based on the data.
             ";
            RoomPuzzle = new PuzzleSQLInjection();

            //Room Items
            Inventory = new Inventory();
        }

        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            if (East == null)
            {
                East = new Room_Meeting();
                East.West = this;
            }
            if (North == null)
            {
                North = new Room_Break();
                North.South = this;
            }
        }
    }
}
