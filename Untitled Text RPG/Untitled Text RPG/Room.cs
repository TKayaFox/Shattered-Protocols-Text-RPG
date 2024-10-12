using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untitled_Text_RPG;
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

        //Run Puzzle Logic if applicable
        Puzzle();
    }

    ///Puzzle Logic if Applicable
    public void Puzzle()
    {
        //puzzle logic
        if (roomPuzzle != null)
        {
            //Start Puzzle
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
            Console.WriteLine (inventory.ToString);
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
