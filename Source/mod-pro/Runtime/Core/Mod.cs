using System.IO;

using ModPro.Runtime.Utilities;

using StankUtilities.Runtime.Utilities;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Class that creates a mod.
    /// </summary>
    public class Mod
    {
        #region Constructor

        /// <summary>
        /// Initializes the Mod object.
        /// </summary>
        /// <param name="filePath">Path to the mod.</param>
        /// <param name="name">Name of the mod.</param>
        /// <param name="script">Path to the mod's main script.</param>
        public Mod(string filePath, string name, string script)
        {
            FilePath = filePath;
            Name = name;
            Script = script;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the path of the mod.
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// Returns the temporary path of the mod.
        /// </summary>
        public string TempFilePath { get; set; } = "";

        /// <summary>
        /// Returns the name of the mod.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Returns the mod's main script.
        /// </summary>
        public string Script { get; set; } = "";

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the mod.
        /// </summary>
        public void LoadMod()
        {
            // Check if mod is a ZIP archive mod.
            if(IOUtility.IsZipFile(FilePath))
            {
                //DebuggerUtility.Log("is zip file!");
                DebuggerUtility.Log(FilePath);
            }
            else if(Directory.Exists(FilePath)) // Make sure mod directory exists.
            {
                //DebuggerUtility.Log("is a folder mod!");
            }
            else
            {
                DebuggerUtility.LogError("Could not load mod because it's file path was not properly found!");
            }

            // Execute the mod's script!
            //LuaUtility.ExecuteLuaScript(FilePath, Script, new LuaAPIBase());
        }

        /// <summary>
        /// Ensures that the FilePath property is not serialized in the mod's configuration file.
        /// </summary>
        /// <returns>Returns a bool that is always false.</returns>
        public bool ShouldSerializeFilePath()
        {
            return false;
        }

        /// <summary>
        /// Ensures that the TempFilePath property is not serialized in the mod's configuration file.
        /// </summary>
        /// <returns>Returns a bool that is always false.</returns>
        public bool ShouldSerializeTempFilePath()
        {
            return false;
        }

        #endregion
    }
}
