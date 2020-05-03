using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common.Threading
{
    /// <summary>
    /// Worker thread
    /// </summary>
    internal class WorkerThread : IEquatable<WorkerThread>
    {
#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerThread"/> class.
        /// </summary>
        /// <param name="threadStart">The method that executes on the thread.</param>
        /// <param name="ownerName">The name.</param>
        public WorkerThread(ParameterizedThreadStart threadStart, string ownerName)
        {
            _ownerName = ownerName;
            _threadStartHandler = threadStart;

            Reset(0);
        }

#endregion

#region Fields

        private bool _isProcessing;
        /// <summary>
        /// The thread associated with this worker
        /// </summary>
        private Thread? _thread;
        /// <summary>
        /// Name of owner worker
        /// </summary>
        private readonly string _ownerName;
        /// <summary>
        /// Start method of this thread
        /// </summary>
        private readonly ParameterizedThreadStart _threadStartHandler;

#endregion

#region Properties

        /// <summary>
        /// Indicates, whether the worker _thread is stopped or not.
        /// </summary>
        public bool Stopped { get; set; }

        /// <summary>
        /// Gets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public int ThreadID
        {
            get { return _thread?.ManagedThreadId ?? 0; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _thread?.Name ?? string.Concat("WorkerThread_", _ownerName, "_undefined"); }
        }

        /// <summary>
        /// Gets a value indicating whether the _thread is closed.
        /// </summary>
        /// <value><c>true</c> if the thread is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get { return _thread == null; }
        }

        /// <summary>
        /// Start time of this thread
        /// </summary>
        public DateTime StartTime { get; private set; }

#endregion

#region Public methods

        /// <summary>
        /// Determines whether the specified timeout is timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><c>true</c> if the specified timeout is timeout; otherwise, <c>false</c>.</returns>
        public bool IsTimeout(TimeSpan timeout)
        {
            if (timeout == TimeSpan.Zero || !_isProcessing)
            {
                return false;
            }

            return StartTime == DateTime.MinValue || DateTime.UtcNow.Subtract(StartTime) >= timeout;
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public TimeSpan GetDuration()
        {
            return StartTime == DateTime.MinValue ? TimeSpan.Zero : DateTime.UtcNow.Subtract(StartTime);
        }

        /// <summary>
        /// Resets the specified PTS.
        /// </summary>
        /// <param name="threadID"></param>
        public bool Reset(int threadID)
        {
            if (!IsClosed && !Close(DateTimeHelper.TS_SECONDS_01, threadID))
            {
                return false;
            }

            Stopped = false;
            StartTime = DateTime.MinValue;
            _isProcessing = false;

            _thread = new Thread(_threadStartHandler) { IsBackground = true };
            _thread.Name = string.Concat("WorkerThread_", _ownerName, '_', _thread.ManagedThreadId);
            _thread.Start(this);

            return true;
        }

        /// <summary>
        /// Closes this thread after scecified waiting period.
        /// </summary>
        /// <param name="wait">The waiting period.</param>
        /// <param name="threadID"></param>
        /// <param name="join"></param>
        public bool Close(TimeSpan wait, int threadID, bool join = true)
        {
            if (IsClosed || threadID > 0 && ThreadID != threadID)
            {
                return false;
            }

            if (!Stopped)
            {
                Stopped = true;

                if (join && _thread?.Join(wait) == false)
                {
                    _thread.Abort();
                }
            }

            _thread = null;
            return true;
        }

        /// <summary>
        /// Start processing
        /// </summary>
        public void Start()
        {
            _isProcessing = true;
            StartTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Stop processing
        /// </summary>
        public void Stop()
        {
            _isProcessing = false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return _threadStartHandler.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as WorkerThread);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(WorkerThread? other)
        {
            return other != null && ThreadID == other.ThreadID;
        }

#endregion
    }
}