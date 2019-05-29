using System.Linq;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that makes dealing with reflection easier.
    /// </summary>
    public static class ReflectionUtility
    {
        #region Public Methods

        /// <summary>
        /// Finds all types of a generic type T that are a sub class of type T.
        /// </summary>
        /// <typeparam name="T">Generic type that will be the parent class.</typeparam>
        /// <returns>Returns a Type Array of all sub classes of the generic type T.</returns>
        public static System.Type[] GetSubClassesOf<T>()
        {
            // Get all types that are in the same assembly as the generic types.
            System.Type[] allTypesInAssembly = System.Reflection.Assembly.GetAssembly(typeof(T)).GetTypes();

            // Find all of the types that are a sub class of the generic type by searching through all of the types from above.
            System.Type[] subClassTypes = (from System.Type type in allTypesInAssembly where type.IsSubclassOf(typeof(T)) select type).ToArray();

            return subClassTypes;
        }

        #endregion
    }
}
