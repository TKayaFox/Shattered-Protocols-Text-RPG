using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_Text_RPG
{
    internal class Player
    {
        #region Data Fields and Variables
        List<Item> inventory = new List<Item>();

        public string name
        {
            get => default;
            set
            {
            }
        }
        public int description
        {
            get => default;
            set
            {
            }
        }
        #endregion

        public Player() 
        { 
        }

        #region Inventory Management
        /// <summary>
        /// Displays the items in the inventory.
        /// </summary>
        public void ShowInventory()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("You have no items");
                return;
            }

            Console.WriteLine("You are holding the following items:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Item item)
        {
            inventory.Add(item);
        }

        /// <summary>
        /// Drops an item from the inventory.
        /// </summary>
        /// <param name="itemName">The name of the item to drop.</param>
        /// <returns>The dropped item or null if not found.</returns>
        public Item Drop(string itemName)
        {
            //check inventory for matches
            Item itemToDrop = inventory.Find(item => item.Name.Equals(itemName);

            if (itemToDrop != null)
            {
                inventory.Remove(itemToDrop);
                Console.WriteLine($"{itemToDrop.Name} has been dropped from your inventory.");
                return itemToDrop;
            }
            else
            {
                Console.WriteLine($"You don't have {itemName} in your inventory.");
                return null;
            }
        }


        #endregion
    }
}
