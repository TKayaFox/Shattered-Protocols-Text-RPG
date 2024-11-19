using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management.Args
{
    /// <summary>
    /// Stores information needed for Item events
    /// </summary>
    public class ItemArgs : EventArgs
    {
        String name;
        String description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
    }
}
