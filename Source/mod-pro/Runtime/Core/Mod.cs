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
        /// <param name="api">API to inject into script.</param>
        public void LoadMod(LuaAPIBase api = null)
        {
            // If the script file exists, execute the mod!
            if(File.Exists(TempFilePath + "/" + Script))
            {
                // If no API was given, inject the default one.
                if(api == null)
                {
                    api = new LuaAPIBase();
                }

                // Execute the mod's main script!
                LuaUtility.ExecuteLuaScript(TempFilePath + "/" + Script, api, new string[] { TempFilePath });
            }
            else
            {
                DebuggerUtility.LogError("Cannot load mod because the Lua script in mod.json does not exist!");
            }
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
