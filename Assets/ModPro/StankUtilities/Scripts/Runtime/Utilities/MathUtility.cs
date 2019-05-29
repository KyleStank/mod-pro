namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that makes performing math operations easier.
    /// </summary>
    public static class MathUtility
    {
        #region Public Methods

        /// <summary>
        /// Generates a random GUID.
        /// </summary>
        /// <returns>Returns a string that contains the new random GUID.</returns>
        public static string GenerateGUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Exponentially grows a number.
        /// </summary>
        /// <param name="initialValue">The initial value to grow.</param>
        /// <param name="rate">The rate to grow by.</param>
        /// <param name="power">The power to multiply everything by.</param>
        /// <returns>Returns a float that has exponentially grown.</returns>
        public static float ExponentiallyGrow(float initialValue, float rate, float power)
        {
            return initialValue * UnityEngine.Mathf.Pow(1.0f + rate, power);
        }

        /// <summary>
        /// Exponentially decays a number.
        /// </summary>
        /// <param name="initialValue">The initial value to decay.</param>
        /// <param name="rate">The rate to decay by.</param>
        /// <param name="power">The power to multiply everything by.</param>
        /// <returns>Returns a float that has exponentially decayed.</returns>
        public static float ExponentiallyDecay(float initialValue, float rate, float power)
        {
            return initialValue * UnityEngine.Mathf.Pow(UnityEngine.Mathf.Clamp(rate, 0.0f, 1.0f - rate), power);
        }

        #endregion
    }
}
