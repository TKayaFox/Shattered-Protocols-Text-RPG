using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Testing : Room
    {
        public Room_Testing()
        {
            Name = "Testing Lab";
            Description = @"
             Welcome to the Testing Lab. Here, simulations of the behavior of the different 
             AI were tested. It is mostly empty with a bunch of cameras on the ceiling. 
             There are a couple of props in the corner. They were probably used so that the 
             AI could learn to recognize objects. 
             ";
            RoomPuzzle = new PuzzleCaesarCipher();

            //Room Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            //  Direction relative to current roon, type of room
            NewDoor(Direction.South, RoomType.Room_Meeting);
            NewDoor(Direction.East, RoomType.Room_Server);
            NewDoor(Direction.West, RoomType.Room_Break);

            //Lock appropriate rooms
            ToggleLock(Direction.South, true);
            ToggleLock(Direction.West, true);
            ToggleLock(Direction.East, true);
        }
    }
}
