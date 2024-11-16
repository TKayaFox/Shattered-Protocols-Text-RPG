using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management.Args;

namespace Shattered_Protocols.Navigation
{
    internal class Door
    {
        public RoomType CurrentRm { get; set; }
        public RoomType DestinationRm { get; set; }
        public bool Locked { get; set; }

        public Door(RoomType currentRm, RoomType destination, bool puzzleLocked = false)
        {
            CurrentRm = currentRm;
            DestinationRm = destination;
            Locked = puzzleLocked;

            //Subscribe to events
            ManageMe();
        }


        //======================== 
        //        Events
        //======================== 

        #region Event Manager
        public void ManageMe()
        {
            //Subscribe to events here, make sure to also include unsubscription
            GameController.Subscribe(EventType.UnlockRoom, OnUnlockRoom);
        }
        public void UnManageMe()
        {
            //UnSubscribe to events here
            GameController.Unsubscribe(EventType.UnlockRoom, OnUnlockRoom);
        }
        #endregion

        #region Events

        //if unlock room event is raised, check if it matches doors room. if so unlock
        private void OnUnlockRoom(EventArgs args)
        {
            //Check if event holds RoomArgs.
            if (args is RoomArgs puzzle)
            {
                //If the events RoomType matches the room on either side, then unlock door.
                if (puzzle.RoomType == CurrentRm || puzzle.RoomType == DestinationRm)
                {
                    Locked = false;
                }
            }
        }
        #endregion
    }
}
