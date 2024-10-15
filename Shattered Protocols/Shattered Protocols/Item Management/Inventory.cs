using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    public class Inventory
    {
        List<Item> inventory = new List<Item>();

        /// <summary>
        /// returns true if inventory list is empty
        /// </summary>
        /// <returns></returns>
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
                    result += $"- {item.Name}\n";
                }
            }
            else
            {
                result = "No Items";
            }
            return result;
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Item item)
        {
            if (item != null)
            {
                inventory.Add(item);
            }
        }

        /// <summary>
        /// Drops an item from the inventory.
        /// </summary>
        /// <param name="itemName">The name of the item to drop.</param>
        /// <returns></returns>
        public Item RemoveItem(String itemName)
        {
            Item item = GetItem(itemName);
            if (item != null)
            {
                inventory.Remove(item);
            }
            return item;
        }

        /// <summary>
        /// Check if a user has an item
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns>returns the matching item</returns>
        public bool HasItem(string itemName)
        {
            return GetItem(itemName) != null;
        }

        public Item GetItem(string itemName)
        {
            return inventory.Find(item => item.Name.Equals(itemName));
        }

        /// <summary>
        /// Static method that will transfer an item from one Inventory object to another
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static bool Transfer(string itemName, Inventory source, Inventory destination)
        {
            bool success = false;
            if (source != null && destination != null)
            {

                //Attempt to get item source room (Will return null and display a message if unable)
                Item item = source.RemoveItem(itemName);

                //If item is null, then state that item was not found
                if (item == null)
                {
                    Console.WriteLine($"{itemName} Not found!");
                }
                //Move Item source Room to Player Inventory
                else
                {
                    destination.Add(item);
                    success = true;
                }
            }
            return success;
        }
    }
}
