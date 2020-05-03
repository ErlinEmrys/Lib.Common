using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Erlin.Lib.Common.Exceptions;

namespace Erlin.Lib.Common.Logging
{
    /// <summary>
    /// Transfer message logging to several log destinations
    /// </summary>
    public class LogMultiplier : ILog
    {
        private readonly IReadOnlyCollection<ILog> _logDestinations;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logDestinations">Log destinations</param>
        public LogMultiplier(params ILog[] logDestinations)
        {
            _logDestinations = new ReadOnlyCollection<ILog>(logDestinations);
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            foreach (ILog fLog in _logDestinations)
            {
                fLog.Dispose();
            }
        }

        /// <summary>
        /// Log any exception
        /// </summary>
        /// <param name="level">Level of the event</param>
        /// <param name="eventTime">Time of the event</param>
        /// <param name="message">Error message to log</param>
        public void Log(TraceLevel level, DateTime eventTime, string message)
        {
            LogToAllDestinations(eventTime, log => log.Log(level, eventTime, message));
        }

        /// <summary>
        /// Makes log to all log destinations
        /// </summary>
        /// <param name="eventTime">Time of the logged event</param>
        /// <param name="logAction">Dynamic log action</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void LogToAllDestinations(DateTime eventTime, Action<ILog> logAction)
        {
            Dictionary<ILog, Exception> logErrors = new Dictionary<ILog, Exception>();

            //Attempt to write log message
            foreach (ILog fLog in _logDestinations)
            {
                try
                {
                    logAction(fLog);
                }
                catch (Exception ex)
                {
                    ex.PreserveStackTrace();
                    logErrors.Add(fLog, ex);
                }
            }

            if (logErrors.Count > 0)
            {
                //Attempt to write log errors
                if (logErrors.Count < _logDestinations.Count)
                {
                    //Some of log systems still works - use it!
                    IEnumerable<ILog> workingLogs = _logDestinations.Where(l => !logErrors.ContainsKey(l));
                    foreach (ILog fLog in workingLogs)
                    {
                        foreach (Exception fError in logErrors.Values)
                        {
                            string message = fError.ToStringFull();
                            fLog.Log(TraceLevel.Error, eventTime, message);
                        }
                    }
                }
                else
                {
                    //Every log system throws exception - we are totaly screwd!
                    throw new CouldNotWriteLogException();
                }
            }
        }
    }
}