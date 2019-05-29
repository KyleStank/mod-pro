using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that makes Unity log messages more pretty.
    /// </summary>
    public static class DebuggerUtility
    {
        public enum LogFormat
        {
            Log,
            Warning,
            Error
        }

        private const string k_PrefixKey = "StankUtilities_LogPrefix";
        private const string k_ColorKey = "StankUtilities_LogColor";

        private static string s_LogPrefix = "StankUtilities";
        private static Color s_LogColor = new Color(183.0f, 34.0f, 35.0f, 255.0f);

        #region Properties

        /// <summary>
        /// The prefix that is displayed in front of log messages.
        /// </summary>
        public static string LogPrefix
        {
            get
            {
                // Try to get the current prefix.
                string prefix = PlayerPrefs.GetString(k_PrefixKey);

                // If a prefix was found, return that. If not, return the last prefix set.
                return string.IsNullOrWhiteSpace(prefix) ? s_LogPrefix : prefix;
            }

            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                // Save the prefix in PlayerPrefs.
                PlayerPrefs.SetString(k_PrefixKey, value);

                // Set the prefix variable.
                s_LogPrefix = value;
            }
        }

        /// <summary>
        /// The color of the prefix that is displayed in front of log messages.
        /// </summary>
        public static Color LogColor
        {
            get
            {
                // If the color has been saved, retrieve it now.
                if(PlayerPrefs.HasKey(k_ColorKey))
                {
                    // Try to get the current color.
                    string color = PlayerPrefs.GetString(k_ColorKey);

                    // Try to convert string to a Color object.
                    Color newColor;
                    if(ColorUtility.TryParseHtmlString(color, out newColor))
                    {
                        s_LogColor = newColor;
                    }
                }

                return s_LogColor;
            }

            set
            {
                if(value == null)
                {
                    return;
                }

                // Save the Color in PlayerPrefs.
                PlayerPrefs.SetString(k_ColorKey, "#" + ColorUtility.ToHtmlStringRGBA(value));

                // Set the prefix variable.
                s_LogColor = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs a message to the Unity console with pretty formatting.
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(object msg)
        {
            PrettyDebug(msg, LogFormat.Log);
        }

        /// <summary>
        /// Logs a warning to the Unity console with pretty formatting.
        /// </summary>
        /// <param name="msg"></param>
        public static void LogWarning(object msg)
        {
            PrettyDebug(msg, LogFormat.Warning);
        }

        /// <summary>
        /// Logs an error to the Unity console with pretty formatting.
        /// </summary>
        /// <param name="msg"></param>
        public static void LogError(object msg)
        {
            PrettyDebug(msg, LogFormat.Error);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Prints a pretty Unity log statement with color and text formatting.
        /// </summary>
        /// <param name="msg">The message that we want to print.</param>
        /// <param name="logFormat">The type of log that we want to print.</param>
        private static void PrettyDebug(object msg, LogFormat logFormat = LogFormat.Log)
        {
            if(logFormat == LogFormat.Warning) // Log a Warning.
            {
                Debug.LogWarning("<b><color=#" + ColorUtility.ToHtmlStringRGBA(LogColor) + ">[" + LogPrefix + "]</color> " + msg.ToString() + "</b>");
            }
            else if(logFormat == LogFormat.Error) // Log an Error.
            {
                Debug.LogError("<b><color=#" + ColorUtility.ToHtmlStringRGBA(LogColor) + ">[" + LogPrefix + "]</color> " + msg.ToString() + "</b>");
            }
            else // Log a regular message.
            {
                Debug.Log("<b><color=#" + ColorUtility.ToHtmlStringRGBA(LogColor) + ">[" + LogPrefix + "]</color> " + msg.ToString() + "</b>");
            }
        }

        #endregion
    }
}
