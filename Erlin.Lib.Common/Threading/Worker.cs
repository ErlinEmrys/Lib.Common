using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common.Threading
{
    /// <summary>
    /// Worker for asynchronous process execution
    /// </summary>
    /// <typeparam name="T">Runtime type of items to process</typeparam>
    public sealed class Worker<T> : IDisposable
        where T : notnull
    {
        /// <summary>
        /// Worker item to process
        /// </summary>
        private class WorkerItem
        {
#region Fields

            /// <summary>
            /// Object to process
            /// </summary>
            internal T Value { get; }

#endregion

#region Constructors

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="value">Object to process</param>
            public WorkerItem(T value)
            {
                Value = value;
            }

#endregion Constructors
        }

#region Fields

        private bool _disposed;
        private readonly List<WorkerThread> _threads = new List<WorkerThread>();
        private readonly object _lock = new object();
        private readonly Semaphore _semaphore = new Semaphore(0, int.MaxValue);
        private readonly Action<T> _handler;
        private readonly ConcurrentQueue<WorkerItem> _queue = new ConcurrentQueue<WorkerItem>();
        private int _requiredThreads;
        private int _currentProcessingItemsCount;

#endregion

#region Properties

        /// <summary>
        /// Gets the name of the worker.
        /// </summary>
        /// <value>The name of the worker.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the require threads count
        /// </summary>
        /// <value>The require threads count</value>
        public int RequiredThreads
        {
            get
            {
                return _requiredThreads;
            }
            set
            {
                if (_requiredThreads != value)
                {
                    _requiredThreads = Math.Min(100, Math.Max(0, value));
                    OnChanged();
                }
            }
        }

        /// <summary>
        /// Gets the current threads count
        /// </summary>
        /// <value>Current threads count</value>
        public int Threads
        {
            get { return _threads.Count; }
        }

        /// <summary>
        /// Gets the queue count
        /// </summary>
        /// <value>Queue count</value>
        public int QueueCount
        {
            get { return _queue.Count; }
        }

        /// <summary>
        /// Indicates if worker has not processed items
        /// </summary>
        public bool IsProcessing
        {
            get
            {
                return _queue.Count > 0 && !IsSuspended || _currentProcessingItemsCount > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is suspended
        /// </summary>
        public bool IsSuspended { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this worker is activated
        /// </summary>
        public bool Activated { get; private set; }

#endregion

#region Constructors

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name of the worker.</param>
        /// <param name="handler">A delegate representing a method to be executed</param>
        /// <param name="requiredThreads">Number of threads to process items</param>
        public Worker(string name, Action<T> handler, int requiredThreads)
        {
            _handler = handler;
            Name = name;
            _requiredThreads = Math.Max(1, requiredThreads);
        }

        /// <summary>
        /// Dispose this worker
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                SetThreads(0);

                _semaphore.Close();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

#endregion

#region Methods

        /// <summary>
        /// Creates an entries with the specified values and enqueues them
        /// </summary>
        /// <param name="items">Multiple entry items</param>
        public void Enqueue(IEnumerable<T>? items)
        {
            if (items != null)
            {
                int count = 0;
                foreach (T fItem in items)
                {
                    _queue.Enqueue(new WorkerItem(fItem));
                    count++;
                }

                if (count > 0)
                {
                    if (!Activated)
                    {
                        SetThreads(RequiredThreads);
                    }

                    if (!IsSuspended)
                    {
                        _semaphore.Release(count);
                    }
                }
            }
        }

        /// <summary>
        /// Creates an entry with the specified value and enqueues it
        /// </summary>
        /// <param name="item">Entry item</param>
        public void Enqueue(T item)
        {
            if (!Activated)
            {
                SetThreads(RequiredThreads);
            }

            _queue.Enqueue(new WorkerItem(item));

            if (!IsSuspended)
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Resets this worker
        /// </summary>
        public void Reset()
        {
            Suspend();
            _queue.Clear();
            Resume();
        }

        /// <summary>
        /// Suspends this worker
        /// </summary>
        public void Suspend()
        {
            if (IsSuspended)
            {
                return;
            }

            lock (_lock)
            {
                IsSuspended = true;
            }

            while (_currentProcessingItemsCount > 0)
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Resumes this worker
        /// </summary>
        public void Resume()
        {
            if (!IsSuspended || _disposed)
            {
                return;
            }

            lock (_lock)
            {
                IsSuspended = false;

                _semaphore.Release(Math.Max(1, QueueCount) * 2);
            }
        }

        /// <summary>
        /// Sets the count of threads
        /// </summary>
        /// <param name="desiredThreadsCount">Desired thread counts</param>
        private void SetThreads(int desiredThreadsCount)
        {
            lock (_lock)
            {
                if (Activated && _threads.Count == desiredThreadsCount)
                {
                    return;
                }

                Activated = true;

                if (_threads.Count < desiredThreadsCount)
                {
                    for (int i = _threads.Count; i < desiredThreadsCount; i++)
                    {
                        _threads.Add(new WorkerThread(Process, Name));
                    }
                }
                else if (_threads.Count > desiredThreadsCount)
                {
                    int count = _threads.Count - desiredThreadsCount;
                    List<WorkerThread> removes = _threads.GetRange(_threads.Count - count, count);

                    removes.ForEach(wt => wt.Stopped = true);

                    _semaphore.Release(_threads.Count * 10);

                    Thread.Sleep(DateTimeHelper.TS_SECONDS_01);

                    foreach (WorkerThread fWorkerThread in removes)
                    {
                        fWorkerThread.Close(DateTimeHelper.TS_SECONDS_01, 0);

                        if (_threads.Contains(fWorkerThread))
                        {
                            _threads.Remove(fWorkerThread);
                        }
                    }

                    _semaphore.Release(Math.Max(1, QueueCount + 1));
                }
            }
        }

        /// <summary>
        /// Process thread
        /// </summary>
        /// <param name="state">Worker thread</param>
        private void Process(object? state)
        {
            WorkerThread? wt = state as WorkerThread;
            if (wt == null)
            {
                throw new InvalidOperationException();
            }

            bool exit = false;

            while (!exit && !_disposed && !wt.Stopped)
            {
                try
                {
                    while (!wt.Stopped)
                    {
                        _semaphore.WaitOne();

                        if (IsSuspended)
                        {
                            continue;
                        }

                        if (_queue.Count == 0)
                        {
                            break;
                        }

                        if (!_queue.TryDequeue(out WorkerItem? entry))
                        {
                            continue;
                        }

                        wt.Start();

                        try
                        {
                            Interlocked.Increment(ref _currentProcessingItemsCount);
                            _handler(entry.Value);
                        }
                        finally
                        {
                            wt.Stop();
                            Interlocked.Decrement(ref _currentProcessingItemsCount);
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                    exit = true;
                }
                catch (ThreadInterruptedException)
                {
                    exit = true;
                }
                catch (Exception e)
                {
                    wt.Stop();
                    Log.Error(e, $"Worker.{Name}");
                }
                finally
                {
                    if (exit || _disposed || wt.Stopped)
                    {
                        try
                        {
                            wt.Close(TimeSpan.Zero, Thread.CurrentThread.ManagedThreadId, false);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when a configuration is changed
        /// </summary>
        private void OnChanged()
        {
            if (!Activated || RequiredThreads == Threads || _disposed)
            {
                return;
            }

            SetThreads(RequiredThreads);
        }

#endregion
    }
}