using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Puzzles;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Meeting : Room
    {
        public Room_Meeting()
        {
            Name = "Meeting Room";
            Description = @"
             Welcome to the Meeting Room. Here, ideas were challenged, brains were stormed, and
             presentations were slept through. There is one big table in the middle with many chairs surrounding
             it.
             ";
            RoomPuzzle = new PuzzleBinaryLock();

            //Room Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            North = RoomEnum.Room_Testing;
            South = RoomEnum.Room_Start;
            West = RoomEnum.Room_Development;
        }
    }
}
