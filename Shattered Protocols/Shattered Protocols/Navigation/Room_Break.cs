using Shattered_Protocols.Puzzles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Break : Room
    {
        public Room_Break()
        {
            Name = "Breakroom";
            Description = @"
             Welcome to the Break Room. A freezer full of frozen burritos and a microwave stained with various
             food remains were previously used for refueling energy deprived programmers. A couple of lockers
             are broken open in the corner of the room.
             ";
            RoomPuzzle = new PuzzleCaesarCipher();

            //Room Items
            Inventory = new Inventory();
            Item key = new Item("Emergency Shutdown Key", "This key should shut doen the full system if I can just find the right place to use it!");
            Inventory.Add(key);
        }

        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            //Load rooms if not yet loaded
            if (South == null)
            {
                South = new Room_Development();
                South.North = this;
            }
            //Load rooms if not yet loaded
            if (East == null)
            {
                East = new Room_Testing();
                East.West = this;
            }
        }
    }
}
