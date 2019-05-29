using System.Collections.Generic;

using UnityEngine;

namespace StankUtilities.Runtime.ScriptableObjects.Variables
{
    /// <summary>
    /// Generic ScriptableObject that allows us to give any type an Initial and Runtime value.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public abstract class Variable<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T InitialValue;
        public string SaveName = "";

        private T m_RuntimeValue;

        #region Properties

        /// <summary>
        /// The runtime/current value of the variable. Gets reset when game is started and exited. Any changes DO NOT carry over to the inspector.
        /// </summary>
        public T RuntimeValue
        {
            get
            {
                return m_RuntimeValue;
            }

            set
            {
                if (!EqualityComparer<T>.Default.Equals(m_RuntimeValue, value))
                {
                    m_RuntimeValue = value;
                }
            }
        }

        #endregion

        #region Unity Methods

        /// <summary>
        /// Set the runtime value to be equal to the inital value when the game begins.
        /// </summary>
        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the value of the variable.
        /// </summary>
        /// <param name="value">Value to set variable to.</param>
        public void SetValue(T value)
        {
            RuntimeValue = value;
        }

        /// <summary>
        /// Allows the RuntimeValue to be set by 
        /// </summary>
        /// <param name="variable"></param>
        public static implicit operator T(Variable<T> variable)
        {
            if(variable == null)
            {
                return default(T);
            }

            return variable.RuntimeValue;
        }

        #endregion
    }
}
