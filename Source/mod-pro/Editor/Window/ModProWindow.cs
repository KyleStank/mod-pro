#if EDITOR

using System.IO;

using UnityEngine;
using UnityEditor;

using ModPro.Runtime.Core;
using ModPro.Runtime.Data;

namespace ModPro.Editor.Window
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

            EditorGUILayout.BeginVertical();

            // If the settings file hasn't been selected, show a notice.
            GUILayout.Label("Settings File: " + ModProManager.SettingsPath);
            if(string.IsNullOrWhiteSpace(ModProManager.SettingsPath))
            {
                GUILayout.Label("No settings file is chosen! Create a new one.");
            }

            EditorGUILayout.BeginHorizontal();

            // Display "Create Settings File" button.
            if(GUILayout.Button(new GUIContent("Create Settings File")))
            {
                // Settings file.
                ModProManager.SettingsPath = EditorUtility.SaveFilePanel("Create Settings File", ModProManager.SettingsPath, "mod_settings", "json");
                SetupModPro();
            }

            // Display "Choose Settings File" button.
            if(GUILayout.Button(new GUIContent("Choose Settings File")))
            {
                // Settings file.
                ModProManager.SettingsPath = EditorUtility.OpenFilePanel("Choose Settings File", ModProManager.SettingsPath, "json");
                SetupModPro();
            }

            EditorGUILayout.EndHorizontal();

            // If the mods folder hasn't been selected, show a notice.
            GUILayout.Label("Mods Folder: " + ModProManager.ModsPath);
            if(string.IsNullOrWhiteSpace(ModProManager.ModsPath))
            {
                GUILayout.Label("No mods folder has been chosen!");
            }

            // Display "Choose Mods Folder" button.
            if(GUILayout.Button(new GUIContent("Choose Mods Folder")))
            {
                // Mods folder.
                ModProManager.ModsPath = EditorUtility.OpenFolderPanel("Choose Mods Folder", ModProManager.ModsPath, "");
            }

            // Display "Setup" button.
            if(!string.IsNullOrWhiteSpace(ModProManager.SettingsPath) && !string.IsNullOrWhiteSpace(ModProManager.ModsPath))
            {
                if(GUILayout.Button(new GUIContent("Setup")))
                {
                    SetupModPro();
                }
            }

            EditorGUILayout.EndVertical();

            // If the ModSettings object is null, do not proceed.
            if(ModProManager.TheModSettings == null || (string.IsNullOrWhiteSpace(ModProManager.SettingsPath) || string.IsNullOrWhiteSpace(ModProManager.ModsPath)))
            {
                return;
            }

            EditorGUILayout.BeginHorizontal();

            // Display "Open Entity Manager" button.
            if(GUILayout.Button(new GUIContent("Open Entity Manager")))
            {
                // Open the Entity Manager window.
                EntityDataWindow.ShowWindow();
            }

            // Display "Clear PlayerPrefs" button.
            if(GUILayout.Button(new GUIContent("Clear PlayerPrefs")))
            {
                PlayerPrefs.DeleteAll();
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
            // Setup ModPro.
            SetupModPro();

            // Show existing window instance. If one doesn't exist, make one.
            GetWindow<ModProWindow>("ModPro Dashboard");
        }

        /// <summary>
        /// Adds the ability to create a Lua script inside the Unity Editor
        /// </summary>
        [MenuItem("Assets/Create/ModPro/Lua Script", priority = 1)]
        public static void CreateLuaScript()
        {
            // Get current path in project.
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            // If no current path was found, set the default to be the Assets folder.
            if(string.IsNullOrWhiteSpace(path))
            {
                path = "Assets";
            }
            else if(!string.IsNullOrWhiteSpace(Path.GetExtension(path)))
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            // Initialize asset.
            TextAsset textAsset = new TextAsset("");

            // Create asset.
            ProjectWindowUtil.CreateAsset(textAsset, AssetDatabase.GenerateUniqueAssetPath(path + "/script.lua"));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up ModPro.
        /// </summary>
        private static void SetupModPro()
        {
            // Create instance of ModSettings.
            ModProManager.InitializeModSettings();

            // Refresh the AssetDatabase.
            AssetDatabase.Refresh();
        }

        #endregion
    }
}

#endif
