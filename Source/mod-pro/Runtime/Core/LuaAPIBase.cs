using StankUtilities.Runtime.Utilities;

using MoonSharp.Interpreter;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Base API class that adds basic functions to the Lua API.
    /// </summary>
    [MoonSharpUserData]
    public class LuaAPIBase
    {
        #region Public Methods

        /// <summary>
        /// Logs a message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void Log(object msg)
        {
            DebuggerUtility.Log(msg);
        }

        /// <summary>
        /// Logs a warning message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void LogWarning(object msg)
        {
            DebuggerUtility.LogWarning(msg);
        }

        /// <summary>
        /// Logs an error message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void LogError(object msg)
        {
            DebuggerUtility.LogError(msg);
        }

        #endregion
    }
}
