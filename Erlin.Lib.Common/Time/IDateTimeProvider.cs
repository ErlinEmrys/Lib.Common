using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Time
{
    /// <summary>
    /// Interface for providing date and time
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Returns current date and time
        /// </summary>
        /// <returns>Current date and time</returns>
        DateTime Now { get; }
    }
}