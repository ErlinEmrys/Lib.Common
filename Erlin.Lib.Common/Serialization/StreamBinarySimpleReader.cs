using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Binary simple reader from stream
    /// </summary>
    public class StreamBinarySimpleReader : BaseSimpleReadWriter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="toRead">Stream to read</param>
        public StreamBinarySimpleReader(Stream toRead)
        {
            Reader = new BinaryReaderProxy(toRead);
        }
    }
}