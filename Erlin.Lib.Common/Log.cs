using System;
using System.Diagnostics;

using Erlin.Lib.Common.Logging;

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Basic logging class
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Current log system
        /// </summary>
        public static ILog LogSystem { get; set; } = new LogMultiplier();

        /// <summary>
        /// Log any exception as info
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Additional message</param>
        public static void Info(Exception ex, string? message = null)
        {
            string text = ex.ToStringFull();
            if (!string.IsNullOrWhiteSpace(message))
            {
                text += Environment.NewLine + Environment.NewLine + message;
            }

            LogSystem.Log(TraceLevel.Info, EnvironmentHelper.DateTime.Now, text);
        }

        /// <summary>
        /// Log any exception
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Additional message</param>
        public static void Error(Exception ex, string? message = null)
        {
            string text = ex.ToStringFull();
            if (!string.IsNullOrWhiteSpace(message))
            {
                text += Environment.NewLine + Environment.NewLine + message;
            }

            LogSystem.Log(TraceLevel.Error, EnvironmentHelper.DateTime.Now, text);
        }

        /// <summary>
        /// Log custom error message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Error(string message)
        {
            LogSystem.Log(TraceLevel.Error, EnvironmentHelper.DateTime.Now, message);
        }

        /// <summary>
        /// Log custom warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Warning(string message)
        {
            LogSystem.Log(TraceLevel.Warning, EnvironmentHelper.DateTime.Now, message);
        }

        /// <summary>
        /// Log custom warning message
        /// </summary>
        /// <param name="condition">Dynamic condition if log this message</param>
        /// <param name="message">Message to log</param>
        public static void Warning(bool condition, string message)
        {
            if (condition)
            {
                Warning(message);
            }
        }

        /// <summary>
        /// Log custom info message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Info(string message)
        {
            LogSystem.Log(TraceLevel.Info, EnvironmentHelper.DateTime.Now, message);
        }

        /// <summary>
        /// Log custom info message
        /// </summary>
        /// <param name="condition">Dynamic condition if log this message</param>
        /// <param name="message">Message to log</param>
        public static void Info(bool condition, string message)
        {
            if (condition)
            {
                Info(message);
            }
        }

        /// <summary>
        /// Log custom trace message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Trace(string message)
        {
            LogSystem.Log(TraceLevel.Verbose, EnvironmentHelper.DateTime.Now, message);
        }

        /// <summary>
        /// Log custom trace message
        /// </summary>
        /// <param name="condition">Dynamic condition if log this message</param>
        /// <param name="message">Message to log</param>
        public static void Trace(bool condition, string message)
        {
            if (condition)
            {
                Trace(message);
            }
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public static void Dispose()
        {
            LogSystem?.Dispose();
        }
    }
}