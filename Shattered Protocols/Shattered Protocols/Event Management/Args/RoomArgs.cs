using Shattered_Protocols.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management.Args
{
    internal class RoomArgs : EventArgs
    {
        public RoomType roomType;
        public Room room;
    }
}
