using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.FileSystem
{
    /// <summary>
    /// Helper for file system
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Ensure that dirctory exist, if not, create it
        /// </summary>
        /// <param name="path">Directory path to ensure</param>
        /// <returns>Ensured directory path</returns>
        public static string DirectoryEnsure(string path)
        {
            if (!CheckIfPathIsDirectory(path))
            {
                path = GetDirectoryPath(path);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Returns true if path is pointing only to directory, False if its file
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>True - is directory only</returns>
        public static bool CheckIfPathIsDirectory(string? path)
        {
            return string.IsNullOrEmpty(Path.GetExtension(path));
        }

        /// <summary>
        /// Returns directory path from file path
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>Directory path</returns>
        public static string GetDirectoryPath(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Directory?.FullName ?? throw new InvalidOperationException($"Could not get directory from file path: {filePath}");
        }

        /// <summary>
        /// Write all text to single file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="contents">Content to be written</param>
        public static void WriteAllText(string path, string contents)
        {
            WriteAllText(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// Write all text to single file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="contents">Content to be written</param>
        /// <param name="encoding">Encoding of file</param>
        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            DirectoryEnsure(path);
            File.WriteAllText(path, contents, encoding);
        }

        /// <summary>
        /// Write all binry data to single file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="content">Content to be written</param>
        public static void WriteAllBytes(string path, byte[] content)
        {
            DirectoryEnsure(path);
            File.WriteAllBytes(path, content);
        }

        /// <summary>
        /// Deletes the specified directory if exist (with all under)
        /// </summary>
        /// <param name="directoryPath">Path to directory</param>
        public static void DirectoryDelete(string directoryPath)
        {
            DirectoryInfo info = new DirectoryInfo(directoryPath);
            if (info.Exists)
            {
                info.Delete(true);
                info.Refresh();
                while (info.Exists)
                {
                    Thread.Sleep(1);
                    info.Refresh();
                }
            }
        }
    }
}