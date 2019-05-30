using System;

using UnityEngine;

using StankUtilities.Runtime.Utilities;

using MoonSharp.Interpreter;

namespace ModPro.Runtime.Core
{
    /// <summary>
    /// Class that adds an API to any modders.
    /// </summary>
    [MoonSharpUserData]
    public class ModAPI
    {
        /// <summary>
        /// Logs a message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void Log(object msg)
        {
            DebuggerUtility.Log(msg);
        }

        /// <summary>
        /// Logs a warning message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void LogWarning(object msg)
        {
            DebuggerUtility.LogWarning(msg);
        }

        /// <summary>
        /// Logs an error message to the Unity console.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        public void LogError(object msg)
        {
            DebuggerUtility.LogError(msg);
        }

        /// <summary>
        /// Adds a component to a GameObject in the scene.
        /// </summary>
        /// <param name="gameObject">GameObject to add component to.</param>
        /// <param name="component">Component to add to GameObject.</param>
        public void AddComponent(DynValue gameObject, DynValue component)
        {
            // Convert DynValue into GameObject, and then add the component after converting it into its Type.
            gameObject.ToObject<GameObject>().AddComponent(component.ToObject() as Type);
        }
        
        public Entity GetPlayerEntity()
        {
            return GameObject.Find("Player").GetComponent<Entity>();
        }

        //public Sprite LoadSpriteFromFile(string file, float pixelsPerUnit)
        //{
        //    return ResourceUtility.LoadNewSprite(file, pixelsPerUnit);
        //}

        public Sprite LoadNewSprite(string path)
        {
            return ResourceUtility.LoadNewSprite(path);
        }

        public string GetModDirectory()
        {
            // TODO: Add back GameDataPath
            //return Settings.GameDataPath + "Mods/";
            return "";
        }
    }
}
