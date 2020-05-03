using System;
using System.Runtime.Serialization;

namespace Erlin.Lib.Common.Exceptions
{
    /// <summary>
    /// Exception when we are not able to write log through any configured log system
    /// </summary>
    [Serializable]
    public class CouldNotWriteLogException : Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CouldNotWriteLogException()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Custom error message</param>
        public CouldNotWriteLogException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="innerException">Original thrown exception</param>
        public CouldNotWriteLogException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Serialization ctor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected CouldNotWriteLogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}