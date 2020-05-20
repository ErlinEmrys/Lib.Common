using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.FileSystem;

namespace Erlin.Lib.Common.Reflection
{
    /// <summary>
    /// Helper class for working with assemblies
    /// </summary>
    public static class AssemblyHelper
    {
        private static string? _baseLocation;

        /// <summary>
        /// Return current Lib.Common assembly
        /// </summary>
        public static Assembly CommonBaseAssembly { get; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Path to location of this base assembly
        /// </summary>
        public static string BaseLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_baseLocation))
                {
                    string? dir = null;
                    Process currentProcess = Process.GetCurrentProcess();
                    if (currentProcess.MainModule != null)
                    {
                        dir = FileHelper.GetDirectoryPath(currentProcess.MainModule.FileName);
                    }
                    else
                    {
                        throw new InvalidOperationException("Current process missing MainModule!");
                    }

                    _baseLocation = dir;
                }

                return _baseLocation;
            }
        }
    }
}