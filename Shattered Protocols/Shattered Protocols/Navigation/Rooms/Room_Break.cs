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
        public Room_Break() : base(RoomType.Room_Break)
        {
            Name = "Breakroom";
            Description = @"
    Welcome to the Break Room. A freezer full of frozen burritos and a microwave 
    stained with various food remains were previously used for refueling energy 
    deprived programmers. A couple of lockers are broken open in the corner of 
    the room. In one of the lockers is a flash drive in the shape of a rubber
    ducky! This must be important…
             ";
            RoomPuzzle = null;

            //Room Items
            Inventory = new Inventory();
            Item key = new Item("ducky_flash_drive",  "    This drive holds the access codes needed to shut doen the full system if I can just find the right place to use it!");
            Inventory.Add(key);

            //Define Neighboring Rooms
            //  DirectionEnum relative to current roon, type of room
            NewDoor(DirectionEnum.South, RoomType.Room_Development);
            NewDoor(DirectionEnum.East, RoomType.Room_Testing);

            //Lock appropriate rooms
            //If fix Puzzle for this room uncomment
            //SetLock(DirectionEnum.South, true);
            //SetLock(DirectionEnum.East, true);
        }
    }
}
