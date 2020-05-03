using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Exceptions
{
    /// <summary>
    /// Exception when run to not implemented enum vale branch
    /// </summary>
    [Serializable]
    public class EnumValueNotImplementedException : Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="value">Value of the enum</param>
        public EnumValueNotImplementedException(Enum value) : base(CreateMessage(value))
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="value">Value of the enum</param>
        /// <param name="innerException">Original thrown exception</param>
        public EnumValueNotImplementedException(Enum value, Exception? innerException) : base(CreateMessage(value), innerException)
        {
        }

        /// <summary>
        /// Serialization ctor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected EnumValueNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Create error message
        /// </summary>
        /// <param name="value">Value of the enum</param>
        /// <returns>Error message</returns>
        private static string CreateMessage(Enum value)
        {
            return $"Enum branch not implemented for value: {value}";
        }
    }
}