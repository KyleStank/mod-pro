using System.IO;
using System.IO.Compression;

using UnityEngine;
using UnityEditor;

using ModPro.Runtime.Modding;

using Newtonsoft.Json;

namespace ModPro.Editor
{
    public class ModProWindow : EditorWindow
    {
        // Add menu item named "My Window" to the Window menu
        [MenuItem("Tools/ModPro")]
        public static void ShowWindow()
        {
            if(PlayerPrefs.HasKey("GameDataDirectory"))
            {
                // TODO: Add back GameDataPath
                //Settings.GameDataPath = PlayerPrefs.GetString("GameDataDirectory");
            }

            // Show existing window instance. If one doesn't exist, make one.
            GetWindow<ModProWindow>("ModPro Dashboard");
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();



            EditorGUILayout.EndVertical();

            // TODO: Add back base settings for Window.
            //EditorGUILayout.BeginHorizontal();

            //// Display game data path.
            //EditorGUILayout.LabelField(new GUIContent("Game Data Path: " + Settings.GameDataPath));

            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();

            //// Display "Choose Folder" button.
            //if(GUILayout.Button(new GUIContent("Choose Folder")))
            //{
            //    string path = EditorUtility.OpenFolderPanel("Choose Game Data Folder", Settings.GameDataPath, "") + "/";
            //    Settings.GameDataPath = path;
            //    PlayerPrefs.SetString("GameDataDirectory", Settings.GameDataPath);
            //}

            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginVertical();

            //// Display "Setup Required Directories" button.
            //if(GUILayout.Button(new GUIContent("Setup Required Directories")))
            //{
            //    if(!Directory.Exists(Settings.GameDataPath))
            //    {
            //        Directory.CreateDirectory(Settings.GameDataPath);
            //    }

            //    if(!Directory.Exists(Settings.GameDataPath + "Mods"))
            //    {
            //        Directory.CreateDirectory(Settings.GameDataPath + "Mods");
            //    }

            //    Settings.LoadSettings();
            //    Settings.SaveSettings();

            //    AssetDatabase.Refresh();
            //}

            //// Update Mods.
            //if(GUILayout.Button(new GUIContent("Update Mods")))
            //{
            //    Settings.Data.Mods.Clear();
            //    Settings.FindMods();
            //    Settings.SaveSettings();
            //    AssetDatabase.Refresh();
            //}

            //EditorGUILayout.EndVertical();
        }
    }
}
