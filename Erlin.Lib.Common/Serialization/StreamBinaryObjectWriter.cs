using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Binary object writer to stream
    /// </summary>
    public class StreamBinaryObjectWriter : BaseObjectReadWriter
    {
        /// <summary>
        /// In-memory stream
        /// </summary>
        protected MemoryStream InnerStream { get; set; }

        /// <summary>
        /// Stream for writing
        /// </summary>
        protected Stream? ToWriteStream { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        protected StreamBinaryObjectWriter()
        {
            InnerStream = new MemoryStream();
            Writer = new BinaryWriterProxy(InnerStream);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="toWrite">Stream to write into</param>
        public StreamBinaryObjectWriter(Stream toWrite) : this()
        {
            ToWriteStream = toWrite;
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (ToWriteStream != null)
            {
                //Type table
                IWriter typeTableWriter = new BinaryWriterProxy(ToWriteStream);
                WriteTypeTable(typeTableWriter);

                //Data
                byte[] arr = InnerStream.ToArray();
                ToWriteStream.Write(arr, 0, arr.Length);
            }

            InnerStream?.Dispose();
        }
    }
}