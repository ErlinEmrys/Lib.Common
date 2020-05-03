using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// File binary reader
    /// </summary>
    public class FileBinaryReader : StreamBinaryObjectReader
    {
        private readonly FileStream _fileStream;
        private readonly GZipStream? _zipStream;

        /// <summary>
        /// Path to readed file
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Is file compressed
        /// </summary>
        public bool Decompress { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="filePath">File path to read</param>
        /// <param name="decompress">File is compressed</param>
        public FileBinaryReader(string filePath, bool decompress = false) : base(null)
        {
            FilePath = filePath;
            Decompress = decompress;

            Stream stream = _fileStream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (Decompress)
            {
                stream = _zipStream = new GZipStream(_fileStream, CompressionMode.Decompress, false);
            }

            SetStream(stream);
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            _zipStream?.Dispose();
            _fileStream.Dispose();
        }
    }
}