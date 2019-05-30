using System.Collections.Generic;

using ModPro.Runtime.Core;

using StankUtilities.Runtime.Data;

namespace ModPro.Runtime.Data
{
    /// <summary>
    /// Class that handles all settings about mods.
    /// </summary>
    public class ModSettings : BaseSettings
    {
        private const string k_ModsFolderKey = "ModsFolder";
        private const string k_ModsListKey = "Mods";

        private string m_ModsFolder = "";
        private List<Mod> m_Mods = new List<Mod>();

        #region Constructor

        /// <summary>
        /// Initializes the settings object.
        /// </summary>
        /// <param name="filePath">Path at which the settings will be saved and loaded.</param>
        /// <param name="modsPath">Path at which mods are stored.</param>
        public ModSettings(string filePath, string modsPath) : base(filePath)
        {
            // Set the mods folder.
            ModsFolder = modsPath;

            // Try to load settings.
            if(!Load())
            {
                // Add default settings.
                OnInitialSetup();

                // Save the settings.
                Save();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the path to the mods folder.
        /// </summary>
        public string ModsFolder
        {
            get
            {
                return m_ModsFolder;
            }

            set
            {
                // Set the mods folder value in the settings file.
                SetSetting<string>(k_ModsFolderKey, value);

                m_ModsFolder = value;
            }
        }

        /// <summary>
        /// Returns a list of all of the mods.
        /// </summary>
        public List<Mod> Mods
        {
            get
            {
                return m_Mods;
            }

            set
            {
                // Set the mods value in the settings file.
                SetSetting<List<Mod>>(k_ModsListKey, value);

                m_Mods = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Invoked whe the settings are saved.
        /// </summary>
        public override void OnSave() { }

        /// <summary>
        /// Invoked when the settings are loaded.
        /// </summary>
        public override void OnLoad()
        {
            // Load the mod folder path.
            ModsFolder = LoadSetting<string>(k_ModsFolderKey);

            // Load the mods from the settings.
            Mods = LoadSetting<List<Mod>>(k_ModsListKey);
        }

        /// <summary>
        /// Sets up the base mod settings.
        /// </summary>
        public override void OnInitialSetup()
        {
            // Add mods to the settings data.
            SettingsData.Add(new Setting(k_ModsFolderKey, ModsFolder));
            SettingsData.Add(new Setting(k_ModsListKey, Mods));
        }

        #endregion
    }
}
