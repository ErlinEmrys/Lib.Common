using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Simple debug class for measuring time
    /// </summary>
    public class Debugwatch : IDisposable
    {
        /// <summary>
        /// Associated message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Start time
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Associated message</param>
        public Debugwatch(string message)
        {
            Start = DateTimeHelper.Now;
            Message = message;
        }

        /// <summary>
        /// Stops the watch and log info
        /// </summary>
        public void Dispose()
        {
            TimeSpan duration = DateTimeHelper.Now - Start;
            Log.Trace($"{Message} [{duration}]");
        }
    }
}