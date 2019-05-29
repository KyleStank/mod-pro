using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that adds the ability to easily create different assets in code.
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// Create a one colorde texture with a specified width and height.
        /// </summary>
        /// <param name="width">Width of the texture.</param>
        /// <param name="height">Height of the texture.</param>
        /// <param name="color">Color of the texture.</param>
        /// <returns>Returns a Texture2D with a width, height, and single color.</returns>
        public static Texture2D MakeTexture(int width, int height, Color color)
        {
            // Create an array of pixel colors for the texture.
            Color[] pixelColors = new Color[width * height];

            // Loop through all of the pixel colors.
            for(int i = 0; i < pixelColors.Length; i++)
            {
                // Set the color of the current pixel.
                pixelColors[i] = color;
            }

            // Create the texture.
            Texture2D texture = new Texture2D(width, height);

            // Set the color of all of the pixels with our color array from above.
            texture.SetPixels(pixelColors);

            // Apply all of the changes to the texture.
            texture.Apply();

            return texture;
        }
    }
}
