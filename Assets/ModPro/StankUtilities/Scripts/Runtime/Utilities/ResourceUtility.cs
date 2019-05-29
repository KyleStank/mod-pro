using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that handles loading assets from outside of Unity.
    /// </summary>
    public class ResourceUtility
    {
        /// <summary>
        /// Loads a Sprite from an image file.
        /// </summary>
        /// <param name="filePath">Image file to load.</param>
        /// <param name="pixelsPerUnit">Pixels per unit.</param>
        /// <returns>Returns a Sprite that was loaded from an image file.</returns>
        public static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 100.0f)
        {
            // Load the image.
            Texture2D spriteTexture = LoadTexture(filePath);

            // Check to make sure the Texture was properly created.
            if(spriteTexture == null)
            {
                DebuggerUtility.LogError("Couldn't create a Sprite because the Texture was not properly loaded!");
                return null;
            }

            // Create the Sprite with the texture.
            return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit);
        }

        /// <summary>
        /// Loads a Sprite from a Texture.
        /// </summary>
        /// <param name="spriteTexture">Texture to create Sprite with.</param>
        /// <param name="pixelsPerUnit">Pixels per unit.</param>
        /// <returns>Returns a Sprite that was loaded from a Texture.</returns>
        public static Sprite LoadNewSprite(Texture2D spriteTexture, float pixelsPerUnit = 100.0f)
        {
            // Check to make sure the provided Texture exists.
            if(spriteTexture == null)
            {
                DebuggerUtility.LogError("Couldn't create a Sprite from a Texture because the provided Texture was null!");
                return null;
            }

            // Create the sprite with the texture.
            Sprite sprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit);

            return sprite;
        }

        /// <summary>
        /// Loads a Sprite from an image file inside of a ZIP archive.
        /// </summary>
        /// <param name="path">Path to the ZIP archive.</param>
        /// <param name="textureName">Path to the image inside of the ZIP archive.</param>
        /// <param name="pixelsPerUnit">Pixels per unit.</param>
        /// <returns>Returns a Sprite that was loaded from a ZIP archive.</returns>
        public static Sprite LoadNewSpriteFromZIP(string path, string textureName, float pixelsPerUnit = 100.0f)
        {
            // Load the image from the ZIP archive.
            Texture2D spriteTexture = LoadTextureFromZIP(path, textureName);

            // Check to make sure the Texture was properly created.
            if(spriteTexture == null)
            {
                DebuggerUtility.LogError("Couldn't create a Sprite from a ZIP archive because the Texture was not properly loaded!");
                return null;
            }

            // Create the sprite with the texture.
            return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit);
        }

        /// <summary>
        /// Loads an image from a file and converts it to a Texture.
        /// </summary>
        /// <param name="filePath">Path to the image file.</param>
        /// <returns>Returns a Texture2D that was loaded from a file.</returns>
        public static Texture2D LoadTexture(string filePath)
        {
            // Read all of the bytes in the file.
            byte[] bytes = IOUtility.ReadAllBytes(filePath);

            // Create an empty texture.
            Texture2D texture = new Texture2D(1, 1);

            // Make sure the image properly loads.
            if(!texture.LoadImage(bytes))
            {
                DebuggerUtility.LogError("Couldn't properly load the image into the Texture!");
                return null;
            }

            return texture;
        }

        /// <summary>
        /// Loads an image file from a ZIP archive and converts it to a Texture.
        /// </summary>
        /// <param name="path">Path to the ZIP archive.</param>
        /// <param name="textureName">Path to the image inside of the ZIP archive.</param>
        /// <returns>Returns a Texture2D that was loaded from a ZIP archive.</returns>
        public static Texture2D LoadTextureFromZIP(string path, string textureName)
        {
            Texture2D texture = null;

            // Open the ZIP Archive.
            IOUtility.OpenZIPArchive(path, (file, zip, entry, stream) =>
            {
                // Check if the current file in the ZIP archive matches the image we want to load.
                if(entry.Name == textureName)
                {
                    // Create an empty texture.
                    texture = new Texture2D(1, 1);

                    // Make sure the image properly loads.
                    if(!texture.LoadImage(IOUtility.ReadAllBytes(stream)))
                    {
                        DebuggerUtility.LogError("Couldn't properly load the image into the Texture from the ZIP archive!");
                        texture = null;
                        return;
                    }
                }
            });

            return texture;
        }
    }
}
