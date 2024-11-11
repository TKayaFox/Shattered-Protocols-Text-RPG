using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management.Args;
using Shattered_Protocols.Navigation.Rooms;

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
        private Dictionary<RoomType, Room>
            roomDictionary = new Dictionary<RoomType, Room>();
        private Dictionary<RoomType, Func<Room>>
            RoomConstructorDictionary = new Dictionary<RoomType, Func<Room>>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Map()
        {
            //Get all needed room constructors into dictionary
            InitializeConstructorLibrary();

            //Set initial room as the current room
            currentRoom = FindRoomReference(RoomType.Room_Start);

            //Enter the Starting Room
            currentRoom.Enter();
        }

        /// <summary>
        /// Adds Room Constructor Methods to dictionary
        /// </summary>
        private void InitializeConstructorLibrary()
        {
            //Build Room constructor dictionary
            RoomConstructorDictionary.Add(RoomType.Room_Break, () => new Room_Break());
            RoomConstructorDictionary.Add(RoomType.Room_Development, () => new Room_Development());
            RoomConstructorDictionary.Add(RoomType.Room_Meeting, () => new Room_Meeting());
            RoomConstructorDictionary.Add(RoomType.Room_Operations, () => new Room_Operations());
            RoomConstructorDictionary.Add(RoomType.Room_Server, () => new Room_Server());
            RoomConstructorDictionary.Add(RoomType.Room_Start, () => new Room_Start());
            RoomConstructorDictionary.Add(RoomType.Room_Testing, () => new Room_Testing());
        }

        #region Map Navigation
        //Moves the current Room to another direction if able
        public void Move(Direction direction)
        {
            //Determine what roomtype to load
            RoomType roomType = currentRoom.UseDoor(direction);

            if (roomType == RoomType.Locked)
            {
                GameController.Output($"This door is locked. You cannot go this way!");
            }
            else if (roomType == RoomType.Null)
            {
                GameController.Output($"You cannot go this way!");
            }
            else
                { 

                //Get the reference to the correct room
                Room room = FindRoomReference(roomType);

                //Make sure room is valid
                if (room != null)
                {
                    GameController.Output($"Entering new Room: {room.Name}");
                    currentRoom = room;
                    room.Enter();
                }
            }
        }

        private Room FindRoomReference(RoomType roomType)
        {

            Room room = null;

            //Check if roomtype already has a dictionary entry
            if (roomDictionary.ContainsKey(roomType))
            {
                room = roomDictionary[roomType];
            }

            //If not attempt to construct a dictionary entry
            else if (RoomConstructorDictionary.ContainsKey(roomType))
            {
                room = RoomConstructorDictionary[roomType]();
                roomDictionary.Add(roomType, room);
            }
            //If roomType is not in either dictionary then assume it is not implemented

            return room;
        }
        #endregion
    }
}
