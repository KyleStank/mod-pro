using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using StankUtilities.Runtime.Utilities;

namespace StankUtilities.Editor.Utilities
{
    /// <summary>
    /// Useful utility class that makes extending the Unity Editor easier.
    /// </summary>
    public static class EditorUtilities
    {
        #region Properties

        /// <summary>
        /// Returns the skin color. Adjusts color based on if the user has the Pro Unity skin or not.
        /// </summary>
        public static Color SkinColor
        {
            get
            {
                return EditorGUIUtility.isProSkin ? new Color(0.0f, 0.0f, 0.0f, 0.1f) : new Color(1.0f, 1.0f, 1.0f, 0.3f);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a texture that can be used as a background color for any custom editor field, rect, etc.
        /// </summary>
        /// <returns>Returns a Texture2D that can be used as a background color for any custom editor extension.</returns>
        public static Texture2D MakeBackgroundTexture()
        {
           return AssetUtility.MakeTexture(Mathf.RoundToInt(EditorGUIUtility.currentViewWidth), 1, SkinColor);
        }

        /// <summary>
        /// Searches the entire project for assets that match a type.
        /// </summary>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <returns>Returns a List of type T.</returns>
        public static List<T> FindAssetsByType<T>() where T : Object
        {
            // Create a list for the assets.
            List<T> assets = new List<T>();

            // Search the project for the type and put it into an array of guids.
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));

            // Loop through the array of guids.
            for(int i = 0; i < guids.Length; i++)
            {
                // Get the asset path of the current guid.
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);

                // Create an asset of type T.
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if(asset != null)
                {
                    // Add the asset to the assets list.
                    assets.Add(asset);
                }
            }

            return assets;
        }

        /// <summary>
        /// Searches the project for all prefabs with an attached component.
        /// </summary>
        /// <typeparam name="T">Component that must be attached to the prefabs.</typeparam>
        /// <returns>Returns a Type T prefab.</returns>
        public static List<T> FindPrefabsByType<T>() where T : Object
        {
            // Create a list for the prefabs.
            List<T> prefabs = new List<T>();

            // Search the project for all prefabs.
            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            // Loop through the array of guids.
            for(int i = 0; i < guids.Length; i++)
            {
                // Get the asset path of the current guid.
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);

                // Get all assets at the path.
                Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);

                // Loop through assets.
                for(int k = 0; k < assets.Length; k++)
                {
                    // Convert asset to GameObject.
                    GameObject obj = assets[k] as GameObject;
                    if(obj == null)
                    {
                        continue;
                    }

                    // Try to get type T as a component.
                    T prefab = obj.GetComponent<T>();
                    if(prefab == null)
                    {
                        continue;
                    }

                    // Add this prefab to the list of prefabs.
                    prefabs.Add(prefab);
                }
            }

            return prefabs;
        }

        #endregion
    }
}
