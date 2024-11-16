using Shattered_Protocols;
using Shattered_Protocols.Puzzles;
using Shattered_Protocols.Navigation;
using static System.Collections.Specialized.BitVector32;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management.Args;
using System.Net.Sockets;
public abstract class Room
{
    private string name;
    private Inventory inventory;
    private string description;
    private Puzzle roomPuzzle;
    private RoomType roomType;

    //Neighboring Rooms
    private Dictionary<Direction, Door> roomDictionary = new Dictionary<Direction, Door>();

    #region Getters and Setters
    public string Name
    {
        get => name;
        set => name = value;
    }
    public string Description
    {
        get => description;
        set => description = value;
    }
    public Inventory Inventory
    {
        get => inventory;
        set => inventory = value;
    }
    public Puzzle RoomPuzzle
    {
        get => roomPuzzle;
        set => roomPuzzle = value;
    }
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    public Room(RoomType roomType)
    {
        name = "Unfinished Room";
        description = "This room not yet implemented";
        roomPuzzle = null;
        this.roomType = roomType;

        //Room Items
        inventory = new Inventory();

        //Subscribe to eventmanager
        ManageMe();
    }


    ///Called when first entering a room
    public virtual void Enter(Direction originDirection)
    {
        //Unlock the door that was used to enter (If it was used it should be unlocked)
        if (roomDictionary.ContainsKey(originDirection))
        {
            roomDictionary[originDirection].Locked = false;
        }

        //Make sure puzzle knows what room it belongs to
        if (roomPuzzle != null)
            roomPuzzle.Room = roomType;

        //Display room name and description using ToString
        GameController.Output(ToString());

        //Run ShowPuzzle Logic if applicable
        ShowPuzzle();
    }

    ///ShowPuzzle Logic if Applicable
    public void ShowPuzzle()
    {
        //puzzle logic (If there is a puzzle and it is not already solved)
        if (roomPuzzle != null && !roomPuzzle.IsSolved)
        {
            //Start ShowPuzzle
            roomPuzzle.Start();
        }
    }


    public RoomType UseDoor(Direction direction)
    {
        RoomType doorType = RoomType.Null;

        //Make sure direction is in roomDictionary
        if (roomDictionary.ContainsKey(direction))
        {
            //Variables stored for readability
            Door door = roomDictionary[direction];

            //Check if door exists
            if (door != null)
            {
                //Check if Door is locked (And puzzle not solved)
                if (roomPuzzle != null && door.Locked)
                {
                    doorType = RoomType.Locked;
                }
                //else return correct roomType
                else
                {
                    doorType = door.DestinationRm;
                }
            }
        }
        return doorType;
    }

    #region Directional Reference
    //Get stored roomType without checking for locks
    public RoomType GetRoomType(Direction direction)
    {
        RoomType roomType = RoomType.Null;
        if (roomDictionary.ContainsKey(direction))
        {
            roomType = roomDictionary[direction].DestinationRm;
        }
        return roomType;
    }

    public void NewDoor(Direction direction, RoomType destinationRm, bool puzzleLocked = false)
    {

        //remove existing dictionary entry if needed
        if (roomDictionary.ContainsKey(direction))
        {
            roomDictionary.Remove(direction);
        }
        Door door = new Door(roomType, destinationRm, puzzleLocked);
        roomDictionary.Add(direction, door);
    }
    #endregion
    #region Door Handling
    private bool DoorExists(Direction direction)
    {
        return roomDictionary.ContainsKey(direction) && roomDictionary[direction] != null;
    }

    public void SetLock(Direction direction, bool isLocked)
    {
        if (DoorExists(direction))
        {
            // Set the lock state to isLocked
            roomDictionary[direction].Locked = isLocked;
        }
    }
    #endregion


    #region String Processing

    /// <summary>
    /// Override ToString to display room info
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        //Display room name and description
        String roomData = $"{name}- {description}\n";
        roomData = GetRoomItemString(roomData);
        roomData = GetRoomExitString(roomData);

        return roomData;
    }

    private string GetRoomExitString(string roomData)
    {
        //Determine all possible Exits
        List<string> exits = new List<string>();
        if (roomDictionary.ContainsKey(Direction.North))
        {
            exits.Add("north");
        }
        if (roomDictionary.ContainsKey(Direction.South))
        {
            exits.Add("south");
        }
        if (roomDictionary.ContainsKey(Direction.East))
        {
            exits.Add("east");
        }
        if (roomDictionary.ContainsKey(Direction.West))
        {
            exits.Add("west");
        }

        //Add to string all possible exits
        if (exits.Count > 1)
        {
            roomData += $"\tThere are Doorways to the {string.Join(", ", exits)}.";
        }
        else if (exits.Count > 0)
        {
            roomData += $"\tThere is a doorway to the {string.Join(", ", exits)}.";
        }
        else
        {
            roomData += "\tThere are no exits.";
        }

        return roomData;
    }

    private string GetRoomItemString(string roomData)
    {

        //Show any items in the room
        if (!inventory.IsEmpty())
        {
            roomData += inventory.ToString() + "\n"; // Append to roomData instead of printing
        }
        else
        {
            roomData += "\tThe Room has no items you can interact with\n";
        }

        return roomData;
    }
    #endregion



    //======================== 
    //        Events
    //======================== 

    #region Event Manager
    public void ManageMe()
    {
        //Subscribe to events here, make sure to also include unsubscription
        GameController.Subscribe(EventType.UseItem, OnUseItem);
    }
    public void UnManageMe()
    {
        //UnSubscribe to events here
        GameController.Unsubscribe(EventType.UseItem, OnUseItem);
    }
    #endregion

    #region Events
    internal virtual void OnUseItem(EventArgs args)
    {
        //By default Items do nothing, must override in child class.
        //  If Room does use item unpack args to check if correct item is being used.
    }
    #endregion
}
