using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Puzzles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Break : Room
    {
        public Room_Break()
        {
            Name = "Breakroom";
            Description = @"
             Welcome to the Break Room. A freezer full of frozen burritos and a microwave 
             stained with various food remains were previously used for refueling energy 
             deprived programmers. A couple of lockers are broken open in the corner of 
             the room. In one of the lockers is a key! This must be important…
             ";
            RoomPuzzle = null;

            //Room Items
            Inventory = new Inventory();
            Item key = new Item("flashdrive", "This drive holds the access codes needed to shut doen the full system if I can just find the right place to use it!");
            Inventory.Add(key);

            //Define Neighboring Rooms
            //  Direction relative to current roon, type of room
            NewDoor(Direction.South, RoomType.Room_Development);
            NewDoor(Direction.East, RoomType.Room_Testing);

            //Lock appropriate rooms
            ToggleLock(Direction.North, true);
            ToggleLock(Direction.East, true);
        }
    }
}
