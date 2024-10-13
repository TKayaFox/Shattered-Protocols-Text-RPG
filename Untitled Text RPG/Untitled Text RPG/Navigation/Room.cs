using Shattered_Protocols;
public class Room
{
    private string name = "Unfinished Room";
    private string description = "This room not yet implemented";
    private Inventory inventory = new Inventory();
    private Puzzle roomPuzzle;

    //Neighboring Rooms
    private Room northRoom = null;
    private Room southRoom = null;
    private Room westRoom = null;
    private Room eastRoom = null;

    // Events
    public event Action<Room> OnEnter; // Triggered when the room is entered
    public event Action<Item> OnItemAdded; // Triggered when an item is added
    public event Action<Item> OnItemRemoved; // Triggered when an item is removed
    public event Action<Puzzle> OnPuzzleSolved; // Triggered when a puzzle is solved

    #region Getters and Setters
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
            Console.WriteLine (inventory.ToString());
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
        if (exits.Count > 0)
        {
            roomData += $"There are doorways to the {string.Join(", ", exits)}.";
        }
        else
        {
            roomData += "There are no exits.";
        }

        return roomData;
    }
}
