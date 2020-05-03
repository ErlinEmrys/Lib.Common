using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Binary simple writer to stream
    /// </summary>
    public class StreamBinarySimpleWriter : BaseObjectReadWriter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="toWrite">Stream to write into</param>
        public StreamBinarySimpleWriter(Stream toWrite)
        {
            Writer = new BinaryWriterProxy(toWrite);
        }
    }
}