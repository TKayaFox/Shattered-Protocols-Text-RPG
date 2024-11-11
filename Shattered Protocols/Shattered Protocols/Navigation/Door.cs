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
        Direction direction;
        RoomType roomType;
        bool puzzleLocked;

        public Door(RoomType roomtype, Direction direction)
        {
            RoomType = roomtype;
            Direction = direction;
        }

        public Direction Direction { get => direction; set => direction = value; }
        public RoomType RoomType { get => roomType; set => roomType = value; }
        public bool PuzzleLocked { get => puzzleLocked; set => puzzleLocked = value; }
    }
}
