using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management.Args
{
    public class InventoryArgs : EventArgs
    {
        public Inventory Inventory { get; set; } = new Inventory();
    }
}
