using System;
using System.Collections.Generic;
using Shattered_Protocols.Event_Management;
using Shattered_Protocols.Event_Management.Args;
using System.Linq;
using System.Text;
using Shattered_Protocols.Enumerations;

namespace Shattered_Protocols.Puzzles
{
    public abstract class Puzzle
    {
        public string Description { get; set; }
        public bool IsSolved { get; protected set; } = false;
        public int AttemptCount { get; set; } = 0;
        public RoomType RoomUnlock { get; set; } = RoomType.Null;
        public Inventory Rewards { get; set; }

        // Constructor to initialize description and required item
        protected Puzzle(string description)
        {
            Description = description;
            Rewards = new Inventory("Puzzle Rewards");
        }

        /// <summary>
        /// Starts the puzzle logic.
        /// </summary>
        public virtual void Start()
        {
            ResetattemptCount();
        }

        /// <summary>
        /// Reads player input and determines how best to handle it.
        /// </summary>
        public void ReadCommand(string command, string remainder)
        {
            //Unless there is an override for ReadCommand(string,string) then convert to ReadCommand(string) for simplicity
            string stringInput = "";

            if (remainder != null)
            {
                stringInput = $"{command} {remainder}";
            }
            else
            {
                stringInput = command;
            }

            //trim of blankspace after or before string
            stringInput = stringInput.Trim();

            ReadCommand(stringInput);
        }
        public abstract void ReadCommand(string stringInput);


        // set a reset for when the player first encounters the puzzle
        public void ResetattemptCount()
        {
            AttemptCount = 0;
        }

        public virtual void PuzzleSolved(String resolutionMsg = "")
        {
            //mark solved
            IsSolved = true;

            //Display resolution message
            GameController.Output("\tCorrect! Puzzle solved. \n" + resolutionMsg);
            EventPublisher();
        }

        /// <summary>
        /// Event Publisher raises needed events
        ///     If RoomUnlock is not null, unlock all doors to and from that roomtype
        ///     if rewards inventory is not empty drop all reward items
        /// </summary>
        public virtual void EventPublisher()
        {
            //raise event that puzzle has been solved IF a room was stored
            if (RoomUnlock != RoomType.Null)
            {
                RoomArgs args = new RoomArgs();
                args.RoomType = RoomUnlock;
                GameController.Publish(EventType.UnlockRoom, args);
            }

            //raise rewards Inventory holds any items, drop the into room
            if (!Rewards.IsEmpty())
            {
                InventoryArgs args = new InventoryArgs();
                args.Inventory = Rewards;
                GameController.Publish(EventType.DropInventory, args);
            }
        }
    }
}
