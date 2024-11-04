using Shattered_Protocols;
using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Testing : Room
    {
        public Room_Testing()
        {
            Name = "Testing Room";
            Description = "temp description";
            RoomPuzzle = new PuzzleReverseString();

            //Room Items
            Inventory = new Inventory();
        }

        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            //Load rooms if not yet loaded
            if (South == null)
            {
                South = new Room_Testing();
                South.North = this;
            }
            if (East == null)
            {
                East = new Room_Break();
                East.West = this;
            }
            if (West == null)
            {
                West = new Room_Server();
                West.East = this;
            }
        }
    }
}
