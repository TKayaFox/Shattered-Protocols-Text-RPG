using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
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
            //  Direction relative to current roon, type of room
            NewDoor(Direction.North, RoomType.Room_Testing);
            NewDoor(Direction.South, RoomType.Room_Start);
            NewDoor(Direction.West, RoomType.Room_Development);

            //Lock appropriate rooms
            ToggleLock(Direction.North, true);
            ToggleLock(Direction.West, true);
        }
    }
}
