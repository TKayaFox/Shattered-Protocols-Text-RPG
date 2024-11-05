using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Server : Room
    {
        public Room_Server()
        {
            Name = "Server Room";
            Description = @"
             Welcome to the Server Room. Many server towers shadow over you as the lighting in this room was
             not well thought out. This is the lifeblood of how the AI would connect to other areas of the world.
             Even if you just blew this up, the AI would find a way to set back up with its already far-reaching
             influence.
             ";
            RoomPuzzle = new PuzzleCodeInjection();

            //Room Items
            Inventory = new Inventory();
        }

        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            //Load rooms if not yet loaded
            if (South == null)
            {
                South = new Room_Operations();
                South.North = this;
            }
            if (West == null)
            {
                West = new Room_Testing();
                West.East = this;
            }
        }
    }
}
