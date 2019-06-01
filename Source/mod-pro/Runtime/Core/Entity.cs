using UnityEngine;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Base class that controls parts of a GameObject that can be easily replaced through mods.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        /// <summary>
        /// Contains data that a Entity can use.
        /// </summary>
        [SerializeField]
        private EntityData m_EntityData = null;

        #region Properties

        /// <summary>
        /// Data the the Entity uses.
        /// </summary>
        public EntityData Data
        {
            get
            {
                return m_EntityData;
            }

            set
            {
                m_EntityData = value;
            }
        }

        #endregion
    }
}
