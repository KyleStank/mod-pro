using UnityEngine;
using UnityEditor;

// TODO: Remove ALL code from ModPro.
using ModPro.Runtime.Game;

using StankUtilities.Runtime.Utilities;

namespace ModPro.Editor
{
    /// <summary>
    /// Creates a custom inspector view for the EntityData class in the Unity Editor.
    /// </summary>
    [CustomEditor(typeof(EntityData))]
    public class EntityDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EntityData entityData = (EntityData)target;

            // Entity Name.
            EditorGUI.BeginChangeCheck();
            string entityName = EditorGUILayout.TextField(new GUIContent("Name: "), entityData.EntityName);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(entityData, "Changed Entity Data's Name");
                entityData.EntityName = entityName;
            }

            // Entity ID.
            EditorGUILayout.LabelField(new GUIContent("ID: " + entityData.ID));

            // Generate New ID.
            EditorGUI.BeginChangeCheck();
            if(GUILayout.Button("Generate New ID"))
            {
                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(entityData, "Changed Entity Data's ID");
                    entityData.ID = MathUtility.GenerateGUID();
                }
            }

            // Save Data.
            if(GUILayout.Button("Save"))
            {
                // TODO: Add back saving.
                //Settings.Data.Entities.Add(new Runtime.Data.EntityContainer(entityData));
                //Settings.SaveSettings();

                AssetDatabase.Refresh();
            }
        }
    }
}
