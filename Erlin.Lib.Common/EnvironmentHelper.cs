using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Helper class for environment variables
    /// </summary>
    public static class EnvironmentHelper
    {
        private static IReadOnlyCollection<string> NewLineBreakers { get; } = new ReadOnlyCollection<string>(new[] { "\r\n", "\n", "\r" });

        /// <summary>
        /// Provider of date and time
        /// </summary>
        public static IDateTimeProvider DateTime { get; set; } = new SystemDateTimeProvider();

        /// <summary>
        /// All variants of standart line breaker
        /// </summary>
        public static string[] GetNewLineBreakers()
        {
            return NewLineBreakers.ToArray();
        }

        /// <summary>
        /// Call .net garbage collector
        /// </summary>
        public static void CallGarbageCollector()
        {
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Call to power off PC, call before program exit
        /// </summary>
        public static void PowerOffPc()
        {
            Log.Warning("Shuting down PC in 3 seconds!");
            Process.Start("shutdown", "/s /t 3");
        }

        /// <summary>
        /// Returns current stack trace (without this method call)
        /// </summary>
        /// <returns>Current stack trace</returns>
        public static string GetStackTrace()
        {
            StackTrace trace = new StackTrace(2, true);
            return trace.ToString();
        }
    }
}