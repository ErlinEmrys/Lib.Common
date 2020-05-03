using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Erlin.Lib.Common.Logging
{
    /// <summary>
    /// Console loging mechanism
    /// </summary>
    public class ConsoleLog : ILog
    {
        private static object SyncRoot { get; } = new object();

        /// <summary>
        /// Ctor
        /// </summary>
        public ConsoleLog()
        {
            Console.OutputEncoding = Encoding.UTF8;
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
            message = $"[{eventTime.ToString("hh:mm:ss", CultureInfo.InvariantCulture)}] {message}";

            lock (SyncRoot)
            {
                ConsoleColor origBackground = Console.BackgroundColor;
                ConsoleColor origForeground = Console.ForegroundColor;
                Console.BackgroundColor = PickBackground(level);
                Console.ForegroundColor = PickForeground(level);

                Console.WriteLine(message);

                Console.BackgroundColor = origBackground;
                Console.ForegroundColor = origForeground;
            }
        }

        /// <summary>
        /// Picks background color for system console based on level of the event
        /// </summary>
        /// <param name="level">Level of the logged event</param>
        /// <returns>Picked background color</returns>
        private static ConsoleColor PickBackground(TraceLevel level)
        {
            switch (level)
            {
                case TraceLevel.Verbose:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.Black;
            }
        }

        /// <summary>
        /// Picks foreground color for system console based on level of the event
        /// </summary>
        /// <param name="level">Level of the logged event</param>
        /// <returns>Picked foreground color</returns>
        private static ConsoleColor PickForeground(TraceLevel level)
        {
            switch (level)
            {
                case TraceLevel.Error:
                    return ConsoleColor.Red;
                case TraceLevel.Warning:
                    return ConsoleColor.Yellow;
                case TraceLevel.Info:
                    return ConsoleColor.Green;
                case TraceLevel.Verbose:
                    return ConsoleColor.Blue;
                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}