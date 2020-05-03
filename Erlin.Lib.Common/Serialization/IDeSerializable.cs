using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Interface of any object that can be serialized and deserialized
    /// </summary>
    public interface IDeSerializable
    {
        /// <summary>
        /// Dummy string for DeSerialization constructor
        /// </summary>
        const string DUMMY_STRING = "DeSerialization dummy string";

        /// <summary>
        /// (De)Serialize this object
        /// </summary>
        /// <param name="rw">Reader/writer</param>
        void DeSerialize(IObjectReadWriter rw);
    }
}