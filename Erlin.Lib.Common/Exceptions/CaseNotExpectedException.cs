using System;
using System.Runtime.Serialization;

namespace Erlin.Lib.Common.Exceptions
{
    /// <summary>
    /// Exception when go to not implemented and not expected case statement
    /// </summary>
    [Serializable]
    public class CaseNotExpectedException : Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="value">Value of the case</param>
        public CaseNotExpectedException(object value) : base(CreateMessage(value))
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="value">Value of the case</param>
        /// <param name="innerException">Original thrown exception</param>
        public CaseNotExpectedException(object value, Exception? innerException) : base(CreateMessage(value), innerException)
        {
        }

        /// <summary>
        /// Serialization ctor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected CaseNotExpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Create error message
        /// </summary>
        /// <param name="value">Value of the case</param>
        /// <returns>Error message</returns>
        private static string CreateMessage(object value)
        {
            return $"Case not expected or implemented for value: {value}";
        }
    }
}