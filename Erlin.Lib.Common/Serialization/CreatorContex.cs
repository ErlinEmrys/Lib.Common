using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Kontext of object creation in De/Serialize
    /// </summary>
    public class CreatorContex<T>
        where T : class, IDeSerializable
    {
        /// <summary>
        /// De/Serialization object type name
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Instance of object
        /// </summary>
        public T? Instance { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="typeName">De/Serialization object type name</param>
        public CreatorContex(string typeName)
        {
            TypeName = typeName;
        }
    }
}