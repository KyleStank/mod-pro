using System.IO;

using UnityEngine;

using ModPro.Runtime.Core;

using StankUtilities.Runtime.Utilities;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.REPL;

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
        public static void LoadAssemblies()
        {
            // TODO: Remove all of this once not needed.
            //// Register any type that don't get registered above.
            //UserData.RegisterType<Vector3>();
            //UserData.RegisterType<Transform>();
            //UserData.RegisterType<GameObject>();
            //UserData.RegisterType<ResourceUtility>();

            //// Register static types.
            //script.Globals["Vector3"] = UserData.CreateStatic<Vector3>();
            //script.Globals["Transform"] = UserData.CreateStatic<Transform>();
            //script.Globals["GameObject"] = UserData.CreateStatic<GameObject>();
            //script.Globals["ResourceUtility"] = UserData.CreateStatic<ResourceUtility>();
        }

        /// <summary>
        /// Executes Lua code.
        /// </summary>
        /// <param name="luaCode">Lua code to execute.</param>
        /// <param name="api">API to pass into Lua script.</param>
        /// <param name="modulePaths">Module paths to pass into the script loader.</param>
        public static void ExecuteLuaCode(string luaCode, LuaAPIBase api, string[] modulePaths = null)
        {
            // Create a new script.
            Script script = new Script(CoreModules.Preset_Complete);

            // Set the script loader.
            script.Options.ScriptLoader = new ReplInterpreterScriptLoader();

            // Set the module paths, if possible.
            if(modulePaths != null)
            {
                // Loop through all module paths.
                for(int i = 0; i < modulePaths.Length; i++)
                {
                    // If the current module path doesn't exist, do not continue.
                    if(!Directory.Exists(modulePaths[i]))
                    {
                        DebuggerUtility.LogWarning("Path doesn't exist! Skipping this module path...");
                        continue;
                    }

                    // Get the last character of the path.
                    char lastCharacter = modulePaths[i][modulePaths.Length - 1];

                    // If the last character is not a forward slash, add one.
                    if(lastCharacter != '/')
                    {
                        modulePaths[i] += '/';
                    }

                    // Add MoonSharp's "?" identifier.
                    modulePaths[i] += "?.lua";
                }
                
                // Set the script loader's module paths.
                ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = modulePaths;
            }

            // Set the registration policy to automatic.
            UserData.RegistrationPolicy = InteropRegistrationPolicy.Automatic;

            // Register the inital types.
            UserData.RegisterAssembly();

            // Add the API into the Lua script.
            script.Globals["game"] = api;

            // Execute the Lua script!
            script.DoString(luaCode);
        }

        /// <summary>
        /// Executes a Lua script from a file.
        /// </summary>
        /// <param name="path">Lua script to execute.</param>
        /// <param name="api">API to pass into Lua script.</param>
        /// <param name="modulePaths">Module paths to pass into the script loader.</param>
        public static void ExecuteLuaScript(string path, LuaAPIBase api, string[] modulePaths = null)
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
            ExecuteLuaCode(File.ReadAllText(path), api, modulePaths);
        }

        /// <summary>
        /// Executes a Lua script that is inside a ZIP archive.
        /// </summary>
        /// <param name="path">Path to the ZIP archive.</param>
        /// <param name="scriptFile">Path to the Lua script inside of the ZIP archive.</param>
        /// <param name="api">API to pass into Lua script.</param>
        /// <param name="modulePaths">Module paths to pass into the script loader.</param>
        public static void ExecuteLuaScript(string path, string scriptFile, LuaAPIBase api, string[] modulePaths = null)
        {
            // Open the file.
            IOUtility.OpenZIPArchive(path, (file, zip, entry, stream) =>
            {
                // Check if the current file in the ZIP archive matches the Lua script we want to execute.
                if(entry.Name == scriptFile)
                {
                    StreamReader reader = new StreamReader(stream);

                    // Execute code!
                    ExecuteLuaCode(reader.ReadToEnd(), api, modulePaths);
                }
            });
        }
    }
}
