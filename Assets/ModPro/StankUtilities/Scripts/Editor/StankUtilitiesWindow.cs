using UnityEngine;
using UnityEditor;

using StankUtilities.Runtime.Utilities;

namespace StankUtilities.Editor
{
    public class StankUtilitiesWindow : EditorWindow
    {
        // Add menu item named "StankUtilities" to the Window menu
        [MenuItem("Tools/StankUtilities")]
        public static void ShowWindow()
        {
            // Show existing window instance. If one doesn't exist, make one.
            GetWindow<StankUtilitiesWindow>("StankUtilities Dashboard");
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();

            // Debugger Prefix.
            EditorGUI.BeginChangeCheck();
            string debuggerPrefix = EditorGUILayout.TextField(new GUIContent("Logger Prefix:"), DebuggerUtility.LogPrefix);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(this, "Changed Logger Prefix");
                DebuggerUtility.LogPrefix = debuggerPrefix;
            }

            // Debugger Color.
            EditorGUI.BeginChangeCheck();
            Color debuggerColor = EditorGUILayout.ColorField(new GUIContent("Logger Prefix Color:"), DebuggerUtility.LogColor);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(this, "Changed Logger Prefix Color");
                DebuggerUtility.LogColor = debuggerColor;
            }

            // Save Button.
            if(GUILayout.Button(new GUIContent("Save Settings")))
            {
                // Save settings!
                PlayerPrefs.Save();
            }

            EditorGUILayout.EndVertical();
        }
    }
}
