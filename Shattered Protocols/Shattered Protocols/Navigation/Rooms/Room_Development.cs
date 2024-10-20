using Shattered_Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_Text_RPG.Navigation.Rooms
{
    internal class Room_Development : Room
    {
        public Room_Development()
        {
            Name = "Meeting Room";
            Description = "Here ideas were challenged, brains were stormed, and presentations were slept through. There is one big table in the middle with many chairs surrounding it.";
            RoomPuzzle = null;

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
        }
    }
}
