using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that contains various different extensions to Unity's Gizmo system.
    /// </summary>
    public static class GizmosUtilities
    {
        #region Public Methods

        /// <summary>
        /// Draws a 2D circle in 3D space around a transform.
        /// </summary>
        /// <param name="transform">Transform to draw the circle around.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="color">Color of the circle.</param>
        /// <param name="yOffset">Optional parameter that adjusts the y offset. Don't change this unless you know what it does.</param>
        public static void DrawCircle(Transform transform, float radius, Color color, float yOffset = 0.01f)
        {
            // Set the color.
            Gizmos.color = color;

            // Set the length of each line segment.
            float segmentLength = 0.05f;

            // Set the current position to this game object's origin plus to the radius.
            Vector3 pos = transform.position + new Vector3(radius, yOffset, radius);

            // Create variables to store the new position and the last position of each line segment.
            Vector3 newPos = pos;
            Vector3 lastPos = pos;

            // Loop through each line segment until we go a full circle.
            for(float i = 0.0f; i < Mathf.PI * 2.0f; i += segmentLength)
            {
                // Calculate the X and Y position.
                float x = radius * Mathf.Cos(i);
                float y = radius * Mathf.Sin(i);

                // Set the new position of the segment.
                newPos = transform.position + new Vector3(x, yOffset, y);

                // Draw the segment.
                Gizmos.DrawLine(pos, newPos);

                // Set the current position as the new position.
                pos = newPos;
            }

            // Finish the line from the last segment to the original segment.
            Gizmos.DrawLine(pos, lastPos);
        }

        #endregion
    }
}
