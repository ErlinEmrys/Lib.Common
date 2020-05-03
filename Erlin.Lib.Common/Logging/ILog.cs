using System;
using System.Diagnostics;

namespace Erlin.Lib.Common.Logging
{
    /// <summary>
    /// Interface of any basic loging mechanism
    /// </summary>
    public interface ILog : IDisposable
    {
        /// <summary>
        /// Log any exception
        /// </summary>
        /// <param name="level">Level of the event</param>
        /// <param name="eventTime">Time of the event</param>
        /// <param name="message">Error message to log</param>
        void Log(TraceLevel level, DateTime eventTime, string message);
    }
}