using Shattered_Protocols;
using Shattered_Protocols.Puzzles;
using Shattered_Protocols.Navigation;
public abstract class Room
{
    private string name;
    private Inventory inventory;
    private string description;
    private Puzzle roomPuzzle;

    //Neighboring Rooms
    private Dictionary<Direction, RoomType> roomDictionary = new Dictionary<Direction, RoomType>();
    private Dictionary<Direction, RoomType> lockedRoomDictionary = new Dictionary<Direction, RoomType>();

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
    #region Neighbor Rooms
    public RoomType North
    {
        get => GetUnlockedRoomType(Direction.North);
        set => roomDictionary[Direction.North] = value;
    }
    public RoomType South
    {
        get => GetUnlockedRoomType(Direction.South);
        set => roomDictionary[Direction.South] = value;
    }
    public RoomType East
    {
        get => GetUnlockedRoomType(Direction.East);
        set => roomDictionary[Direction.East] = value;
    }
    public RoomType West
    {
        get => GetUnlockedRoomType(Direction.West);
        set => roomDictionary[Direction.West] = value;
    }
    #endregion
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    public Room()
    {
        name = "Unfinished Room";
        description = "This room not yet implemented";
        roomPuzzle = null;

        //Room Items
        inventory = new Inventory();
    }


    ///Called when first entering a room
    public void Enter()
    {
        //Display room name and description using ToString
        Console.WriteLine(ToString());

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

    /// <summary>
    /// This method will retorn RoomType Locked if the door is locked, or the correct roomtype if it is not locked
    /// I really could not come up with a better name for this
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    public RoomType GetUnlockedRoomType(Direction direction)
    {
        RoomType roomType = RoomType.Null;

        //Check if door exists
        if (roomDictionary.ContainsKey(direction))
        {
            //Check if door is locked AND puzzle is not solved
            if (lockedRoomDictionary.ContainsKey(direction) && !roomPuzzle.IsSolved)
            {
                roomType = RoomType.Locked;
            }
            else
            {
                roomType = roomDictionary[direction];
            }
        }

        return roomType;
    }

    /// <summary>
    /// Override ToString to display room info
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        //Display room name and description
        String roomData = $"{name}- {description}\n";

        //Show any items in the room
        if (!inventory.IsEmpty())
        {
            roomData += inventory.ToString() + "\n"; // Append to roomData instead of printing
        }
        else
        {
            roomData += "\tThe Room has no items you can interact with\n";
        }

        //Determine all possible Exits
        List<string> exits = new List<string>();
        if (northRoom != RoomType.Null)
        {
            exits.Add("north");
        }
        if (southRoom != RoomType.Null)
        {
            exits.Add("south");
        }
        if (eastRoom != RoomType.Null)
        {
            exits.Add("east");
        }
        if (westRoom != RoomType.Null)
        {
            exits.Add("west");
        }

        //Add to strring all possible exits
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
}
