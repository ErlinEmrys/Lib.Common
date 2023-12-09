using System.Collections.Concurrent;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common.Threading;

/// <summary>
///    Worker for asynchronous process execution
/// </summary>
public sealed class TaskWorker<T> : IAsyncDisposable
	where T : notnull
{
	public const string WORKER_NAME_PREFIX = "TaskWorker:";

	private bool _disposed;
	private readonly bool _cancelOnDispose;
	private readonly Func<T, CancellationToken, Task> _handler;
	private readonly ConcurrentQueue<T> _queue = new();
	private readonly HashSet<WorkItem<T>> _work = [];
	private CancellationTokenSource _cancelSource = new();

	public TaskWorker(
		string name,
		Func<T, CancellationToken, Task> handler, int maxConcurrency, bool cancelOnDispose = true )
	{
		Name = WORKER_NAME_PREFIX + name;
		_cancelOnDispose = cancelOnDispose;
		_handler = handler;
		MaxConcurrency = maxConcurrency;
	}

	/// <summary>
	///    Worker name
	/// </summary>
	public string Name { get; }

	/// <summary>
	///    Number of items waiting in queue to be processed
	/// </summary>
	public int QueueCount
	{
		get { return _queue.Count; }
	}

	/// <summary>
	///    Number of items currently being processed
	/// </summary>
	public int CurrentConcurrency
	{
		get
		{
			lock( _work )
			{
				return _work.Count;
			}
		}
	}

	/// <summary>
	///    Whether the worker contains no items to process
	/// </summary>
	public bool IsEmpty
	{
		get
		{
			lock( _work )
			{
				return QueueCount + _work.Count <= 0;
			}
		}
	}

	private int _maxConcurrency;

	/// <summary>
	///    Target for how many items work at same time
	/// </summary>
	public int MaxConcurrency
	{
		get { return _maxConcurrency; }
		set
		{
			if( _maxConcurrency != value )
			{
				_maxConcurrency = value;
				Start();
			}
		}
	}

	/// <summary>
	///    Whether the work of this Worker has been suspended
	/// </summary>
	public bool IsSuspended { get; private set; }

	/// <summary>
	///    Suspend the work of this Worker, returned Task serves as waiting handle for items currently
	///    being processed
	/// </summary>
	/// <returns></returns>
	public Task Suspend()
	{
		if( !IsSuspended )
		{
			IsSuspended = true;
			Log.Dbg( "{Worker} has been suspended", Name );
		}

		return WaitToFinish();
	}

	/// <summary>
	///    Resume the work of this Worker after it was suspended
	/// </summary>
	public void Resume()
	{
		if( IsSuspended )
		{
			IsSuspended = false;
			Log.Dbg( "{Worker} is being resumed", Name );

			Start();
		}
	}

	/// <summary>
	///    Full reset of this Worker: clear all queued items and cancel all currently processed items
	/// </summary>
	public async Task Reset()
	{
		Log.Dbg( "{Worker} is being reset", Name );

		Task suspendTask = Suspend();
		await _cancelSource.CancelAsync();
		await suspendTask;

		_cancelSource = new CancellationTokenSource();
		_queue.Clear();

		Resume();
	}

	/// <summary>
	///    Creates an entries with the specified values and enqueues them
	/// </summary>
	/// <param name="items">Multiple entry items</param>
	public void Enqueue( IEnumerable<T>? items )
	{
		if( items != null )
		{
			int count = 0;
			foreach( T fItem in items )
			{
				_queue.Enqueue( fItem );
				count++;
			}

			if( count > 0 )
			{
				Log.Vrb( "{Worker} enqueued {Count} items", Name, count );
				Start();
			}
		}
	}

	/// <summary>
	///    Creates an entry with the specified value and enqueues it
	/// </summary>
	/// <param name="item">Entry item</param>
	public void Enqueue( T item )
	{
		Enqueue(
			new[]
			{
				item
			} );
	}

	/// <summary>
	///    Starts working on the queued items
	/// </summary>
	private void Start( WorkItem<T>? previousWork = null )
	{
		lock( _work )
		{
			if( previousWork != null )
			{
				_work.Remove( previousWork );
			}

			if( _disposed || IsSuspended || _queue.IsEmpty )
			{
				return;
			}

			int count = Math.Min( _queue.Count, MaxConcurrency - _work.Count );

			if( count > 0 )
			{
				Log.Vrb( "{Worker} starting {Count} tasks", Name, count );

				for( int i = 0; i < count; i++ )
				{
					WorkItem<T>? work = RunTask();
					if( work != null )
					{
						_work.Add( work );
					}
				}
			}
		}
	}

	/// <summary>
	///    Runs a working task for one queue item
	/// </summary>
	/// <returns></returns>
	private WorkItem<T>? RunTask()
	{
		if( _disposed || !_queue.TryDequeue( out T? item ) )
		{
			return null;
		}

		Task work = ParallelHelper.Run(
			async token =>
			{
				try
				{
					await _handler( item, token );
				}
				catch( Exception e )
				{
					Log.Err(
						e, "{Worker} failed task for item {Item}{NewLine}{Exception}", Name, item,
						Environment.NewLine, e.ToJson() );
				}
			}, _cancelSource.Token );

		WorkItem<T> workItem = new( item, work );
		workItem.Continuation = workItem.Work.ContinueWith(
			_ =>
			{
				try
				{
					Start( workItem );
				}
				catch( Exception e )
				{
					Log.Err( e, "{Worker} failed task continuation", Name );
				}
			} );

		return workItem;
	}

	/// <summary>
	///    Waits to process all the items
	/// </summary>
	public async Task WaitToFinish( CancellationToken cancelToken = default )
	{
		Start:
		while( !IsSuspended && ( QueueCount > 0 ) )
		{
			await Task.Delay( IDateTimeProvider.TS_MILLISECONDS_010, cancelToken );
		}

		await Task.WhenAll( GetAllWorkTasks() );

		if( ( !IsSuspended && ( QueueCount > 0 ) ) || ( GetAllWorkTasks().Count > 0 ) )
		{
			goto Start;
		}
	}

	/// <summary>
	///    Releasing of all resources
	/// </summary>
	public async ValueTask DisposeAsync()
	{
		Log.Dbg( "{Worker} is being disposed!", Name );

		if( _disposed )
		{
			return;
		}

		_disposed = true;

		if( _cancelOnDispose )
		{
			await _cancelSource.CancelAsync();
		}

		await Task.WhenAny(
			Task.WhenAll( GetAllWorkTasks() ),
			Task.Delay(
				_cancelOnDispose ? IDateTimeProvider.TS_MILLISECONDS_100
					: IDateTimeProvider.TS_MINUS_MILLISECONDS_001 ) );

		_cancelSource.Dispose();
		Log.Dbg( "{Worker} disposed!", Name );
	}

	/// <summary>
	///    Get all actual runtime Tasks that this Worker utilizes
	/// </summary>
	private HashSet<Task> GetAllWorkTasks()
	{
		HashSet<Task> workTasks = [];
		lock( _work )
		{
			foreach( WorkItem<T> fWork in _work )
			{
				workTasks.Add( fWork.Work );
				workTasks.Add( fWork.Continuation );
			}
		}

		return workTasks;
	}

	/// <summary>
	///    Internal worker item
	/// </summary>
	private sealed class WorkItem<TT>( TT item, Task work )
		where TT : notnull
	{
		/// <summary>
		///    Item we are working on
		/// </summary>

		// ReSharper disable once UnusedMember.Local
		public TT Item { get; init; } = item;

		/// <summary>
		///    Task representing working on item
		/// </summary>
		public Task Work { get; } = work;

		/// <summary>
		///    Task representing continuation after work is completed
		/// </summary>
		public Task Continuation { get; set; } = default!;
	}
}
