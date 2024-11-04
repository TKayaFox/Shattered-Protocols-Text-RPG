using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Server : Room
    {
        public Room_Server()
        {
            Name = "Server Room";
            Description = "temp description";
            RoomPuzzle = new PuzzleCodeInjection();

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
                South = new Room_Operations();
                South.North = this;
            }
            if (West == null)
            {
                West = new Room_Testing();
                West.East = this;
            }
        }
    }
}
