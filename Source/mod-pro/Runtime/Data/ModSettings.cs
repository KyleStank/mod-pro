using System.Collections.Generic;
using System.IO;

using ModPro.Runtime.Core;

using StankUtilities.Runtime.Data;
using StankUtilities.Runtime.Utilities;

namespace ModPro.Runtime.Data
{
    /// <summary>
    /// Class that handles all settings about mods.
    /// </summary>
    public class ModSettings : BaseSettings
    {
        private const string k_ModsFolderKey = "ModsFolder";
        private const string k_ModsListKey = "Mods";
        private const string k_EntitiesListKey = "Entities";

        private const string k_MainModFileName = "mod.json";
        private const string k_TempModsFolder = "mods_temp";

        private string m_ModsFolder = "";
        private List<Mod> m_Mods = new List<Mod>();
        private List<EntityData> m_Entities = new List<EntityData>();

        #region Constructor

        /// <summary>
        /// Initializes the settings object.
        /// </summary>
        /// <param name="filePath">Path at which the settings will be saved and loaded.</param>
        /// <param name="modsPath">Path at which mods are stored.</param>
        public ModSettings(string filePath, string modsPath) : base(filePath)
        {
            // If the mods path is empty, do not proceed.
            if(string.IsNullOrWhiteSpace(modsPath))
            {
                return;
            }

            // Set the mods folder.
            ModsFolder = modsPath;

            // Create the directory if it doesn't exist.
            if(!Directory.Exists(ModsFolder))
            {
                Directory.CreateDirectory(ModsFolder);
            }

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

        /// <summary>
        /// Returns the list of Entities.
        /// </summary>
        public List<EntityData> Entities
        {
            get
            {
                return m_Entities;
            }

            set
            {
                // Set the entites value in the settings file.
                SetSetting<List<EntityData>>(k_EntitiesListKey, value);

                m_Entities = value;
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

            // Load the entities from the settings.
            Entities = LoadSetting<List<EntityData>>(k_EntitiesListKey);
        }

        /// <summary>
        /// Sets up the base mod settings.
        /// </summary>
        public override void OnInitialSetup()
        {
            // Scan the mods directory.
            ScanModDirectory();

            // Add mods to the settings data.
            SettingsData.Add(new Setting(k_ModsFolderKey, ModsFolder));
            SettingsData.Add(new Setting(k_ModsListKey, Mods));
            SettingsData.Add(new Setting(k_EntitiesListKey, Entities));
        }

        /// <summary>
        /// Scans the mod directory to check for any mods.
        /// </summary>
        public void ScanModDirectory()
        {
            // Empty the current Mods list.
            Mods = new List<Mod>();

            // Scan for directory mods.
            ScanForDirectoryMods();

            // Scan for ZIP mods.
            ScanForZIPMods();

            // Move mods to the temporary mods directory.
            CreateTempForMods();
        }

        /// <summary>
        /// Adds a Mod to the main Mods list.
        /// </summary>
        /// <param name="mod">Mod to add to the list.</param>
        /// <param name="filePath">Custom file path to give to Mod.</param>
        /// <returns>Returns a bool that is true if the Mod is successfully added. Returns false otherwise.</returns>
        public bool AddMod(Mod mod, string filePath = "")
        {
            if(mod == null)
            {
                DebuggerUtility.LogError("Cannot add Mod to main Mod list because the provided Mod is null!");
                return false;
            }

            // If a custom file path was provided, set it here.
            if(!string.IsNullOrWhiteSpace(filePath))
            {
                mod.FilePath = filePath;
            }

            // Add mod to list.
            Mods.Add(mod);

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Scans the Mods folder for mods that are inside a regular folder.
        /// </summary>
        private void ScanForDirectoryMods()
        {
            // Find all directories in the mods folder.
            string[] modFolderDirectories = Directory.GetDirectories(ModsFolder);

            // Loop through all found directories.
            for(int i = 0; i < modFolderDirectories.Length; i++)
            {
                // Skip temporary mods folder.
                if(modFolderDirectories[i].ToLower().Contains(k_TempModsFolder.ToLower()))
                {
                    continue;
                }

                // Get all files in the current directory.
                string[] files = Directory.GetFiles(modFolderDirectories[i]);

                // Search for the "mod.json" file.
                for(int k = 0; k < files.Length; k++)
                {
                    // If "mod.json" is found, set the temporary mod object.
                    if(Path.GetFileName(files[k]).ToLower() == k_MainModFileName.ToLower())
                    {
                        if(AddMod(JSONUtility.DeserializeObject<Mod>(File.OpenText(files[k]).ReadToEnd()), modFolderDirectories[i]))
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Scans the Mods folder for mods that are inside of a ZIP archive.
        /// </summary>
        private void ScanForZIPMods()
        {
            // Find all files in the mods folder.
            string[] modFolderFiles = Directory.GetFiles(ModsFolder);

            // Loop through all found files.
            for(int i = 0; i < modFolderFiles.Length; i++)
            {
                // Skip temporary mods folder.
                if(modFolderFiles[i].ToLower().Contains(k_TempModsFolder.ToLower()))
                {
                    continue;
                }

                // If the current file is a zip file, check if it is a mod.
                if(IOUtility.IsZipFile(modFolderFiles[i]))
                {
                    // Open zip file.
                    IOUtility.OpenZIPArchive(modFolderFiles[i], (file, zipArchive, entry, stream) =>
                    {
                        // If "mod.json" is found, set the temporary mod object.
                        if(entry.Name.ToLower() == k_MainModFileName.ToLower())
                        {
                            if(AddMod(JSONUtility.DeserializeObject<Mod>(new StreamReader(stream).ReadToEnd()), modFolderFiles[i]))
                            {
                                return;
                            }
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Creates a temporary folder where all mods will be loaded from.
        /// </summary>
        private void CreateTempForMods()
        {
            string path = ModsFolder + k_TempModsFolder;

            // Create the temporary directory if it doesn't exist.
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Get temp directory's information.
            DirectoryInfo tempDirectory = new DirectoryInfo(path);

            // Remove all files in temporary folder.
            FileInfo[] files = tempDirectory.GetFiles();
            for(int i = 0; i < files.Length; i++)
            {
                files[i].Delete();
            }

            // Remove all directories in temporary folder.
            DirectoryInfo[] directories = tempDirectory.GetDirectories();
            for(int i = 0; i < directories.Length; i++)
            {
                directories[i].Delete(true);
            }

            // Copy all mods to temporary directory.
            for(int i = 0; i < Mods.Count; i++)
            {
                string destPath = tempDirectory + "/" + Mods[i].Name + "_" + i;

                // Create destination folder.
                if(!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }

                // If current mod is simply a directory, copy it to the temporary folder.
                if(Directory.Exists(Mods[i].FilePath))
                {
                    // Copy mod's contents.
                    IOUtility.DirectoryCopy(Mods[i].FilePath, destPath, true);

                    // Set mod's temporary path.
                    Mods[i].TempFilePath = destPath;
                }

                // If current mod if a zip file, uncompress it and then copy it to the temporary folder.
                if(IOUtility.IsZipFile(Mods[i].FilePath))
                {
                    // Extract mod's contents.
                    IOUtility.ExtractZIPArchive(Mods[i].FilePath, destPath);

                    // Set mod's temporary path.
                    Mods[i].TempFilePath = destPath;
                }
            }
        }

        #endregion
    }
}
