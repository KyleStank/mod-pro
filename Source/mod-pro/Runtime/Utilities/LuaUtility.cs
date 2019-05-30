using System.IO;

using UnityEngine;

using ModPro.Runtime.Modding;

using StankUtilities.Runtime.Utilities;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace ModPro.Runtime.Utilities
{
    /// <summary>
    /// Class that creates useful functions for creating custom converters for MoonSharp.
    /// </summary>
    public static class LuaUtility
    {
        /// <summary>
        /// Registers all required assemblies into a provided script.
        /// </summary>
        /// <param name="script">Script to load assemblies into.</param>
        public static void LoadAssemblies(Script script)
        {
            if(script == null)
            {
                DebuggerUtility.LogError("Unable to load required Lua assemblies because the script provided is null!");
                return;
            }

            // Set the registration policy to automatic.
            UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;

            // Register the inital types.
            UserData.RegisterAssembly();

            // Register any type that don't get registered above.
            UserData.RegisterType<Vector3>();
            UserData.RegisterType<Transform>();
            UserData.RegisterType<GameObject>();
            UserData.RegisterType<ResourceUtility>();

            // Register static types.
            script.Globals["Vector3"] = UserData.CreateStatic<Vector3>();
            script.Globals["Transform"] = UserData.CreateStatic<Transform>();
            script.Globals["GameObject"] = UserData.CreateStatic<GameObject>();
            script.Globals["ResourceUtility"] = UserData.CreateStatic<ResourceUtility>();

            // Register global functions.
            //script.Globals["AddComponent"] = (Action<DynValue, DynValue>)AddComponent;

            // Add the modding API into the script.
            // TODO: Add back global namespace.
            //script.Globals[Settings.Data.PublicNamespace] = new ModAPI();
            script.Globals["game"] = new ModAPI();
        }

        /// <summary>
        /// Executes Lua code.
        /// </summary>
        /// <param name="luaCode">Lua code to execute.</param>
        /// <param name="includeAPI">Should the modding API be loaded into the code?</param>
        public static void ExecuteLuaCode(string luaCode, bool includeAPI)
        {
            // Create a new script.
            Script script = new Script();

            // Possibly load the modding API into the script.
            if(includeAPI)
            {
                // Load Lua assembleis.
                LoadAssemblies(script);
            }

            // Execute the Lua script!
            script.DoString(luaCode);
            //script.DoString(@"
            //    file = game.GetModDirectory() .. 'crappymod/Calvin.png'

            //    entity = game.GetPlayerEntity()
            //    entity.SetNewSprite(0, game.LoadNewSprite(file))
            //");
        }

        /// <summary>
        /// Executes a Lua script from a file.
        /// </summary>
        /// <param name="path">Lua script to execute.</param>
        /// <param name="includeAPI">Should the modding API be loaded into the script?</param>
        public static void ExecuteLuaScript(string path, bool includeAPI = true)
        {
            // Check to make sure the file exists.
            if(!File.Exists(path))
            {
                DebuggerUtility.LogError("Couldn't execute the Lua script because the provided file doesn't exist!");
                return;
            }

            // Check to make sure the file is a Lua script.
            if(!IOUtility.IsFileExtension(path, ".lua"))
            {
                DebuggerUtility.LogError("Couldn't execute the Lua script because the file provided is not a Lua script!");
                return;
            }

            // Execute code!
            ExecuteLuaCode(File.ReadAllText(path), includeAPI);
        }

        /// <summary>
        /// Executes a Lua script that is inside a ZIP archive.
        /// </summary>
        /// <param name="path">Path to the ZIP archive.</param>
        /// <param name="scriptFile">Path to the Lua script inside of the ZIP archive.</param>
        /// <param name="includeAPI">Should the modding API be loaded into the script?</param>
        public static void ExecuteLuaScript(string path, string scriptFile, bool includeAPI = true)
        {
            // Open the file.
            IOUtility.OpenZIPArchive(path, (file, zip, entry, stream) =>
            {
                // Check if the current file in the ZIP archive matches the Lua script we want to execute.
                if(entry.Name == scriptFile)
                {
                    StreamReader reader = new StreamReader(stream);

                    // Execute code!
                    ExecuteLuaCode(reader.ReadToEnd(), includeAPI);
                }
            });
        }
    }
}
