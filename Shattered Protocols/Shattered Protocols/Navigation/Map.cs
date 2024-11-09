using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Event_Management;
using Shattered_Protocols.Event_Management.Args;
using Shattered_Protocols.Navigation;
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
        private Dictionary<RoomEnum, Room>
            roomDictionary = new Dictionary<RoomEnum, Room>();
        private Dictionary<RoomEnum, Func<Room>>
            RoomConstructorDictionary = new Dictionary<RoomEnum, Func<Room>>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Map()
        {
            //Get all needed room constructors into dictionary
            InitializeConstructorLibrary();

            //Set initial room as the current room
            currentRoom = GetRoom(RoomEnum.Room_Start);

            //Enter the Starting Room
            currentRoom.Enter();
        }

        /// <summary>
        /// Adds Room Constructor Methods to dictionary
        /// </summary>
        private void InitializeConstructorLibrary()
        {
            //Build Room constructor dictionary
            RoomConstructorDictionary.Add(RoomEnum.Room_Break, () => new Room_Break());
            RoomConstructorDictionary.Add(RoomEnum.Room_Development, () => new Room_Development());
            RoomConstructorDictionary.Add(RoomEnum.Room_Meeting, () => new Room_Meeting());
            RoomConstructorDictionary.Add(RoomEnum.Room_Operations, () => new Room_Operations());
            RoomConstructorDictionary.Add(RoomEnum.Room_Server, () => new Room_Server());
            RoomConstructorDictionary.Add(RoomEnum.Room_Start, () => new Room_Start());
            RoomConstructorDictionary.Add(RoomEnum.Room_Testing, () => new Room_Testing());
        }

        #region Map Navigation
        //Moves the current Room to another direction if able
        public void Move(Direction direction)
        {
            //Determine what roomtype to load
            RoomEnum roomType = GetRoomType(direction);

            //Get the reference to the correct room
            Room room = GetRoom(roomType);

            //Make sure room is valid
            if (room != null)
            {
                Console.WriteLine($"Entering new Room: {room.Name}");
                currentRoom = room;
                room.Enter();

                //preload adjacent rooms
                LoadNeighbors();
            }
            else
            {
                Console.WriteLine($"You cannot go this way");
            }
        }

        private void LoadNeighbors()
        {
            GetRoom(currentRoom.North);
            GetRoom(currentRoom.South);
            GetRoom(currentRoom.East);
            GetRoom(currentRoom.West);
        }

        private RoomEnum GetRoomType(Direction direction)
        {
            RoomEnum roomType = 0;

            //Get the next room using directional references in current room
            switch (direction)
            {
                case Direction.North:
                    roomType = currentRoom.North;
                    break;
                case Direction.South:
                    roomType = currentRoom.South;
                    break;
                case Direction.West:
                    roomType = currentRoom.West;
                    break;
                case Direction.East:
                    roomType = currentRoom.East;
                    break;
            }

            return roomType;
        }

        private Room GetRoom(RoomEnum roomType)
        {
            Room room;

            //Check if room is Null, simply return a Null reference
            if (roomType == RoomEnum.Null)
            {
                room = null;
            }
            //Get existing room from dictionary if possible
            else if (roomDictionary.ContainsKey(roomType))
            {
                room = roomDictionary[roomType];
            }
            //If not null, and doesnt exist yet, then construct room and return it
            else
            {
                room = RoomConstructorDictionary[roomType]();
                roomDictionary.Add(roomType, room);
            }

            return room;
        }

        #endregion
    }
}
