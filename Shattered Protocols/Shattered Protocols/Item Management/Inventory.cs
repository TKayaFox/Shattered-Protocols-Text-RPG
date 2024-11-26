using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    /// <summary>
    /// Inventory handles Item object management, allowing easy adding/removing/checking of Items in a list, as well as transferring between Inventories.
    /// </summary>
    public class Inventory
    {
        string name;
        List<Item> inventory = new List<Item>();

        #region Getters and Setters
        public string Name
        {
            get => name;
            set => name = value;
        }
        #endregion

        /// <summary>
        /// Constructor overloaded to allow custom inventory naming
        /// By default the Inventory is named "RoomUnlock" but this can be overwritten with a string input
        /// </summary>
        /// <param name="name">String storing the name of the inventory, RoomUnlock by default</param>
        public Inventory(string name = "Room")
        {
            this.name = name;
        }


        /// <summary>
        /// Checks if the inventory is empty of items
        /// </summary>
        /// <returns>returns true if inventory list is empty</returns>
        public bool IsEmpty()
        {
            return inventory.Count == 0;
        }

        /// <summary>
        /// Override ToString to display all items in inventory
        /// </summary>
        /// <returns>String of all items in inventory</returns>
        public override string ToString()
        {
            string result = "";

            if (inventory.Count != 0)
            {
                foreach (var item in inventory)
                {
                    result += $"    {item.Name}\n";
                }
            }
            else
            {
                result = "  No Items";
            }
            return result;
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public bool Add(Item item)
        {
            bool success = false;   
            if (item != null)
            {
                inventory.Add(item);
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Removes (and returns) an item from the inventory (identified by input string) and returns it
        /// </summary>
        /// <param name="itemName">The name of the item to drop.</param>
        /// <returns>named item</returns>
        public Item TakeItem(String itemName)
        {
            Item item = GetItem(itemName);
            if (item != null)
            {
                inventory.Remove(item);
            }
            return item;
        }


        /// <summary>
        /// Removes all items from inventories and returns as an array
        /// </summary>
        /// <returns>Array of Items</returns>
        public Item[] TakeAll()
        {
            // Convert inventory to an array
            Item[] items = inventory.ToArray();

            // Clear the inventory
            inventory.Clear();

            // Return the items
            return items;
        }

        /// <summary>
        /// Check if a user has an item
        /// </summary>
        /// <param name="itemName">The name of the item to look for</param>
        /// <returns>Boolean designating whether the item is in inventory</returns>
        public bool HasItem(string itemName)
        {
            return GetItem(itemName) != null;
        }

        /// <summary>
        /// Get an item reference from inventory (if stored) and return it, without deleting from inventory
        /// </summary>
        /// <param name="itemName">The name of the item to get</param>
        /// <returns>The Item or a Null reference</returns>
        public Item GetItem(string itemName)
        {
            foreach (var item in inventory)
            {
                if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Static method that will transfer a named item from one Inventory object to another
        ///     If itemName is not provided transfers all items from source to destiantion inventory
        /// </summary>
        /// <param name="itemName">name of item to be transferred, defaults to "all"</param>
        /// <param name="source">Source Inventory to transfer from</param>
        /// <param name="destination">Inventory to transfer to</param>
        public static bool Transfer(string itemName, Inventory source, Inventory destination)
        {
            bool success = false;

            //Make sure source and destination are valid
            if (source != null && destination != null)
            {
                //If user did not specify a specific item, or specified ALL then transfer all items
                if (itemName == "" || itemName == "all" || itemName == "everything")
                {
                    success = Transfer(source, destination);
                }
                else //Add singular item
                {
                    //Attempt to get item source room (Will return null and display a message if unable)
                    Item item = source.TakeItem(itemName);

                    //notify user of result
                    if (item != null)
                    {
                        success = destination.Add(item);
                        GameController.Output($"    {item.Name} added to {destination.Name}");
                    }
                    else
                    {
                        GameController.Output("    I dont see that!");
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Overloaded Static method that will transfer all items from one Inventory object to another
        /// </summary>
        /// <param name="destination">destination inventory</param>
        /// <param name="items">an array of items to be transferred</param>
        /// <returns></returns>
        public static bool Transfer(Inventory source, Inventory destination)
        {
            //get all items from target inventory
            Item[] items = source.TakeAll();

            bool success = false;
            if (destination != null)
            {
                //If items array is empty, then state that item was not found
                if (items.Length > 0)
                {
                    // Transfer each item to the destination
                    foreach (Item item in items)
                    {
                        success = destination.Add(item);
                        GameController.Output($"    {item.Name} added to {destination.Name}");
                    }
                }
                else
                {
                    GameController.Output($"    There is nothing here!");
                }
            }
            return success;
        }
    }
}
