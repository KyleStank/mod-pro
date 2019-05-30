using System.Collections.Generic;

using UnityEngine;

using ModPro.Runtime.Modding;

using StankUtilities.Runtime.Utilities;

namespace ModPro.Runtime.Game
{
    /// <summary>
    /// The main Mod manager that handles all mods for the game.
    /// </summary>
    public class ModManager : Singleton<ModManager>
    {
        [SerializeField]
        private string m_ActiveModName = "";

        #region Properties

        /// <summary>
        /// Returns the list of all mods.
        /// </summary>
        public List<Mod> Mods { get; set; } = null;

        /// <summary>
        /// Returns the mod that is currently active.
        /// </summary>
        public Mod ActiveMod { get; set; } = null;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            // Activate the mod!
            if(!string.IsNullOrWhiteSpace(m_ActiveModName))
            {
                ActivateMod(FindMod(m_ActiveModName));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activates a mod.
        /// </summary>
        /// <param name="mod">Mod to activate.</param>
        public void ActivateMod(Mod mod)
        {
            if(mod == null)
            {
                DebuggerUtility.LogWarning("Couldn't activate mod because it was null!");
                return;
            }

            // Set the active mod.
            ActiveMod = mod;

            // Load the mod.
            ActiveMod.LoadMod();
        }

        /// <summary>
        /// Searches for a mod by name.
        /// </summary>
        /// <param name="name">Name of the mod to search for.</param>
        /// <returns>Returns a Mod reference.</returns>
        public Mod FindMod(string name)
        {
            return Mods == null ? null : Mods.Find(mod => mod.Name.ToLower() == name.ToLower());
        }

        #endregion
    }
}
