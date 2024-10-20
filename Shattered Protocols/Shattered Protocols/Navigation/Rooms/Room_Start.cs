using Shattered_Protocols;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Untitled_Text_RPG.Navigation.Rooms
{
    internal class Room_Start : Room
    {
        public Room_Start()
        {
            this.Name = "Front Lobby";
            this.Description = "Welcome to the front Lobby. There are empty desks where receptionists would welcome guests. Each desk holds a computer and accompanying phone. Calling for help would be pointless here, you are on your own... Also the phones probably dont event work anymore.";
            this.RoomPuzzle = null;

            //Room Items
            this.Inventory = new Inventory();

            Item testItem = new Item("Pants", "A pair of pants");
            Item computer = new Item("Computer", "A receptionist's computer. It's too heavily corrupted to use.");
            Item phone = new Item("Phone", "A receptionists phone. It's attached to the desk.");

            this.Inventory.Add(testItem);
            this.Inventory.Add(computer);
            this.Inventory.Add(phone);
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
