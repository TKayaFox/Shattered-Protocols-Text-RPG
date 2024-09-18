using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Untitled_Text_RPG;
public class Room
{
    private string name = "Room";
    private string description = "This room not yet implemented";
    private Room northRoom = null;
    private Room southRoom = null;
    private Room westRoom = null;
    private Room eastRoom = null;
    string northRoomFileName = "";
    string southRoomFileName = "";
    string westRoomFileName = "";
    string eastRoomFileName = "";
    private ArrayList items = new ArrayList();

    /// <summary>
    /// Constructor does nothing.
    ///     See Load static method instead for loading room data
    ///     See Enter method for when player first enters the room
    /// </summary>
    public Room() { }

    /// <summary>
    /// Load attempts to load file data for a room.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="room"></param>
    /// <returns>
    /// A valid Room object if the file path is valid and the room data is successfully loaded.
    /// Returns null if the file path is invalid or if there is an error in loading the room.
    /// </returns>
    public static Room Load(string filePath, Room room)
    {
        room = null;

        //if filepath is not "" then attempt to load it
        if (filePath != "")
        {
            //try to load data from filepath
                //Load data
            //catch any exceptions such as file not found, and display an error for user
                //Display error message
        }
        return room;
    }

    ///Called when first entering a room
    public void Enter()
    {
        //Load nearby rooms
        LoadNeighbors();

        //Display room name and description using ToString
        Console.WriteLine(ToString());

        //Run Puzzle Logic if applicable
        Puzzle();
    }

    ///Find and load the room data for neighboring rooms and pre-load them
    public void LoadNeighbors()
    {
        //for each possible room direction attempt to load the appropriate file
        northRoom = Load(northRoomFileName, northRoom);
        southRoom = Load(northRoomFileName, northRoom);
        westRoom = Load(northRoomFileName, northRoom);
        eastRoom = Load(northRoomFileName, northRoom);
    }

    ///Puzzle Logic if Applicable
    public void Puzzle()
    {
        //puzzle logic
    }

    /// <summary>
    /// OVerride ToString to display room info
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        String roomData = "";
        //Display room name and description

        //Show possible exits (ex. "There are doorways to the north and south")
        return roomData;
    }
}
