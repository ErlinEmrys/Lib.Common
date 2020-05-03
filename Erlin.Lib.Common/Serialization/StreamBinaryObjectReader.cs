using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Binary object reader from stream
    /// </summary>
    public class StreamBinaryObjectReader : BaseObjectReadWriter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="toRead">Stream to read</param>
        public StreamBinaryObjectReader(Stream? toRead)
        {
            if (toRead != null)
            {
                SetStream(toRead);
            }
        }

        /// <summary>
        /// Set stream to read
        /// </summary>
        /// <param name="toRead">Stream to read</param>
        protected void SetStream(Stream toRead)
        {
            //Type table
            IReader typeTableReader = new BinaryReaderProxy(toRead);
            ReadTypeTable(typeTableReader);

            Reader = new BinaryReaderProxy(toRead);
        }
    }
}