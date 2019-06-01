using System.IO;

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
            // Setup default directories.
            string defaultSettingsPath = string.IsNullOrWhiteSpace(ModProManager.SettingsPath) ? Application.dataPath + "/Game/Settings/" : ModProManager.SettingsPath;
            string defaultModsPath = string.IsNullOrWhiteSpace(ModProManager.ModsPath) ? Application.dataPath + "/Game/Mods/" : ModProManager.ModsPath;

            // Create default directories.
            Directory.CreateDirectory(defaultSettingsPath);
            Directory.CreateDirectory(defaultModsPath);

            // Initialize ModPro.
            ModProManager.InitializeModSettings(defaultSettingsPath + "mod_settings.json", defaultModsPath);
        }

        #endregion
    }
}
