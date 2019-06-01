#if EDITOR

using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using ModPro.Runtime.Core;
using ModPro.Runtime.Data;

namespace ModPro.Editor
{
    /// <summary>
    /// Creates a custom inspector for the Entity component.
    /// </summary>
    [CustomEditor(typeof(Entity))]
    public class EntityEditor : UnityEditor.Editor
    {
        private List<EntityData> m_ModEntities = null;

        private Entity m_Entity = null;

        private string[] m_EntityDataDropdownOptions = null;

        #region Unity Methods

        private void OnEnable()
        {
            // Get the target and assign it a variable for re-use.
            m_Entity = target as Entity;

            ModProManager.InitializeModSettings();
            m_ModEntities = ModProManager.TheModSettings.Entities;

            // Assign a default entity if needed.
            if(m_Entity.Data == null && m_ModEntities.Count > 0)
            {
                m_Entity.Data = m_ModEntities[0];
            }

            // Set dropdown display options.
            m_EntityDataDropdownOptions = new string[m_ModEntities.Count];
            for(int i = 0; i < m_ModEntities.Count; i++)
            {
                m_EntityDataDropdownOptions[i] = m_ModEntities[i].Name;
            }
        }

        /// <summary>
        /// Invoked when the Inspector GUI's is drawn in the Unity Editor.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Get the target and assign it a variable for re-use.
            m_Entity = target as Entity;

            EditorGUILayout.BeginVertical();

            // Entity Settings.
            EditorGUILayout.LabelField(new GUIContent("Entity Settings"), EditorStyles.boldLabel);

            // Display dropdown for all entities.
            if(m_ModEntities.Count > 0)
            {
                EditorGUI.BeginChangeCheck();
                int entityIndex = EditorGUILayout.Popup(
                    new GUIContent("Entity Data:", "EntityData that is assigned to this entity."),
                    m_ModEntities.FindIndex(e => e.ID == m_Entity.Data.ID),
                    m_EntityDataDropdownOptions);
                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(m_Entity, "Changed Entity's Entity Data");
                    m_Entity.Data = m_ModEntities[entityIndex];
                }
            }

            EditorGUILayout.EndVertical();

            // Mark object as dirty so all changes persist.
            if(GUI.changed)
            {
                EditorUtility.SetDirty(m_Entity);
            }
        }

        #endregion
    }
}

#endif
