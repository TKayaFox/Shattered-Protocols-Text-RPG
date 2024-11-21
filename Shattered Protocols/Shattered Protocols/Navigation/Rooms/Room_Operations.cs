using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management.Args;
using Shattered_Protocols.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols.Navigation.Rooms
{
    internal class Room_Operations : Room
    {
        public Room_Operations() : base(RoomType.Room_Operations)
        {
            Name = "Heart of Operations";
            Description = @"
    Welcome to the Heart of Operations. There is a big supercomputer that takes up most 
    of this room. The computer is protected by an anti-blast, anti-EMP casing. Even if 
    you nuked this building, UtopiaNet would still survive. That's why you are here. 
    There seems to be a USB port in the casing. Huh... there is also a suspicious looking 
    avian symbol above the port...
             ";
            RoomPuzzle = new PuzzleSQLInjection();

            //Room Items
            Inventory = new Inventory();


            //Define Neighboring Rooms
            //  DirectionEnum relative to current roon, type of room
            NewDoor(DirectionEnum.North, RoomType.Room_Server);
        }

        /// <summary>
        /// Override Enter so that Puzzle no longer displays automatically
        /// </summary>
        /// <param name="originDirection"></param>
        public override void Enter(DirectionEnum originDirection)
        {
            RoomPuzzle.Room = RoomType.Room_Operations;

            //Display room name and description using ToString
            GameController.Output(ToString());
        }


        //Override OnUseItem to allow FlashDrive usage
        internal override void OnUseItem(EventArgs args)
        {
            // Check if args is of type ItemArgs (if so store as itemArgs)
            if (args is ItemArgs itemArgs)
            {
                // Unpack Args as ItemArgs
                string name = itemArgs.Name;

                // Check if name is "flashDrive"
                if (name.Equals("ducky_flash_drive", StringComparison.OrdinalIgnoreCase))
                {
                    //Run ShowPuzzle Logic if applicable
                    ShowPuzzle();
                }
            }
            else
            {
                GameController.Output("    This Item cannot be used here!");
            }
        }
    }
}
