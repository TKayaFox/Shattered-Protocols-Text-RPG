using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    /*
        Dev Note: I'm trying a new way of simplifying the eventManager. 
        In the past i manually wrote a relay for each event. This was straightforward but really bloaty.
        This time I am attempting to use "Delegates" in order to make the eventmanager work with ANY event types.
        Strictly speaking, this is NOT using C# Events, it is mimicking the behavior
            Instead it stores a list of Delegate methods to call when an event is raised.
            This is similar, but let's us keep the eventManager light, and keep everything decoupled.
    /*/
    /// <summary>
    /// Rather than using the standard Event system this Event Manager is Delegate based.
    ///     This allows us to simply use a delegate dictionary, rather than having to update eventManager with every new Event Type as it is created.
    /// </summary>
    public class EventManager
    {
        //Dictionary to hold events and their associated listeners
        /*/ String used to reference events by type
            Action<> to hold the delegated event method
                object to pass eventargs or other through the event
        /*/
        private Dictionary<EventType, Action<EventArgs>>
            eventDictionary = new Dictionary<EventType, Action<EventArgs>>();

        /// <summary>
        /// Subscribe method allows an object to add a method to be called any time a specific event type is raised
        /// </summary>
        /// <param name="eventType">Type of event to subscribe to</param>
        /// <param name="listener">Delegate Method to call when the event is raised</param>
        public void Subscribe(EventType eventType, Action<EventArgs> listener)
        {
            //Check if such an event exists in the dictionary yet
            if (!eventDictionary.ContainsKey(eventType))
            {
                // Initialize the event entry if it doesn't exist
                eventDictionary[eventType] = delegate { };
            }
            // Add listener to the event
            eventDictionary[eventType] += listener;
        }

        /// <summary>
        /// Subscribe method allows an object to add a method to be called any time a specific event type is raised
        /// </summary>
        /// <param name="eventType">Type of event to unsubscribe from</param>
        /// <param name="listener">Delegate Method call to remove from dictionary</param>
        public void Unsubscribe(EventType eventType, Action<EventArgs> listener)
        {
            if (eventDictionary != null && eventDictionary.ContainsKey(eventType))
            {
                // Remove listener from the event
                eventDictionary[eventType] -= listener;

                // Clean up if no listeners remain
                if (eventDictionary[eventType] == null)
                {
                    eventDictionary.Remove(eventType);
                }
            }
        }

        /// <summary>
        /// Method for publishing an event to notify all listeners
        /// </summary>
        /// <param name="eventType">Type of event to raise</param>
        /// <param name="args">An EventArgs object that contains any needed information</param>
        public void Publish(EventType eventType, EventArgs args)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                // Invoke all listeners associated with the event
                eventDictionary[eventType]?.Invoke(args);
            }
        }

        /// <summary>
        /// Tells the eventManager to remove ALL subscribers
        /// </summary>
        public void Reset()
        {
            eventDictionary.Clear();
        }
    }
}
