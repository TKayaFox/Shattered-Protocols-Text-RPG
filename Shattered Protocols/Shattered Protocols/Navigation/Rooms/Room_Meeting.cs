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
        public Room_Meeting() : base(RoomType.Room_Meeting)
        {
            Name = "Meeting Room";
            Description = @"
    Welcome to the Meeting Room. Here, ideas were challenged, brains were stormed, 
    and presentations were slept through. There is one big table in the middle with 
    many chairs surrounding it. 
             ";
            RoomPuzzle = new PuzzleRegex();

            //Tell Puzzle to unlock all doors to THIS room
            RoomPuzzle.RoomUnlock = RoomType.Room_Meeting;

            //RoomUnlock Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            //  DirectionEnum relative to current roon, type of room
            NewDoor(DirectionEnum.North, RoomType.Room_Testing);
            NewDoor(DirectionEnum.West, RoomType.Room_Development);
            NewDoor(DirectionEnum.South, RoomType.Room_Start);

            //Lock appropriate rooms
            SetLock(DirectionEnum.North, true);
        }
    }
}
