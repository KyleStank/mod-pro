using UnityEngine;

using ModPro.Runtime.Data;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Main class that managed all of ModPro.
    /// </summary>
    public static class ModProManager
    {
        // TODO: Try to refactor this class so it is not so reliant on other scripts calling on the "SettingsPath" and "ModsPath" properties.

        private const string k_SettingsPathKey = "MP_SettingsPath";
        private const string k_ModsPath = "MP_ModsPath";

        private static string s_SettingsPath = "";
        private static string s_ModsPath = "";

        #region Properties

        /// <summary>
        /// Returns an instance to ModPro's ModSettings object.
        /// </summary>
        public static ModSettings TheModSettings { get; private set; } = null;

        /// <summary>
        /// Returns the path of where the ModSettings file is stored.
        /// </summary>
        public static string SettingsPath
        {
            get
            {
                // Try to retrieve the value from PlayerPrefs.
                s_SettingsPath = PlayerPrefs.GetString(k_SettingsPathKey);

                return s_SettingsPath;
            }

            set
            {
                // Save the value in PlayerPrefs.
                PlayerPrefs.SetString(k_SettingsPathKey, value);

                s_SettingsPath = value;
            }
        }

        /// <summary>
        /// Returns the path of where mods are stored.
        /// </summary>
        public static string ModsPath
        {
            get
            {
                // Try to retrieve the value from PlayerPrefs.
                s_ModsPath = PlayerPrefs.GetString(k_ModsPath);

                return s_ModsPath;
            }

            set
            {
                // Save the value in PlayerPrefs.
                PlayerPrefs.SetString(k_ModsPath, value);

                s_ModsPath = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the ModSettings object.
        /// </summary>
        public static void InitializeModSettings()
        {
            // If the settings path is empty or the mods folder path is empty, do not proceed.
            if(string.IsNullOrWhiteSpace(SettingsPath) || string.IsNullOrWhiteSpace(ModsPath))
            {
                return;
            }

            // Create new instance of ModSettings!
            TheModSettings = new ModSettings(SettingsPath, ModsPath);
        }

        /// <summary>
        /// Initializes the ModSettings object.
        /// </summary>
        /// <param name="settingsPath">Path of the settings file.</param>
        /// <param name="modsPath">Path of where mods are stored.</param>
        public static void InitializeModSettings(string settingsPath, string modsPath)
        {
            // If any parameters are empty, do not proceed.
            if(string.IsNullOrWhiteSpace(settingsPath) || string.IsNullOrWhiteSpace(modsPath))
            {
                return;
            }

            // Set the settings and mod paths.
            SettingsPath = settingsPath;
            ModsPath = modsPath;

            // Initialize ModSettings!
            InitializeModSettings();
        }

        #endregion
    }
}
