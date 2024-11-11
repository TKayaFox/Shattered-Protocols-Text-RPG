using Shattered_Protocols;
using Shattered_Protocols.Puzzles;
using Shattered_Protocols.Navigation;
using static System.Collections.Specialized.BitVector32;
using Shattered_Protocols.Enumerations;
public abstract class Room
{
    private string name;
    private Inventory inventory;
    private string description;
    private Puzzle roomPuzzle;

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


    public RoomType UseDoor(Direction direction)
    {
        RoomType roomType = RoomType.Null;

        //Make sure direction is in roomDictionary
        if (roomDictionary.ContainsKey(direction))
        {
            //Variables stored for readability
            Door door = roomDictionary[direction];
            bool doorLocked = roomPuzzle.IsSolved && door.PuzzleLocked;

            //Check if door exists
            if (door != null)
            {
                //Check if Door is locked (And puzzle not solved)
                if (roomPuzzle != null && doorLocked)
                {
                    roomType = RoomType.Locked;
                }
                //else return correct roomType
                else
                {
                    roomType = door.RoomType;
                }
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
}
