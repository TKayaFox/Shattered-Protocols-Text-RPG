using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untitled_Text_RPG.Navigation.Rooms;

namespace Shattered_Protocols.Navigation
{
    /// <summary>
    /// Linked List style Map management for Text RPG
    /// </summary>
    internal class Map
    {
        Room startRoom;
        Room currentRoom;
        public Room CurrentRoom { get => currentRoom; set => currentRoom = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Map()
        {
            //set Map current room as the start room
            //  Rooms currently add neighboring rooms for themselves. Edit: Move that to here
            startRoom = new Room_Start();
            currentRoom = startRoom;

            //Enter the Starting Room
            currentRoom.Enter();
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
                Console.WriteLine($"Entering new Room: {room.Name}");
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
