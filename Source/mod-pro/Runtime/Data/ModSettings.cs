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
        private const string k_ModsListKey = "Mods";

        #region Constructor

        /// <summary>
        /// Initializes the settings object.
        /// </summary>
        /// <param name="filePath">Path at which the settings will be saved and loaded.</param>
        public ModSettings(string filePath) : base(filePath) { }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a list of all of the mods.
        /// </summary>
        public List<Mod> Mods { get; set; } = new List<Mod>();

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
            // Load the mods from the settings.
            Mods = LoadSetting<List<Mod>>(k_ModsListKey);
        }

        /// <summary>
        /// Sets up the base mod settings.
        /// </summary>
        public override void OnInitialSetup()
        {
            // Create initial list of mods.
            Mods = new List<Mod>();
            Mods.Add(new Mod("mymod.zip", "My Mod", "main.lua"));

            //Mods.Add(new Mod("More Grass", 1));
            //Mods.Add(new Mod("Better Combat", 2));
            //Mods.Add(new Mod("Hot Females", 3));
            
            // Add mods to the settings data.
            SettingsData.Add(new Setting(k_ModsListKey, Mods));

            // Save the settings.
            Save(Newtonsoft.Json.Formatting.Indented);
        }

        #endregion
    }
}
