using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Navigation
{
    /// <summary>
    /// Linked List style Map management for Text RPG
    /// </summary>
    internal class Map
    {
        Room startRoom;
        Room currentRoom;
        /// <summary>
        /// Constructor
        /// </summary>
        public Map()
        {
            //set Map current room as the start room
            startRoom = new Room();
            currentRoom = startRoom;

            //Add all Rooms to Map
            startRoom.North = new Room(); //etc
            //Edit with actual rooms when able
        }

        //Moves the current Room to another direction if able
        public void Move(Direction direction)
        {
            Room room = null;

            //Find appropriate room
            switch (direction)
            {
                case Direction.North:
                    room = currentRoom.North;
                    break;
                case Direction.South:
                    room = currentRoom.South;
                    break;
                case Direction.West:
                    room = currentRoom.West;
                    break;
                case Direction.East:
                    room = currentRoom.East;
                    break;
            }

            //Make sure room is valid
            if (room != null)
            {
                Console.WriteLine($"Entering new Room: {room.name}");
                currentRoom = room;
                room.Enter();
            }
            else
            {
                Console.WriteLine($"You cannot go this way");
            }
        }
    }
}
