using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Navigation;

namespace Shattered_Protocols
{
    internal class Game
    {
        //List all extraneous words that might be input so keyword can be matched
        private static readonly string[] SKIP_WORDS = 
            { "show","the", "move", "please", "kindly", "go", "walk", "to", "at", "on", "just" };

        private const string startRoomFile = "Room 1";

        private Player player;
        private Room currentRoom;
        private Map map = new Map();

        /// <summary>
        /// Initialize Game and start logic
        /// </summary>
        public Game()
        {
            //Initialize
            this.player = new Player();
            Room startRoom = new Room();


            //Show intro text with story information
            currentRoom = startRoom;

            //Enter the Starting Room
            currentRoom.Enter();

            //EDIT: setup the input loop and end state

            //Get player input and translate into commands
            GetPlayerInput();
        }

        /// <summary>
        /// Read input source console, break into parts for ReadCommand and call ReadCommand to translate players intent
        /// </summary>
        private void GetPlayerInput()
        {
            //Get player input source console
            string playerInput = Console.ReadLine();

            //Edit: Convert to lower case
            playerInput = playerInput.ToLower();

            //Seperate keyword (first word) source input
            string keyword, remainder;
            GetKeyword(playerInput, out keyword, out remainder);

            // Call the ReadCommand method with the keyword and remainder
            ReadCommand(keyword, remainder);
        }

        private static void GetKeyword(string input, out string keyword, out string remainder)
        {
            //repeat if keyword is found in skipWords array
            do
            {
                // Split the input into words
                string[] parts = input.Split(' ', 2); // Split into at most 2 parts

                // get the keyword (first word)
                keyword = parts[0];

                // get the remainder (if any)
                remainder = parts.Length > 1 ? parts[1] : string.Empty;

                //update input string for next iteration if necessary
                input = remainder;
            } while (SKIP_WORDS.Contains(keyword));
        }

        /// <summary>
        /// Reads player input and determines how best to handle it
        /// </summary>
        /// <param name="command"></param>
        private void ReadCommand(string command, string remainder)
        {
            switch (command)
            {
                case "search":
                case "look":
                    Search();
                    break;
                case "help":
                    Help();
                    break;

                //Items
                case "inventory":
                    ShowInventory();
                    break;
                case "take":
                case "grab":
                    //Get rest of string to determine what item player is attempting to take
                    Take(remainder);
                    break;
                case "drop":
                    //Get rest of string to determine what item player is attempting to drop
                    Drop(remainder);
                    break;
                case "use":
                    //Get rest of string to determine what item player is attempting to use
                    Use(remainder);
                    break;

                //Directional inputs
                case "north":
                case "up":
                    ChangeRoom(Direction.North);
                    break;
                case "south":
                case "down":
                    ChangeRoom(Direction.South);
                    break;
                case "west":
                case "left":
                    ChangeRoom(Direction.West);
                    break;
                case "east":
                case "right":
                    ChangeRoom(Direction.East);
                    break;

                // Any invalid commands or not yet programmed commands
                default:
                    if (currentRoom.RoomPuzzle != null)
                    {
                        currentRoom.RoomPuzzle.ReadCommand(command, remainder);
                    }
                    else
                    {
                        Console.WriteLine("Command not recognized. Type Help for a list of commands!");
                    }
                    break;
            }
        }

        //====================================================================
        //                     Player Command Methods
        //====================================================================
        
        /// <summary>
        /// Handles Movement between rooms
        /// </summary>
        /// <param name="direction"></param>
        public void ChangeRoom(Direction direction)
        {
            map.Move(direction);
        }

        /// <summary>
        /// Search command redisplays room description, any items in the room, and the possible directions
        /// </summary>
        public void Search()
        {
            Console.WriteLine(currentRoom.ToString);
        }

        /// <summary>
        /// Display list of possible commands
        /// </summary>
        public void Help()
        {
            //EDIT: Convert Keyword list to be a dictionary with a word and description of the command
        }

        /// <summary>
        /// Display player inventory
        /// </summary>
        public void ShowInventory()
        {
            Console.WriteLine(player.Inventory.ToString());
        }

        //======================== 
        //        Items
        //======================== 
        #region Items
        /// <summary>
        /// Attempt to take an item source the current room and add to player inventory
        /// </summary>
        /// <param name="itemName"></param>
        private void Take(string itemName)
        {
            //Attempt to transfer item from room to inventory
            Inventory.Transfer(itemName, currentRoom.Inventory, player.Inventory);
        }

        /// <summary>
        /// Attempt to drop an item source player inventory into current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Drop(string itemName)
        {
            //Attempt to transfer item from Player inventory to Room
            Inventory.Transfer(itemName, player.Inventory, currentRoom.Inventory);
        }


        /// <summary>
        /// Attempt to use an item by name, first source player inventory then source the current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Use(string itemName)
        {
            //store player and room inventory for readability
            Inventory playerInventory = player.Inventory;
            Inventory roomInventory = currentRoom.Inventory;

            bool used = false;
            Item item = playerInventory.GetItem(itemName);

            //if item is null then check the room player is in
            if (item == null)
            {
                item = roomInventory.GetItem(itemName);
            }

            //if still null, then item not found. otherwise attempt to use it.
            if (item == null)
            {
                Console.WriteLine($"{itemName} Not Found");
            }
            else
            {
                item.Use();
            }
        }
        #endregion
    }
}
