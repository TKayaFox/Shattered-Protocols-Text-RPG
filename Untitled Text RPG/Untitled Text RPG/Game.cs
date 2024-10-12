using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_Text_RPG
{
    internal class Game
    {
        //List all extraneous words that might be input so keyword can be matched
        private static readonly string[] SKIP_WORDS = 
            { "the", "move", "please", "kindly", "go", "walk", "to", "at", "on", "just" };

        private const string startRoomFile = "Room 1";

        private Player player;
        private Room currentRoom;

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
        /// Read input from console, break into parts for ReadCommand and call ReadCommand to translate players intent
        /// </summary>
        private void GetPlayerInput()
        {
            //Get player input from console
            string playerInput = Console.ReadLine();

            //Edit: Convert to lower case
            playerInput = playerInput.ToLower();

            //Seperate keyword (first word) from input
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
                    Console.WriteLine("Command not recognized. Type Help for a list of commands!");
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

        }

        /// <summary>
        /// Search command redisplays room description, any items in the room, and the possible directions
        /// </summary>
        public void Search()
        {
        }

        /// <summary>
        /// Display list of possible commands
        /// </summary>
        public void Help()
        {
        }

        /// <summary>
        /// Display player inventory
        /// </summary>
        public void Inventory()
        {
            player.ShowInventory();
        }

        //======================== 
        //        Items
        //======================== 

        /// <summary>
        /// Attempt to take an item from the current room and add to player inventory
        /// </summary>
        /// <param name="itemName"></param>
        private void Take(string itemName)
        {
            //Attempt to get item from room (Will return null and display a message if unable)
            Item item = currentRoom.Take(itemName);

            //Attempt to add item to inventory
            if (item != null)
            {
                player.Add(item);
            }
        }

        /// <summary>
        /// Attempt to drop an item from player inventory into current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Drop(string itemName)
        {
            //Attempt to get item from room (Will return null and display a message if unable)
            Item item = player.Drop(itemName);

            //Attempt to add item to inventory
            if (item != null)
            {
                currentRoom.Add(item);
            }
        }

        /// <summary>
        /// Attempt to use an item by name, first from player inventory then from the current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Use(string itemName)
        {
            bool used = false;

            // Attempt to use the item from the player's inventory.
            if (player.Use(itemName))
            {
                used = true;
            }
            // If not successful, try to use the item from the current room.
            else if (currentRoom.Use(itemName))
            {
                used = true;
            }

            // Inform the user if the item was not found.
            if (!used)
            {
                Console.WriteLine($"{itemName} Not Found");
            }
        }
    }
}
