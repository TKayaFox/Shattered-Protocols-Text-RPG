using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Break : Room
    {
        public Room_Break()
        {
            Name = "Breakroom";
            Description = "temp description";
            RoomPuzzle = null;

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
        }
    }
}
