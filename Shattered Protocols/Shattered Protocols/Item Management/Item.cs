using Shattered_Protocols.Event_Management.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;

namespace Shattered_Protocols
{
    /// <summary>
    /// Stores information as needed for an Item object.
    ///     For most Items this will just be a name and description with item usage logic being handled elsewhere
    /// </summary>
    public class Item
    {
        // Properties for the name and description of the item
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Constructor to initialize an item with a name and description
        /// </summary>
        /// <param name="name">name of the item</param>
        /// <param name="description">description for the item</param>
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Handles logic for using an item. In most case, this simply packages item information and raises an event to signal what item was used.
        /// What this actually accomplishes will be handled by listeners when applicable
        /// </summary>
        /// <returns></returns>
        public void Use()
        {
            //Make event args
            ItemArgs args = new ItemArgs();
            args.Name = Name;
            args.Description = Description;

            //Raise an event that the item was used
            GameController.Publish(EventType.UseItem, args);
        }
    }
}
