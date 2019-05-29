using UnityEngine;

namespace ModPro.Runtime.Game
{
    /// <summary>
    /// ScriptableObject that contains useful information for Entities.
    /// </summary>
    [CreateAssetMenu(fileName = "Entity Data", menuName = "ModPro/Entity Data", order = 1)]
    public class EntityData : ScriptableObject
    {
        [SerializeField]
        private string m_EntityName = "";
        [SerializeField]
        private string m_ID = "";

        #region Properties

        /// <summary>
        /// Returns the name of the Entity.
        /// </summary>
        public string EntityName
        {
            get
            {
                return m_EntityName;
            }

            set
            {
                if(!Application.IsPlaying(this))
                {
                    m_EntityName = value;
                }
            }
        }

        /// <summary>
        /// Returns the ID of the Entity.
        /// </summary>
        public string ID
        {
            get
            {
                return m_ID;
            }

            set
            {
                if(!Application.IsPlaying(this))
                {
                    m_ID = value;
                }
            }
        }

        #endregion
    }
}
