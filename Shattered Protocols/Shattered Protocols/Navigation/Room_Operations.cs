using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Operations : Room
    {
        public Room_Operations()
        {
            Name = "Heart of Operations";
            Description = "temp description";
            RoomPuzzle = new PuzzlePasswordCracker();

            //Room Items
            Inventory = new Inventory();
        }

        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            //Load rooms if not yet loaded
            if (North == null)
            {
                North = new Room_Testing();
                North.South = this;
            }
        }
    }
}
