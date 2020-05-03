using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common.Logging
{
    /// <summary>
    /// Attached debugger loging mechanism
    /// </summary>
    public class DebuggerLog : ILog
    {
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Minimal log threshold level
        /// </summary>
        public TraceLevel MinLogThreshold { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="minLogThreshold">Minimal log threshold level</param>
        public DebuggerLog(TraceLevel minLogThreshold)
        {
            MinLogThreshold = minLogThreshold;
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Log any message
        /// </summary>
        /// <param name="level">Level of the event</param>
        /// <param name="eventTime">Time of the event</param>
        /// <param name="message">Message to log</param>
        public void Log(TraceLevel level, DateTime eventTime, string message)
        {
            if (MinLogThreshold != TraceLevel.Off && level <= MinLogThreshold)
            {
                lock (SyncRoot)
                {
                    Debugger.Log(0, null, $"[{level}][{eventTime.ToString(DateTimeHelper.FORMAT_TIME_TICKS)}]{message}{Environment.NewLine}");
                }
            }
        }
    }
}