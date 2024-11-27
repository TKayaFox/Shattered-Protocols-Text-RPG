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
        public Room_Testing() : base(RoomType.Room_Testing)
        {
            Name = "Testing Lab";
            Description = @"
    Welcome to the Testing Lab. Here, simulations of the behavior of the different 
    AIs were tested. It is mostly empty with a bunch of cameras on the ceiling. 
    There are a couple of props in the corner. They were probably used so that the 
    AIs could learn to recognize objects. 
             ";
            RoomPuzzle = new PuzzleCaesarCipher();

            //Tell Puzzle to unlock all doors to THIS room
            RoomPuzzle.RoomUnlock = RoomType.Room_Testing;

            //Define Neighboring Rooms
            //  DirectionEnum relative to current roon, type of room
            NewDoor(DirectionEnum.South, RoomType.Room_Meeting);
            NewDoor(DirectionEnum.East, RoomType.Room_Server);
            NewDoor(DirectionEnum.West, RoomType.Room_Break);

            //Lock appropriate rooms
            SetLock(DirectionEnum.West, true);
        }
    }
}
