#if EDITOR

using UnityEngine;
using UnityEditor;

namespace ModPro.Editor
{
    /// <summary>
    /// Custom Unity Editor Window that is responsible for managing ModPro.
    /// </summary>
    public class ModProWindow : EditorWindow
    {
        #region Unity Methods

        private void OnGUI()
        {
            // Create label for "Dashboard"
            GUILayout.Label("Dashboard", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            // Display "Open Entity Manager" button.
            if(GUILayout.Button(new GUIContent("Open Entity Manager")))
            {
                // Open the Entity Manager window.
                EntityDataWindow.ShowWindow();
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Display the main window!
        /// </summary>
        [MenuItem(itemName: "Tools/ModPro/Dashboard", priority = 1)]
        public static void ShowWindow()
        {
            // Show existing window instance. If one doesn't exist, make one.
            GetWindow<ModProWindow>("ModPro Dashboard");
        }

        #endregion
    }
}

#endif
