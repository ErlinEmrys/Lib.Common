using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.FileSystem;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// File binary writer
    /// </summary>
    public class FileBinaryWriter : StreamBinaryObjectWriter
    {
        /// <summary>
        /// Path to writed file
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Should compress write output
        /// </summary>
        public bool Compress { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="filePath">File path to write</param>
        /// <param name="compress">Whether compress output file</param>
        public FileBinaryWriter(string filePath, bool compress = false)
        {
            FilePath = filePath;
            Compress = compress;
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public override void Dispose()
        {
            using (FileStream fileStream = OpenFileStream())
            {
                GZipStream? zipStream = null;
                try
                {
                    Stream toWriteStream = fileStream;
                    if (Compress)
                    {
                        zipStream = new GZipStream(fileStream, CompressionLevel.Optimal, true);
                        toWriteStream = zipStream;
                    }

                    ToWriteStream = toWriteStream;
                    base.Dispose();
                }
                finally
                {
                    zipStream?.Dispose();
                }
            }
        }

        /// <summary>
        /// Open File stream to writed file
        /// </summary>
        /// <returns>Opened file stream</returns>
        private FileStream OpenFileStream()
        {
            FileHelper.DirectoryEnsure(FilePath);
            return File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.None);
        }
    }
}