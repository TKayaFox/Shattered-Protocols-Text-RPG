using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;

namespace Shattered_Protocols.Navigation
{
    internal class Door
    {
        RoomType roomType;
        bool puzzleLocked;

        public Door(RoomType roomtype, bool puzzleLocked = false)
        {
            RoomType = roomtype;
            PuzzleLocked = puzzleLocked;
        }

        public RoomType RoomType { get => roomType; set => roomType = value; }
        public bool PuzzleLocked { get => puzzleLocked; set => puzzleLocked = value; }
    }
}
