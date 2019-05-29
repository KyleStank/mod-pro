namespace StankUtilities.Runtime.ScriptableObjects.Variables
{
    /// <summary>
    /// Interface that allows us to define any type as a numerical variable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INumericalVariable<T>
    {
        /// <summary>
        /// Increments the runtime value by a specified amount.
        /// </summary>
        /// <param name="amount">Amount to increment.</param>
        void IncrementValue(T amount);

        /// <summary>
        /// Decrements the runtime value by a specified amount.
        /// </summary>
        /// <param name="amount">Amount to decrement.</param>
        void DecrementValue(T amount);
    }
}
