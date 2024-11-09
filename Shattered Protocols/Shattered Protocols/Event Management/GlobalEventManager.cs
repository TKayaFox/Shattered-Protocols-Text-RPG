using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols.Event_Management
{
    public static class GlobalEventManager
    {
        // Expose a static instance of EventManager
        private static EventManager _eventManager = new EventManager();

        // Property to get the global EventManager instance
        public static EventManager EventManager => _eventManager;

        #region EventManager Relay Commands
        //The following methods simply relay inputs the the EventManager class
        //  Slightly improves code readability

        public static void Subscribe(string eventName, Action<EventArgs> listener)
        {
            EventManager.Subscribe(eventName, listener);
        }

        public static void Unsubscribe(string eventName, Action<EventArgs> listener)
        {
            EventManager.Unsubscribe(eventName, listener);
        }

        // Method for publishing an event to notify all listeners
        public static void Publish(string eventName, EventArgs args = null)
        {
            EventManager.Publish(eventName, args);
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
            EventManager.ManageObject(objectToManage);
        }
        #endregion
    }
}
