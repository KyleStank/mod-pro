#if EDITOR

using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using ModPro.Runtime.Core;

namespace ModPro.Editor.Window
{
    /// <summary>
    /// Created a window in the Unity Editor that manages all of the base/default Entities in the game.
    /// </summary>
    public class EntityDataWindow : EditorWindow
    {
        private static List<EntityData> modEntities = null;

        #region Unity Methods
        
        private void OnGUI()
        {
            // If the ModSettings object is null, do not proceed.
            if(ModProManager.TheModSettings == null)
            {
                GUILayout.Label("The settings file for ModPro have not been created.");
                GUILayout.Label("Try opening the dashboard and settings up the default file locations for everything.");
                return;
            }

            // Create label for "Entities"
            GUILayout.Label("Entities", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();

            // Begin Entity Creation Buttons.
            EditorGUILayout.BeginHorizontal();

            // Display "+" button.
            if(GUILayout.Button(new GUIContent("New")))
            {
                ModProManager.TheModSettings.Entities.Add(new EntityData("New Entity"));
            }

            EditorGUILayout.EndHorizontal();

            // Begin Entity Collection Display.
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();

            // Display all Entities.
            List<EntityData> entitiesToRemove = new List<EntityData>();
            foreach(EntityData entity in modEntities)
            {
                EditorGUILayout.BeginHorizontal();

                // Entity Name
                EditorGUI.BeginChangeCheck();
                string entityName = EditorGUILayout.TextField(new GUIContent("Entity Name:"), entity.Name);
                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(this, "Changed Entity Name");
                    entity.Name = entityName;
                }

                // ID
                EditorGUILayout.LabelField(new GUIContent("ID: " + entity.ID));

                // Remove Button
                if(GUILayout.Button(new GUIContent("Remove")))
                {
                    entitiesToRemove.Add(entity);
                }

                EditorGUILayout.EndHorizontal();
            }

            // Remove any entities that are marked to be removed.
            foreach(EntityData entity in entitiesToRemove)
            {
                modEntities.Remove(entity);
            }

            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;

            // Display "Save" button
            if(GUILayout.Button(new GUIContent("Save")))
            {
                // Save.
                ModProManager.TheModSettings.Entities = modEntities;
                ModProManager.TheModSettings.Save();
                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Display the window!
        /// </summary>
        [MenuItem(itemName: "Tools/ModPro/Entity Manager", priority = 2)]
        public static void ShowWindow()
        {
            // Create instance of ModSettings.
            ModProManager.InitializeModSettings();

            // Setup required variables.
            modEntities = ModProManager.TheModSettings.Entities;

            // Show existing window instance. If one doesn't exist, make one.
            GetWindow<EntityDataWindow>("Entity Manager");
        }

        #endregion
    }
}

#endif
