using StankUtilities.Runtime.Utilities;

using Newtonsoft.Json;

namespace ModPro.Runtime.Modding
{
    /// <summary>
    /// Class that creates a mod.
    /// </summary>
    public class Mod
    {
        #region Properties

        /// <summary>
        /// Returns the path of the mod file.
        /// </summary>
        [JsonIgnore]
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
            //IOUtility.ExecuteLuaScript(FilePath, Script);
        }

        #endregion
    }
}
