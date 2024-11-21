using Shattered_Protocols.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management.Args
{
    /// <summary>
    /// Stores information needed for room handling events
    /// </summary>
    internal class RoomArgs : EventArgs
    {
        private RoomType roomType;

        public RoomType RoomType { get => roomType; set => roomType = value; }
    }
}
