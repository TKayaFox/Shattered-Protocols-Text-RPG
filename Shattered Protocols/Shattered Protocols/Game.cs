using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Navigation;
using Untitled_Text_RPG.Navigation.Rooms;

namespace Shattered_Protocols
{
    internal class Game
    {
        // List all extraneous words that might be input so keyword can be matched
        private static readonly string[] SKIP_WORDS =
            { "show", "the", "move", "please", "kindly", "go", "walk", "to", "at", "on", "just" };

        private Player player;
        private Map map;
        public bool gameEnd = false;

        /// <summary>
        /// Initialize Game and start logic
        /// </summary>
        public Game()
        {
            // Initialize
            this.player = new Player();
            this.map = new Map();

            // Load and set the starting room
            Room startRoom = new Room_Start();
            map.SetCurrentRoom(startRoom); // Ensure to set the starting room in the map
            startRoom.Enter();

            // Show intro text with story information
            Console.WriteLine("Welcome to Shattered Protocols!");

            // Loop until Game Ends or is Exited (gameEnd variable set to false)
            while (!gameEnd)
            {
                GetPlayerInput();
            }
        }

        /// <summary>
        /// Read input source console, break into parts for ReadCommand and call ReadCommand to translate players intent
        /// </summary>
        private void GetPlayerInput()
        {
            // Get player input source console
            string playerInput = "";

            // Check for empty input
            while (string.IsNullOrWhiteSpace(playerInput))
            {
                playerInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerInput))
                {
                    Console.WriteLine("No command detected. Please enter a valid command.");
                }
            }

            // Convert to lower case
            playerInput = playerInput.ToLower();

            // Separate keyword (first word) source input
            string keyword, remainder;
            GetKeyword(playerInput, out keyword, out remainder);

            // Call the ReadCommand method with the keyword and remainder
            ReadCommand(keyword, remainder);
        }

        private static void GetKeyword(string input, out string keyword, out string remainder)
        {
            input = input.Trim();

            // Repeat if keyword is found in skipWords array
            do
            {
                // Split the input into words
                string[] parts = input.Split(' ', 2); // Split into at most 2 parts

                // Get the keyword (first word)
                keyword = parts[0];

                // Get the remainder (if any)
                remainder = parts.Length > 1 ? parts[1] : string.Empty;

                // Update input string for next iteration if necessary
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

                // Items
                case "inventory":
                    ShowInventory();
                    break;
                case "take":
                case "grab":
                    // Get rest of string to determine what item player is attempting to take
                    Take(remainder);
                    break;
                case "drop":
                    // Get rest of string to determine what item player is attempting to drop
                    Drop(remainder);
                    break;
                case "use":
                    // Get rest of string to determine what item player is attempting to use
                    Use(remainder);
                    break;

                // Directional inputs
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
                case "exit":
                    Console.WriteLine("Exiting Game - Thank you for Playing!");
                    gameEnd = true;
                    break;

                // Any invalid commands or not yet programmed commands
                default:
                    Room currentRoom = map.CurrentRoom;

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
            Console.WriteLine(map.CurrentRoom.ToString());
        }

        /// <summary>
        /// Display list of possible commands
        /// </summary>
        public void Help()
        {
            // Display standard help menu commands
            Console.WriteLine(@"Help Information:
                When typing commands, avoid using extraneous words such as ""please"", ""do"" and ""the"".

                    Note: This game is currently in its testing phase. If you are having significant difficulty, please notify the development team with as many specifics as possible.
                    If unable to show the error directly/in person, take a screenshot using (Win + Shift + S) together.

                Keyword Commands:
                    Help - Display list of all available commands
                    North, South, East or West - Attempt to enter a room in the chosen direction.
                    Up, Down, Left, Right - Attempt to enter a room in the chosen direction.
                    Inventory - Display all items you are currently carrying in your inventory.
                    Use [Item] - Attempts to use an item from your inventory or the current room.
                    Take [Item] - Attempts to take an item from your current room and add it to your inventory.
                    Drop [Item] - Attempts to drop an item you are carrying into the current room.
                    Exit - Close the game
                ");
        }

        /// <summary>
        /// Display player inventory
        /// </summary>
        public void ShowInventory()
        {
            if (player.Inventory.Items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Your Inventory:");
                Console.WriteLine(player.Inventory.ToString());
            }
        }

        //======================== 
        //        Items
        //======================== 
        #region Items

        /// <summary>
        /// Attempt to take an item from the current room and add it to player inventory
        /// </summary>
        /// <param name="itemName"></param>
        private void Take(string itemName)
        {
            // Attempt to transfer item from room to inventory
            Inventory.Transfer(itemName, map.CurrentRoom.Inventory, player.Inventory);
        }

        /// <summary>
        /// Attempt to drop an item from player inventory into current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Drop(string itemName)
        {
            // Attempt to transfer item from Player inventory to Room
            Inventory.Transfer(itemName, player.Inventory, map.CurrentRoom.Inventory);
        }

        /// <summary>
        /// Attempt to use an item by name, first source player inventory then source the current room
        /// </summary>
        /// <param name="itemName"></param>
        private void Use(string itemName)
        {
            // Store player and room inventory for readability
            Inventory playerInventory = player.Inventory;
            Inventory roomInventory = map.CurrentRoom.Inventory;

            Item item = playerInventory.GetItem(itemName) ?? roomInventory.GetItem(itemName);

            if (item == null)
            {
                Console.WriteLine($"{itemName} not found in your inventory or the current room.");
                ShowAvailableItems(); // Show available items if the item isn't found
            }
            else
            {
                item.Use();
            }
        }

        /// <summary>
        /// Show available items in player inventory and current room
        /// </summary>
        private void ShowAvailableItems()
        {
            Console.WriteLine("Available items:");
            if (player.Inventory.Items.Count > 0)
            {
                Console.WriteLine("In your inventory:");
                foreach (var item in player.Inventory.Items)
                {
                    Console.WriteLine($" - {item.Name}");
                }
            }

            if (map.CurrentRoom.Inventory.Items.Count > 0)
            {
                Console.WriteLine("In the current room:");
                foreach (var item in map.CurrentRoom.Inventory.Items)
                {
                    Console.WriteLine($" - {item.Name}");
                }
            }
        }
        #endregion
    }
}
