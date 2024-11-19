using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Enumerations
{
    /// <summary>
    /// Used to easily reference rooms by type
    /// </summary>
    public enum RoomType
    {
        Null,
        Room_Start,
        Room_Meeting,
        Room_Development,
        Room_Operations,
        Room_Testing,
        Room_Break,
        Room_Server,
        Locked
    }
}
