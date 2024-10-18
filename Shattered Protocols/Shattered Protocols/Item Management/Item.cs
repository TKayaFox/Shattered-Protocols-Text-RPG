using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    public class Item
    {
        // Properties for the name and description of the item
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor to initialize an item with a name and description
        public Item()
        {
            Name = "Generic Item";
            Description = "this is a placeholder";
        }

        public bool Use()
        {
            Console.WriteLine($"Using {Name}");
            bool useSuccess = false;

            //Item based logic for what using does, and whether it can be used here

            return useSuccess;
        }
    }
}
