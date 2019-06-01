using StankUtilities.Runtime.Utilities;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Class that contains data about entites.
    /// </summary>
    public class EntityData
    {
        #region Constructor

        /// <summary>
        /// Initialize the EntityData object.
        /// </summary>
        /// <param name="name">Name to give the entity.</param>
        public EntityData(string name)
        {
            // Set the name.
            Name = name;

            // Generate the ID.
            ID = MathUtility.GenerateGUID();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the Entity.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Returns the ID of the Entity.
        /// </summary>
        public string ID { get; private set; } = "";

        #endregion
    }
}
