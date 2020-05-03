using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Main program loop for WIN 10 - keep PC awake, monitor awake and do GC
    /// </summary>
    public class Win10Loop : IDisposable
    {
        private readonly Timer _timer;

        /// <summary>
        /// Current loop
        /// </summary>
        public static Win10Loop? Instance { get; private set; }

        /// <summary>
        /// Loop will keep this PC awake
        /// </summary>
        public bool KeepPcAwake { get; set; }

        /// <summary>
        /// Loop will keep this PC monitor awake
        /// </summary>
        public bool KeepMonitorAwake { get; set; }

        /// <summary>
        /// Do automatic Garabage collection
        /// </summary>
        public bool DoGarbage { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Win10Loop(bool keepPcAwake, bool keepMonitorAwake, bool doGarbage)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Second Win10Loop object at same moment!");
            }

            Instance = this;

            KeepMonitorAwake = keepMonitorAwake;
            KeepPcAwake = keepPcAwake;
            DoGarbage = doGarbage;

            _timer = new Timer(OnTimerElapsed, this, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            _timer.Dispose();
            Instance = null;
        }

        /// <summary>
        /// Action when timer elapsed
        /// </summary>
        /// <param name="state">Timer state object</param>
        private static void OnTimerElapsed(object? state)
        {
            Log.Trace("Win10Loop.Tick");

            try
            {
                if (!(state is Win10Loop loop))
                {
                    throw new InvalidOperationException();
                }

                if (loop.DoGarbage)
                {
                    EnvironmentHelper.CallGarbageCollector();
                }

                if (loop.KeepMonitorAwake || loop.KeepPcAwake)
                {
                    ExecutionState flags = ExecutionState.ES_SYSTEM_REQUIRED;
                    if (loop.KeepMonitorAwake)
                    {
                        flags |= ExecutionState.ES_DISPLAY_REQUIRED;
                    }

                    SetThreadExecutionState(flags);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        /// <summary>
        /// Tells the system, that this program is working and pc should not go into sleep, or turn off monitor
        /// </summary>
        /// <param name="esFlags">Flags of options</param>
        /// <returns>Result state</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        /// <summary>
        /// P/Invoke states
        /// </summary>
        [Flags]
        private enum ExecutionState : uint
        {
            //ES_CONTINUOUS = 0x80000000,
            /// <summary>
            /// System is required
            /// </summary>
            ES_SYSTEM_REQUIRED = 0x00000001,
            /// <summary>
            /// Display is required
            /// </summary>
            ES_DISPLAY_REQUIRED = 0x00000002,

            //ES_AWAYMODE_REQUIRED = 0x00000040,
        }
    }
}