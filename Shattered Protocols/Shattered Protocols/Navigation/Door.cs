using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management.Args;

namespace Shattered_Protocols.Navigation
{
    /// <summary>
    /// Door holds information necessary for movement between Room objects and is required for the Map object.
    /// When locked the door cannot be used.
    /// </summary>
    internal class Door
    {
        public RoomType CurrentRm { get; set; }
        public RoomType DestinationRm { get; set; }
        public bool Locked { get; set; }

        /// <summary>
        /// Constructor, sets up what the door actually connects to and from
        /// </summary>
        /// <param name="currentRm">room type that the door is in</param>
        /// <param name="destination">room type that the door connects to</param>
        /// <param name="locked">Whether the door is initially locked</param>
        public Door(RoomType currentRm, RoomType destination, bool locked = false)
        {
            //stoer variables
            CurrentRm = currentRm;
            DestinationRm = destination;
            Locked = locked;

            //Subscribe to events
            ManageMe();
        }


        //======================== 
        //        Events
        //======================== 

        #region Event Manager
        /// <summary>
        /// ManageMe and UnManageMe are required for IEventManagable, and handle subscription or unsubscription from events
        /// </summary>
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

        /// <summary>
        /// when unlock room event is raised, check door connects (on either side) to the roomtype stored in args. if so unlock the door
        ///     Does nothing if RoomType is not found or doesnt match
        /// </summary>
        /// <param name="args">Should be a RoomArgs object storing the RoomType to be unlocked</param>
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
