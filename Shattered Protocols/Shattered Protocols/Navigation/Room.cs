using Shattered_Protocols;
public abstract class Room
{
    private string name;
    private Inventory inventory;
    private string description;
    private Puzzle roomPuzzle;

    //Neighboring Rooms
    private Room northRoom = null;
    private Room southRoom = null;
    private Room westRoom = null;
    private Room eastRoom = null;

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
    public Room North
    {
        get => northRoom;
        set => northRoom = value;
    }
    public Room South
    {
        get => southRoom;
        set => southRoom = value;
    }
    public Room East
    {
        get => eastRoom;
        set => eastRoom = value;
    }
    public Room West
    {
        get => westRoom;
        set => westRoom = value;
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
        //Load all room neighbors
        LoadNeighboringRooms();

        //Display room name and description using ToString
        Console.WriteLine(ToString());

        //Run ShowPuzzle Logic if applicable
        ShowPuzzle();
    }

    /// <summary>
    /// Attempt to load all neighboring rooms (if not already loaded)
    ///     Make sure to check first that Room is not already loaded!
    /// </summary>
    public abstract void LoadNeighboringRooms();

    ///ShowPuzzle Logic if Applicable
    public void ShowPuzzle()
    {
        //puzzle logic
        if (roomPuzzle != null)
        {
            //Start ShowPuzzle
            roomPuzzle.Start();
        }
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
        if (northRoom != null)
        {
            exits.Add("north");
        }
        if (southRoom != null)
        {
            exits.Add("south");
        }
        if (eastRoom != null)
        {
            exits.Add("east");
        }
        if (westRoom != null)
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
