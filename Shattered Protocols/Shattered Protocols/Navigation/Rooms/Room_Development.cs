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
        public Room_Development()
        {
            Name = "Development Labs";
            Description = @"
             Welcome to the Development Labs. Here, there are computers scattered across desks. The layers
             of dust are only matched by the layers of Cheeto powder across the keyboards. This room was used
             to make many general-purpose AI for the task of gathering information, and generating advice
             based on the data.
             ";
            RoomPuzzle = new PuzzleSQLInjection();

            //Room Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            //  Direction relative to current roon, type of room
            NewDoor(Direction.North, RoomType.Room_Break);
            NewDoor(Direction.East, RoomType.Room_Meeting);

            //Lock appropriate rooms
            ToggleLock(Direction.North, true);
            ToggleLock(Direction.East, true);
        }
    }
}
