using UnityEngine;

using ModPro.Runtime.Core;

namespace ModPro.Runtime
{
    /// <summary>
    /// Optional class that initializes all of the required directories and files for ModPro.
    /// This class can simply be ignored if custom "default" configuration files are required.
    /// </summary>
    public class ModProInitializer : MonoBehaviour
    {
        #region Unity Methods

        private void Awake()
        {
            // Set default settings files if needed.
            if(string.IsNullOrWhiteSpace(ModProManager.SettingsPath) || string.IsNullOrWhiteSpace(ModProManager.ModsPath))
            {
                ModProManager.SettingsPath = Application.dataPath + "/Game/Settings/mod_settings.json";
                ModProManager.ModsPath = Application.dataPath + "/Game/Mods/";
            }

            // Initialize ModPro.
            ModProManager.InitializeModSettings();
        }

        #endregion
    }
}
