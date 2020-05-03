using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Interface of any object that can write to or read from simple non-object stream
    /// </summary>
    public interface ISimpleDeSerializable
    {
        /// <summary>
        /// (De)Serialize this object
        /// </summary>
        /// <param name="rw">Reader/writer</param>
        void DeSerialize(ISimpleReadWriter rw);
    }
}