using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Puzzles;
using static System.Net.Mime.MediaTypeNames;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Start : Room
    {
        public Room_Start() : base(RoomType.Room_Start)
        {
            Name = "Front Desks";
            Description = @"
             Welcome to the Front Desks. There are empty chairs where receptionists would welcome guests. 
             There are computers with an accompanying phone on each one. Calling for help would be pointless here; 
             you are on your own… also, the phones probably don't work anymore.
             ";
            RoomPuzzle = new PuzzleBinaryLock();

            //Room Items
            Inventory = new Inventory();

            //Define Neighboring Rooms
            //  Direction relative to current roon, type of room
            NewDoor(Direction.North, RoomType.Room_Meeting);

            //Lock appropriate rooms
            ToggleLock(Direction.North, true);
            ToggleLock(Direction.South, true);
            ToggleLock(Direction.West, true);
            ToggleLock(Direction.East, true);
        }
    }
}
