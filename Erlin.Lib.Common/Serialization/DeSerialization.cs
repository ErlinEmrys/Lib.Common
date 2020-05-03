using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// DeSerialization util
    /// </summary>
    public static class DeSerialization
    {
        /// <summary>
        /// Clone object by serialize/deserialize
        /// </summary>
        /// <typeparam name="T">Runtime type of cloned object</typeparam>
        /// <param name="item">Object to clone</param>
        /// <returns>Cloned object</returns>
        public static T? Clone<T>(T? item)
            where T : class, IDeSerializable, new()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamBinaryObjectWriter writer = new StreamBinaryObjectWriter(stream);
                writer.ReadWriteN("item", item);
                StreamBinaryObjectReader reader = new StreamBinaryObjectReader(stream);
                return reader.ReadWriteN<T>("item", null);
            }
        }
    }
}