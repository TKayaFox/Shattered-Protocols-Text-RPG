using Shattered_Protocols.Event_Management.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;

namespace Shattered_Protocols
{
    public class Item
    {
        // Properties for the name and description of the item
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor to initialize an item with a name and description
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public bool Use()
        {
            bool useSuccess = false;

            //Make event args
            ItemArgs args = new ItemArgs();
            args.Name = Name;
            args.Description = Description;

            //Raise an event that the item was used
            GameController.Publish(EventType.UseItem, args);

            return useSuccess;

        }
    }
}
