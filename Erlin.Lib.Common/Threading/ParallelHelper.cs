namespace Erlin.Lib.Common.Threading;

/// <summary>
///    Helper for parallel tasks
/// </summary>
public static class ParallelHelper
{
	public const string STACKTRACE_TASK = "TaskRunStackTrace";

	/// <summary>
	///    Setting to true force computing all ParallelHelper method runs synchronously
	/// </summary>
	public static bool GlobalSynchronousOnly
	{
		get;
		set
		{
			field = value;
			Log.Wrn( "ParallelHelper: Global settings for only one thread: {Value}", value );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel.
	/// </summary>
	/// <param name="fromInclusive">The start index, inclusive.</param>
	/// <param name="toExclusive">The end index, exclusive.</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only for</param>
	public static void For( int fromInclusive, int toExclusive, Action< int > action, bool synchronousOnly = false )
	{
		if( fromInclusive > toExclusive )
		{
			return;
		}

		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				action( i );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();

			_ = Parallel.For( fromInclusive, toExclusive, i =>
			{
				try
				{
					action( i );
				}
				catch( Exception ex )
				{
					ex.Data.Add( STACKTRACE_TASK, stackTrace );
					Log.Err( ex, "Parallel task failed!" );
				}
			} );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel.
	/// </summary>
	/// <param name="fromInclusive">The start index, inclusive.</param>
	/// <param name="toExclusive">The end index, exclusive.</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only for</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task ForAsync( int fromInclusive, int toExclusive, Func< int, CancellationToken, Task > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( fromInclusive > toExclusive )
		{
			return;
		}

		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				await action( i, cancelToken );
			}
		}
		else
		{
			HashSet< int > values = [ ];
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				values.Add( i );
			}

			await ParallelHelper.ForEachAsync( values, async ( i, token ) => { await action( i, token ); }, synchronousOnly, cancelToken );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel for 2D array.
	/// </summary>
	/// <param name="width">Width of 2D</param>
	/// <param name="height">Height of 2D</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only for</param>
	public static void For2D( int width, int height, Action< int, int > action, bool synchronousOnly = false )
	{
		if( ( width <= 0 ) || ( height <= 0 ) )
		{
			return;
		}

		int count = width * height;
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			for( int i = 0; i < count; i++ )
			{
				int x = i % width;
				int y = i / width;
				action( x, y );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();
			_ = Parallel.For( 0, count, i =>
			{
				try
				{
					int x = i % width;
					int y = i / width;
					action( x, y );
				}
				catch( Exception ex )
				{
					ex.Data.Add( STACKTRACE_TASK, stackTrace );
					Log.Err( ex, "Parallel task failed!" );
				}
			} );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel for 2D array.
	/// </summary>
	/// <param name="width">Width of 2D</param>
	/// <param name="height">Height of 2D</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only for</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task For2DAsync( int width, int height, Func< int, int, CancellationToken, Task > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( ( width <= 0 ) || ( height <= 0 ) )
		{
			return;
		}

		int count = width * height;
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			for( int i = 0; i < count; i++ )
			{
				int x = i % width;
				int y = i / width;
				await action( x, y, cancelToken );
			}
		}
		else
		{
			HashSet< int > values = [ ];
			for( int i = 0; i < count; i++ )
			{
				values.Add( i );
			}

			await ParallelHelper.ForEachAsync( values, async ( i, token ) =>
			{
				int x = i % width;
				int y = i / width;
				await action( x, y, token );
			}, synchronousOnly, cancelToken );
		}
	}

	/// <summary>
	///    Executes a foreach operation on an enumerable source in which iterations may run in parallel.
	/// </summary>
	/// <param name="source">An enumerable data source.</param>
	/// <param name="body">The delegate that is invoked once per iteration.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only foreach</param>
	/// <typeparam name="TSource">The type of the data in the source.</typeparam>
	public static void ForEach< TSource >( IEnumerable< TSource >? source, Action< TSource > body, bool synchronousOnly = false )
	{
		if( source == null )
		{
			return;
		}

		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			foreach( TSource fItem in source )
			{
				body( fItem );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();
			Parallel.ForEach( source, entity =>
			{
				try
				{
					body( entity );
				}
				catch( Exception ex )
				{
					ex.Data.Add( STACKTRACE_TASK, stackTrace );
					Log.Err( ex, "Parallel task failed!" );
				}
			} );
		}
	}

	/// <summary>
	///    Executes a for each operation on an enumerable source in which iterations may run in parallel.
	/// </summary>
	/// <typeparam name="TSource">The type of the data in the source.</typeparam>
	/// <param name="source">An enumerable data source.</param>
	/// <param name="body">An asynchronous delegate that is invoked once per element in the data source.</param>
	/// <param name="synchronousOnly">Whether this loop should use synchronous only foreach</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task ForEachAsync< TSource >( IEnumerable< TSource >? source, Func< TSource, CancellationToken, ValueTask > body, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( source == null )
		{
			return;
		}

		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			foreach( TSource fItem in source )
			{
				await body( fItem, CancellationToken.None );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();
			await Parallel.ForEachAsync( source, cancelToken, async ( entity, token ) =>
			{
				try
				{
					await body( entity, token );
				}
				catch( Exception ex )
				{
					ex.Data.Add( STACKTRACE_TASK, stackTrace );
					Log.Err( ex, "Parallel task failed!" );
				}
			} );
		}
	}

	/// <summary>
	///    Queues the specified work to run asynchronously
	/// </summary>
	/// <param name="action">The work to execute asynchronously</param>
	/// <param name="synchronousOnly">Whether the action should be executed synchronously</param>
	/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
	public static Task Run( Action action, bool synchronousOnly = false )
	{
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			action();
			return Task.CompletedTask;
		}

		string stackTrace = EnvHelper.GetStackTrace();
		return Task.Run( () =>
		{
			try
			{
				action();
			}
			catch( Exception ex )
			{
				ex.Data.Add( STACKTRACE_TASK, stackTrace );
				Log.Err( ex, "Parallel task failed!" );
				throw;
			}
		} );
	}

	/// <summary>
	///    Queues the specified asynchronous work to run asynchronously.
	/// </summary>
	/// <param name="action">The asynchronous work to execute.</param>
	/// <param name="synchronousOnly">Whether the action should be executed synchronously</param>
	/// <param name="cancelToken">Token for cancellation of the task.</param>
	/// <returns>A task that represents the asynchronous work.</returns>
	public static Task RunTask( Func< Task > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			return action();
		}

		string stackTrace = EnvHelper.GetStackTrace();
		return Task.Run( async () =>
		{
			try
			{
				cancelToken.ThrowIfCancellationRequested();
				await action();
			}
			catch( Exception ex )
			{
				ex.Data.Add( STACKTRACE_TASK, stackTrace );
				Log.Err( ex, "Parallel task failed!" );
				throw;
			}
		}, cancelToken );
	}

	/// <summary>
	///    Queues the specified asynchronous work to run asynchronously
	/// </summary>
	/// <param name="action">The work to execute asynchronously</param>
	/// <param name="synchronousOnly">Whether the action should be executed synchronously</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	/// <returns>A Task that represents a proxy for the Task returned by <paramref name="action"/>.</returns>
	public static Task RunTask( Func< CancellationToken, Task > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			return action( cancelToken );
		}

		string stackTrace = EnvHelper.GetStackTrace();
		return Task.Run( async () =>
		{
			try
			{
				cancelToken.ThrowIfCancellationRequested();
				await action( cancelToken );
			}
			catch( Exception ex )
			{
				ex.Data.Add( STACKTRACE_TASK, stackTrace );
				Log.Err( ex, "Parallel task failed!" );
				throw;
			}
		}, cancelToken );
	}

	/// <summary>
	///    Queues the specified asynchronous work to run asynchronously.
	/// </summary>
	/// <param name="action">The asynchronous work to execute.</param>
	/// <param name="synchronousOnly">Whether the action should be executed synchronously</param>
	/// <param name="cancelToken">Token for cancellation of the task.</param>
	/// <returns>A task that represents the asynchronous work.</returns>
	public static ValueTask RunValueTask( Func< ValueTask > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			return action();
		}

		string stackTrace = EnvHelper.GetStackTrace();
		return new ValueTask( Task.Run( async () =>
		{
			try
			{
				cancelToken.ThrowIfCancellationRequested();
				await action();
			}
			catch( Exception ex )
			{
				ex.Data.Add( STACKTRACE_TASK, stackTrace );
				Log.Err( ex, "Parallel task failed!" );
				throw;
			}
		}, cancelToken ) );
	}

	/// <summary>
	///    Queues the specified asynchronous work to run asynchronously
	/// </summary>
	/// <param name="action">The work to execute asynchronously</param>
	/// <param name="synchronousOnly">Whether the action should be executed synchronously</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	/// <returns>A Task that represents a proxy for the Task returned by <paramref name="action"/>.</returns>
	public static ValueTask RunValueTask( Func< CancellationToken, ValueTask > action, bool synchronousOnly = false, CancellationToken cancelToken = default )
	{
		if( synchronousOnly || ParallelHelper.GlobalSynchronousOnly )
		{
			return action( cancelToken );
		}

		string stackTrace = EnvHelper.GetStackTrace();
		return new ValueTask( Task.Run( async () =>
		{
			try
			{
				cancelToken.ThrowIfCancellationRequested();
				await action( cancelToken );
			}
			catch( Exception ex )
			{
				ex.Data.Add( STACKTRACE_TASK, stackTrace );
				Log.Err( ex, "Parallel task failed!" );
				throw;
			}
		}, cancelToken ) );
	}

	#region Delay methods

	/// <summary>
	///    Creates a Task that will complete after a time delay.
	/// </summary>
	/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned Task</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	public static Task< bool > Delay( int millisecondsDelay )
	{
		return ParallelHelper.Delay( millisecondsDelay, CancellationToken.None );
	}

	/// <summary>
	///    Creates a Task that will complete after a time delay.
	/// </summary>
	/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned Task</param>
	/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned Task</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	public static Task< bool > Delay( int millisecondsDelay, CancellationToken cancellationToken )
	{
		return ParallelHelper.Delay( millisecondsDelay, EnvHelper.Time.Provider, cancellationToken );
	}

	/// <summary>
	///    Creates a Task that will complete after a time delay.
	/// </summary>
	/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned Task</param>
	/// <param name="timeProvider">The <see cref="TimeProvider"/> with which to interpret <paramref name="millisecondsDelay"/>.</param>
	/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned Task</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	public static Task< bool > Delay( int millisecondsDelay, TimeProvider timeProvider, CancellationToken cancellationToken )
	{
		return ParallelHelper.Delay( TimeSpan.FromMilliseconds( millisecondsDelay ), timeProvider, cancellationToken );
	}

	/// <summary>
	///    Creates a Task that will complete after a time delay.
	/// </summary>
	/// <param name="delay">The time span to wait before completing the returned Task</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	///    The <paramref name="delay"/> is <see cref="Timeout.InfiniteTimeSpan"/>.
	/// </exception>
	public static Task< bool > Delay( TimeSpan delay )
	{
		return ParallelHelper.Delay( delay, EnvHelper.Time.Provider, CancellationToken.None );
	}

	/// <summary>Creates a task that completes after a specified time interval.</summary>
	/// <param name="delay">The <see cref="TimeSpan"/> to wait before completing the returned task, or <see cref="Timeout.InfiniteTimeSpan"/> to wait indefinitely.</param>
	/// <param name="timeProvider">The <see cref="TimeProvider"/> with which to interpret <paramref name="delay"/>.</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="delay"/> is <see cref="Timeout.InfiniteTimeSpan"/> or the <see cref="TimeSpan.TotalMilliseconds"/> property is outside intiger range.</exception>
	/// <exception cref="ArgumentNullException">The <paramref name="timeProvider"/> argument is null.</exception>
	public static Task< bool > Delay( TimeSpan delay, TimeProvider timeProvider )
	{
		return ParallelHelper.Delay( delay, timeProvider, CancellationToken.None );
	}

	/// <summary>
	///    Creates a Task that will complete after a time delay.
	/// </summary>
	/// <param name="delay">The time span to wait before completing the returned Task</param>
	/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned Task</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="delay"/> is <see cref="Timeout.InfiniteTimeSpan"/> or the <see cref="TimeSpan.TotalMilliseconds"/> property is outside intiger range.</exception>
	public static Task< bool > Delay( TimeSpan delay, CancellationToken cancellationToken )
	{
		return ParallelHelper.Delay( delay, EnvHelper.Time.Provider, cancellationToken );
	}

	/// <summary>Creates a cancellable task that completes after a specified time interval.</summary>
	/// <param name="delay">The <see cref="TimeSpan"/> to wait before completing the returned task, or <see cref="Timeout.InfiniteTimeSpan"/> to wait indefinitely.</param>
	/// <param name="timeProvider">The <see cref="TimeProvider"/> with which to interpret <paramref name="delay"/>.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the delay operation. The result is <see langword="true"/> if the full delay elapsed; otherwise, <see langword="false"/> if the delay was cancelled.</returns>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="delay"/> is <see cref="Timeout.InfiniteTimeSpan"/> or the <see cref="TimeSpan.TotalMilliseconds"/> property is outside intiger range.</exception>
	/// <exception cref="ArgumentNullException">The <paramref name="timeProvider"/> argument is null.</exception>
	public static async Task< bool > Delay( TimeSpan delay, TimeProvider timeProvider, CancellationToken cancellationToken )
	{
		if( delay == Timeout.InfiniteTimeSpan )
		{
			throw new ArgumentOutOfRangeException( nameof( delay ), delay, "Delay cannot be infinite." );
		}

		if( delay <= TimeSpan.Zero )
		{
			return true;
		}

		if( cancellationToken.IsCancellationRequested )
		{
			return false;
		}

		TaskCompletionSource< bool > cancelSource = new( TaskCreationOptions.RunContinuationsAsynchronously );

		await using CancellationTokenRegistration registration = cancellationToken.Register( static state =>
		{
			TaskCompletionSource< bool > tcs = ( TaskCompletionSource< bool > )state!;
			tcs.TrySetResult( false );
		}, cancelSource );

		Task delayTask = Task.Delay( delay, timeProvider, CancellationToken.None );

		Task completedTask = await Task.WhenAny( delayTask, cancelSource.Task );

		return completedTask == delayTask;
	}

	#endregion
}
