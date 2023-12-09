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
	public static bool GlobalOneThread { get; set; }

	static ParallelHelper()
	{
		if( ParallelHelper.GlobalOneThread )
		{
			Log.Wrn( "ParallelHelper: Global settings for only one thread enabled!" );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel.
	/// </summary>
	/// <param name="fromInclusive">The start index, inclusive.</param>
	/// <param name="toExclusive">The end index, exclusive.</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="oneThread">Whether this loop should use only one-thread for</param>
	public static void For( int fromInclusive, int toExclusive, Action<int> action, bool oneThread = false )
	{
		if( fromInclusive > toExclusive )
		{
			return;
		}

		if( oneThread || ParallelHelper.GlobalOneThread )
		{
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				action( i );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();

			_ = Parallel.For(
				fromInclusive, toExclusive, i =>
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
	/// <param name="oneThread">Whether this loop should use only one-thread for</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task ForAsync(
		int fromInclusive, int toExclusive, Func<int, CancellationToken, Task> action,
		bool oneThread = false, CancellationToken cancelToken = default )
	{
		if( fromInclusive > toExclusive )
		{
			return;
		}

		if( oneThread || ParallelHelper.GlobalOneThread )
		{
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				await action( i, cancelToken );
			}
		}
		else
		{
			HashSet<int> values = [];
			for( int i = fromInclusive; i < toExclusive; i++ )
			{
				values.Add( i );
			}

			await ParallelHelper.ForEachAsync(
				values, async ( i, token ) => { await action( i, token ); }, oneThread, cancelToken );
		}
	}

	/// <summary>
	///    Executes a for loop in which iterations may run in parallel for 2D array.
	/// </summary>
	/// <param name="width">Width of 2D</param>
	/// <param name="height">Height of 2D</param>
	/// <param name="action">The delegate that is invoked once per iteration.</param>
	/// <param name="oneThread">Whether this loop should use only one-thread for</param>
	public static void For2D( int width, int height, Action<int, int> action, bool oneThread = false )
	{
		if( ( width <= 0 ) || ( height <= 0 ) )
		{
			return;
		}

		int count = width * height;
		if( oneThread || ParallelHelper.GlobalOneThread )
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
			_ = Parallel.For(
				0, count, i =>
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
	/// <param name="oneThread">Whether this loop should use only one-thread for</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task For2DAsync(
		int width, int height, Func<int, int, CancellationToken, Task> action, bool oneThread = false,
		CancellationToken cancelToken = default )
	{
		if( ( width <= 0 ) || ( height <= 0 ) )
		{
			return;
		}

		int count = width * height;
		if( oneThread || ParallelHelper.GlobalOneThread )
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
			HashSet<int> values = [];
			for( int i = 0; i < count; i++ )
			{
				values.Add( i );
			}

			await ParallelHelper.ForEachAsync(
				values, async ( i, token ) =>
				{
					int x = i % width;
					int y = i / width;
					await action( x, y, token );
				}, oneThread, cancelToken );
		}
	}

	/// <summary>
	///    Executes a foreach operation on an enumerable source in which iterations may run in parallel.
	/// </summary>
	/// <param name="source">An enumerable data source.</param>
	/// <param name="body">The delegate that is invoked once per iteration.</param>
	/// <param name="oneThread">Whether this loop should use only one-thread foreach</param>
	/// <typeparam name="TSource">The type of the data in the source.</typeparam>
	public static void ForEach<TSource>(
		IEnumerable<TSource>? source, Action<TSource> body, bool oneThread = false )
	{
		if( source == null )
		{
			return;
		}

		if( oneThread || ParallelHelper.GlobalOneThread )
		{
			foreach( TSource fItem in source )
			{
				body( fItem );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();
			Parallel.ForEach(
				source, entity =>
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
	/// <param name="oneThread">Whether this loop should use only one-thread foreach</param>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static async Task ForEachAsync<TSource>(
		IEnumerable<TSource>? source, Func<TSource, CancellationToken, ValueTask> body,
		bool oneThread = false, CancellationToken cancelToken = default )
	{
		if( source == null )
		{
			return;
		}

		if( oneThread || ParallelHelper.GlobalOneThread )
		{
			foreach( TSource fItem in source )
			{
				await body( fItem, CancellationToken.None );
			}
		}
		else
		{
			string stackTrace = EnvHelper.GetStackTrace();
			await Parallel.ForEachAsync(
				source, cancelToken,
				async ( entity, token ) =>
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
	/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
	public static Task Run( Action action )
	{
		string stackTrace = EnvHelper.GetStackTrace();
		return Task.Run(
			() =>
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
	///    Queues the specified asynchronous work to run asynchronously
	/// </summary>
	/// <param name="action">The work to execute asynchronously</param>
	/// <returns>A Task that represents a proxy for the Task returned by <paramref name="action"/>.</returns>
	/// <param name="cancelToken">Token for cancellation of all concurrent tasks</param>
	public static Task Run( Func<CancellationToken, Task> action, CancellationToken cancelToken = default )
	{
		string stackTrace = EnvHelper.GetStackTrace();
		return Task.Run(
			async () =>
			{
				try
				{
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
}
