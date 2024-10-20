using Shattered_Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_Text_RPG.Navigation.Rooms
{
    internal class Room_Meeting : Room
    {
        public Room_Meeting()
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
            //Load rooms if not yet loaded
            if (South == null)
            {
                South = new Room_Start();
                South.North = this;
            }
            if (North == null)
            {
                North = new Room_Testing();
                North.South = this;
            }
            if (West == null)
            {
                West = new Room_Development();
                West.East = this;
            }
        }
    }
}
