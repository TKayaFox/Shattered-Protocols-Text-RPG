using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management;
using Shattered_Protocols.Event_Management.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    public static class GameController
    {
        // Expose a static instance of EventManager
        private static EventManager _eventManager = new EventManager();

        // Property to get the global EventManager instance
        public static EventManager EventManager => _eventManager;

        #region EventManager Relay Commands
        //The following methods simply relay inputs the the EventManager class
        //  Slightly improves code readability

        public static void Subscribe(EventType eventType, Action<EventArgs> listener)
        {
            EventManager.Subscribe(eventType, listener);
        }

        public static void Unsubscribe(EventType eventType, Action<EventArgs> listener)
        {
            EventManager.Unsubscribe(eventType, listener);
        }

        // Method for publishing an event to notify all listeners
        public static void Publish(EventType eventType, EventArgs args = null)
        {
            EventManager.Publish(eventType, args);
        }

        /// <summary>
        /// Tells the eventManager to remove ALL subscribers
        /// </summary>
        public static void Reset()
        {
            EventManager.Reset();
        }


        // Subscribe to all desired events for an object that implements IEventManagable
        public static void ManageObject(IEventManagable objectToManage)
        {
            objectToManage.ManageMe();
        }
        #endregion

        #region Input/Output

        //Globally handle text input and output
        public static void Output(string output)
        {

            //Raise an event printing message for user and logging
            NewLineArgs args = new NewLineArgs();
            args.Line = output;

            Publish(EventType.Output, args);
        }
        public static void GetInput()
        {
            string input = Console.ReadLine().Trim();

            //Raise an event printing message for user and logging
            NewLineArgs args = new NewLineArgs();
            args.Line = input;

            Publish(EventType.Input, args);
        }
        #endregion
    }
}
