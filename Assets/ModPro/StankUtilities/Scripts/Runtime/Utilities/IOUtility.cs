using System.IO;
using System.IO.Compression;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that handles IO (input & output) operations.
    /// </summary>
    public static class IOUtility
    {
        /// <summary>
        /// Checks to see if a file ends with a specific file extension.
        /// </summary>
        /// <param name="file">File to check.</param>
        /// <param name="extension">File extension to check for.</param>
        /// <returns>Returns a bool that is true if the file ends with the provided extension. Returns false otherwise.</returns>
        public static bool IsFileExtension(string file, string extension)
        {
            // Check to make sure the file exists.
            if(!File.Exists(file))
            {
                //LogUtility.LogError("Couldn't check to see if the file was a " + extension + " file because the provided file doesn't exist!");
                return false;
            }
            
            // Check if the file's extension matches the one being looked for.
            return Path.GetExtension(file).ToLower() == extension.ToLower();
        }

        /// <summary>
        /// Checks to see if a file is a ZIP archive.
        /// </summary>
        /// <param name="file">File to check.</param>
        /// <returns>Returns a bool that is true if the file is a ZIP archive. Returns false otherwise.</returns>
        public static bool IsZipFile(string file)
        {
            return IsFileExtension(file, ".zip");
        }

        /// <summary>
        /// Loads an array of bytes from a file.
        /// </summary>
        /// <param name="filePath">Path to the file to read.</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] ReadAllBytes(string filePath)
        {
            // Check to make sure the file exists.
            if(!File.Exists(filePath))
            {
                DebuggerUtility.LogError("Couldn't create a Texture from an image because the provided file doesn't exist!");
                return null;
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Loads an array of bytes from a Stream of data.
        /// </summary>
        /// <param name="stream">Stream of data.</param>
        /// <returns>Returns an array of bytes.</returns>
        public static byte[] ReadAllBytes(Stream stream)
        {
            if(stream == null)
            {
                DebuggerUtility.LogError("Couldn't convert Stream to an array of byte[]! The provided Steam was null!");
                return null;
            }

            byte[] bytes = null;

            // Create a memory stream.
            using(MemoryStream memoryStream = new MemoryStream())
            {
                // Copy the stream to the memory stream.
                stream.CopyTo(memoryStream);

                // Convert the memory stream to an array of bytes.
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        /// <summary>
        /// Opens a ZIP Archive and provides a callback to do whatever is needed while the file is open.
        /// </summary>
        /// <param name="path">Path to the ZIP Archive.</param>
        /// <param name="callback">Callback to execute while the ZIP file is open.</param>
        public static void OpenZIPArchive(string path, System.Action<FileStream, ZipArchive, ZipArchiveEntry, Stream> callback)
        {
            // Check to make sure the ZIP archive exists.
            if(!File.Exists(path))
            {
                DebuggerUtility.LogError("Couldn't create a Texture from a ZIP archive because the provided ZIP file doesn't exist!");
                return;
            }

            // Check to make sure the file is a ZIP archive.
            if(!IsZipFile(path))
            {
                DebuggerUtility.LogError("Couldn't create a Texture from a ZIP archive because the provided file is not a ZIP archive!");
                return;
            }

            // Open the file.
            using(FileStream file = File.OpenRead(path))
            {
                // Open the ZIP archive.
                using(ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
                {
                    // Loop through every file in the ZIP archive.
                    foreach(ZipArchiveEntry entry in zip.Entries)
                    {
                        // Create a Stream for the current file in the ZIP archive.
                        using(Stream stream = entry.Open())
                        {
                            // Execute the callback!
                            callback(file, zip, entry, stream);
                        }
                    }
                }
            }
        }
    }
}
