using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Reflection
{
    /// <summary>
    /// Helper class for working with assemblies
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Return current Lib.Common assembly
        /// </summary>
        public static Assembly CommonBaseAssembly { get; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Path to location of this base assembly
        /// </summary>
        public static string BaseLocation { get; } = Path.GetDirectoryName(CommonBaseAssembly.Location) ?? throw new InvalidOperationException();
    }
}