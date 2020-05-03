using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Misc
{
    /// <summary>
    /// Mathematic calculation helper
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Check if entered number is non-floating point number
        /// </summary>
        /// <param name="value">Number to check</param>
        /// <returns>False - value is floating point number</returns>
        public static bool IsWholeInteger(decimal value)
        {
            return Math.Abs(value % 1) <= 0.0000000000000000000000001m;
        }
    }
}