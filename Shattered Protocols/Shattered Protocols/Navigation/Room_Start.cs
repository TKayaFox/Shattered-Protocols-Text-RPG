using Shattered_Protocols;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Puzzles;
using static System.Net.Mime.MediaTypeNames;

namespace Shattered_Protocols.Navigation
{
    internal class Room_Start : Room
    {
        public Room_Start()
        {
            this.Name = "Front Desks";
            this.Description = @"
             Welcome to the Front Desks. There are empty chairs where receptionists would welcome guests.
             There are computers with an accompanying phone on each one. Calling for help would be pointless
             here you are on your own… also, the phones probably don't work anymore.
             ";
            this.RoomPuzzle = null;

            //Room Items
            Inventory = new Inventory();
        }


        /// <summary>
        /// Attempt to load all neighboring rooms (if not already loaded)
        /// </summary>
        public override void LoadNeighboringRooms()
        {
            //Load rooms if not yet loaded
            if (North == null)
            {
                North = new Room_Meeting();
                North.South = this;
            }
        }
    }
}
