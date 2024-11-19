using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Puzzles;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Development : Room
    {
        public Room_Development() : base(RoomType.Room_Development)
        {
            Name = "Development Labs";
            Description = @"
             Welcome to the Development Labs. Here, there are computers scattered across desks. 
             The layers of dust are only matched by the layers of Cheeto powder across the keyboards. 
             This room was used to make many general-purpose AI for the task of gathering information, 
             and generating advice based on the data.
             ";
            RoomPuzzle = new PuzzlePasswordCracker();

            //Room Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            //  DirectionEnum relative to current roon, type of room
            NewDoor(DirectionEnum.North, RoomType.Room_Break);
            NewDoor(DirectionEnum.East, RoomType.Room_Meeting);

            //Lock appropriate rooms
            SetLock(DirectionEnum.North, true);
            SetLock(DirectionEnum.East, true);
        }
    }
}
