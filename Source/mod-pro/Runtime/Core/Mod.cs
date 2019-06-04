using ModPro.Runtime.Utilities;

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
            // Execute the mod's script!
            LuaUtility.ExecuteLuaScript(FilePath, Script, new LuaAPIBase());
        }

        #endregion
    }
}
