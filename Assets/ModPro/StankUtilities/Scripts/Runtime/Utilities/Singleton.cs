using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Generic singleton class that converts any MonoBehavior into a singleton.
    /// </summary>
    /// <typeparam name="T">Type that we want to convert to a singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T s_Instance = null;

        #region Properties

        /// <summary>
        /// Returns the instance of the class.
        /// </summary>
        public static T Instance
        {
            get
            {
                // If the instance is null, try to find it.
                if(s_Instance == null)
                {
                    // Search everywhere for the type.
                    s_Instance = FindObjectOfType<T>();

                    // If we still didn't find it, create one.
                    if(s_Instance == null)
                    {
                        // Create the game object.
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;

                        // Add this type to the object.
                        s_Instance = obj.AddComponent<T>();
                    }
                }

                return s_Instance;
            }
        }

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            // If the instance is null, create and/or retrieve it.
            if(s_Instance == null)
            {
                s_Instance = this as T;
            }
            else // If there is already an instance in the scene, destroy this one.
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}