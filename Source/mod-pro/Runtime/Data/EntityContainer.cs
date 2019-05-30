namespace ModPro.Runtime.Data
{
    /// <summary>
    /// Class that acts as a "container" for an entity's data.
    /// </summary>
    public class EntityContainer
    {
        private Core.EntityData m_EntityData = null;

        #region Constructor

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="entityData">EntityData that this class will use.</param>
        public EntityContainer(Core.EntityData entityData)
        {
            m_EntityData = entityData;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the Entity.
        /// </summary>
        public string EntityName
        {
            get
            {
                return m_EntityData == null ? "" : m_EntityData.EntityName;
            }
        }

        /// <summary>
        /// Returns the ID of the Entity.
        /// </summary>
        public string ID
        {
            get
            {
                return m_EntityData == null ? "" : m_EntityData.ID;
            }
        }

        #endregion
    }
}
