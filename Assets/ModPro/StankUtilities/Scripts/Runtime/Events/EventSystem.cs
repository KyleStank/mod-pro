using System.Collections.Generic;

using UnityEngine.Events;

namespace StankUtilities.Runtime.Events
{
    /// <summary>
    /// Generic event system that allows for global and object-specific events in any Unity project.
    /// </summary>
    public static class EventSystem
    {
        private static Dictionary<object, Dictionary<string, UnityEvent>> s_ObjectEventDictionary = new Dictionary<object, Dictionary<string, UnityEvent>>();
        private static Dictionary<string, UnityEvent> s_GlobalEventDictionary = new Dictionary<string, UnityEvent>();

        #region Public Methods

        /// <summary>
        /// Subscribe a UnityAction to a global event.
        /// </summary>
        /// <param name="eventName">Name of the global event.</param>
        /// <param name="listener">UnityAction to subscribe.</param>
        public static void StartListening(string eventName, UnityAction listener)
        {
            if(listener == null)
            {
                return;
            }

            // Try to retrieve the event and store its value in the empty UnityEvent.
            UnityEvent newEvent = null;
            if(s_GlobalEventDictionary.TryGetValue(eventName, out newEvent))
            {
                newEvent.AddListener(listener);
            }
            else // If the event doesn't exist.
            {
                // Add the provided UnityAction as a listener of the new UnityEvent.
                newEvent = new UnityEvent();
                newEvent.AddListener(listener);

                // Add this event as a new event to the dictionary.
                s_GlobalEventDictionary.Add(eventName, newEvent);
            }
        }

        /// <summary>
        /// Subscribe a UnityAction to an object-specific event.
        /// </summary>
        /// <param name="obj">Object that event is attached to.</param>
        /// <param name="eventName">Name of the object-specific event.</param>
        /// <param name="listener">UnityAction to subscribe.</param>
        public static void StartListening(object obj, string eventName, UnityAction listener)
        {
            if(listener == null)
            {
                return;
            }

            // Search the object dictionary to see if this object has already been used. Store the value in the empty dictionary.
            Dictionary<string, UnityEvent> objectDict = null;
            if(s_ObjectEventDictionary.TryGetValue(obj, out objectDict))
            {
                // Try to retrieve the event from the object dictionary and store its value in the empty UnityEvent.
                UnityEvent newEvent = null;
                if(objectDict.TryGetValue(eventName, out newEvent))
                {
                    newEvent.AddListener(listener);
                }
            }
            else // If the event doesn't exist.
            {
                // Add the provided UnityAction as a listener of the new UnityEvent.
                UnityEvent newEvent = new UnityEvent();
                newEvent.AddListener(listener);

                // Create a new event dictionary for the object.
                objectDict = new Dictionary<string, UnityEvent>();
                objectDict.Add(eventName, newEvent);

                // Add the object and its event dictionary to the main object dictionary.
                s_ObjectEventDictionary.Add(obj, objectDict);
            }
        }

        /// <summary>
        /// Unsubscribe a UnityAction from a global event.
        /// </summary>
        /// <param name="eventName">Name of the global event.</param>
        /// <param name="listener">UnityAction to unsubscribe.</param>
        public static void StopListening(string eventName, UnityAction listener)
        {
            // Try to retrieve the event and store its value in the empty UnityEvent.
            UnityEvent newEvent = null;
            if(s_GlobalEventDictionary.TryGetValue(eventName, out newEvent))
            {
                newEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// Unsubscribe a UnityAction from an object-specific event.
        /// </summary>
        /// <param name="obj">Object that event is attached to.</param>
        /// <param name="eventName">Name of the object-specific event.</param>
        /// <param name="listener">UnityAction to unsubscribe.</param>
        public static void StopListening(object obj, string eventName, UnityAction listener)
        {
            // Search the object's event dictionary to see if this object has already been used. Store the value in the empty dictionary.
            Dictionary<string, UnityEvent> objectDict = null;
            if(s_ObjectEventDictionary.TryGetValue(obj, out objectDict))
            {
                // Try to retrieve the event from the object's event dictionary and store its value in the empty UnityEvent.
                UnityEvent newEvent = null;
                if(objectDict.TryGetValue(eventName, out newEvent))
                {
                    newEvent.RemoveListener(listener);
                }
            }
        }

        /// <summary>
        /// Triggers a global event.
        /// </summary>
        /// <param name="eventName">Name of the global event.</param>
        public static void TriggerEvent(string eventName)
        {
            // Try to retrieve the event and store its value in the empty UnityEvent.
            UnityEvent newEvent = null;
            if(s_GlobalEventDictionary.TryGetValue(eventName, out newEvent))
            {
                newEvent.Invoke();
            }
        }

        /// <summary>
        /// Trigger an object-specific event.
        /// </summary>
        /// <param name="obj">Object that event is attached to.</param>
        /// <param name="eventName">Name of the object-specific event.</param>
        public static void TriggerEvent(object obj, string eventName)
        {
            // Search the object's event dictionary to see if this object has already been used. Store the value in the empty dictionary.
            Dictionary<string, UnityEvent> objectDict = null;
            if(s_ObjectEventDictionary.TryGetValue(obj, out objectDict))
            {
                // Try to retrieve the event from the object's event dictionary and store its value in the empty UnityEvent.
                UnityEvent newEvent = null;
                if(objectDict.TryGetValue(eventName, out newEvent))
                {
                    newEvent.Invoke();
                }
            }
        }

        #endregion
    }
}
