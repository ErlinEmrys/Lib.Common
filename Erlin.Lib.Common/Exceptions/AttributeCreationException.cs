using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Exceptions
{
    /// <summary>
    /// Exception during attribute ctor
    /// </summary>
    [Serializable]
    public class AttributeCreationException : Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AttributeCreationException()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Custom error message</param>
        public AttributeCreationException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="innerException">Original thrown exception</param>
        public AttributeCreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Serialization ctor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected AttributeCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}