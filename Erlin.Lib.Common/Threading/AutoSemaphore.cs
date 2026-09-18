namespace Erlin.Lib.Common.Threading;

/// <summary>
///    Provides a disposable wrapper around <see cref="SemaphoreSlim"/> that automatically releases
///    the semaphore when the acquired lock handle is disposed.
/// </summary>
/// <remarks>
///    <para>
///       This type is useful for writing semaphore-based critical sections with <c>using</c> or
///       <c>await using</c>-style patterns, ensuring that <see cref="SemaphoreSlim.Release()"/> is called
///       even when an exception is thrown inside the protected section.
///    </para>
///    <para>
///       The object returned from <see cref="Wait()"/> or <see cref="WaitAsync()"/> represents a single
///       acquired semaphore slot. Disposing that object releases exactly one slot.
///    </para>
///    <para>
///       Instances of this class own the wrapped <see cref="SemaphoreSlim"/> when constructed using
///       <see cref="AutoSemaphore(int)"/> or <see cref="AutoSemaphore(int, int)"/>.
///       When constructed with an existing <see cref="SemaphoreSlim"/>, ownership can be controlled with
///       the constructor parameter.
///    </para>
/// </remarks>
/// <example>
///    <code>
/// private readonly AutoSemaphore _lock = new(1, 1);
///
/// public async Task DoWorkAsync()
/// {
///     using IDisposable lease = await _lock.WaitAsync();
///
///     // Protected section.
/// }
/// </code>
/// </example>
public sealed class AutoSemaphore : IDisposable
{
	private readonly SemaphoreSlim _semaphore;

	private bool _disposed;

	/// <summary>
	///    Gets the number of remaining threads that can enter the semaphore.
	/// </summary>
	public int CurrentCount
	{
		get { return _semaphore.CurrentCount; }
	}

	/// <summary>
	///    Gets a <see cref="WaitHandle"/> that can be used to wait on the semaphore.
	/// </summary>
	public WaitHandle AvailableWaitHandle
	{
		get
		{
			ThrowIfDisposed();

			return _semaphore.AvailableWaitHandle;
		}
	}

	/// <summary>
	///    Initializes a new instance of the <see cref="AutoSemaphore"/> class with the
	///    specified initial count.
	/// </summary>
	/// <param name="initialCount">
	///    The initial number of requests for the semaphore that can be granted concurrently.
	/// </param>
	public AutoSemaphore( int initialCount )
		: this( new SemaphoreSlim( initialCount ) )
	{
	}

	/// <summary>
	///    Initializes a new instance of the <see cref="AutoSemaphore"/> class with the
	///    specified initial and maximum counts.
	/// </summary>
	/// <param name="initialCount">
	///    The initial number of requests for the semaphore that can be granted concurrently.
	/// </param>
	/// <param name="maxCount">
	///    The maximum number of requests for the semaphore that can be granted concurrently.
	/// </param>
	public AutoSemaphore( int initialCount, int maxCount )
		: this( new SemaphoreSlim( initialCount, maxCount ) )
	{
	}

	/// <summary>
	///    Initializes a new instance of the <see cref="AutoSemaphore"/> class that wraps an
	///    existing <see cref="SemaphoreSlim"/> instance.
	/// </summary>
	/// <param name="semaphore">
	///    The semaphore instance to wrap.
	/// </param>
	/// <exception cref="ArgumentNullException">
	///    Thrown when <paramref name="semaphore"/> is <see langword="null"/>.
	/// </exception>
	private AutoSemaphore( SemaphoreSlim semaphore )
	{
		_semaphore = semaphore ?? throw new ArgumentNullException( nameof( semaphore ) );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered, and returns a disposable
	///    handle that releases the semaphore when disposed.
	/// </summary>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public IDisposable Wait()
	{
		ThrowIfDisposed();

		_semaphore.Wait();

		return new Releaser( this );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered while observing a
	///    <see cref="CancellationToken"/>, and returns a disposable handle that releases the semaphore
	///    when disposed.
	/// </summary>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="OperationCanceledException">
	///    Thrown when <paramref name="cancellationToken"/> is canceled before the semaphore is entered.
	/// </exception>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public IDisposable Wait( CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		_semaphore.Wait( cancellationToken );

		return new Releaser( this );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered or the specified timeout
	///    expires, and returns a disposable handle that releases the semaphore when disposed.
	/// </summary>
	/// <param name="timeout">
	///    The amount of time to wait for the semaphore.
	/// </param>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="timeout"/> expires.
	/// </exception>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public IDisposable Wait( TimeSpan timeout )
	{
		ThrowIfDisposed();

		if( !_semaphore.Wait( timeout ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered or the specified timeout expires,
	///    and returns a disposable handle that releases the semaphore when disposed.
	/// </summary>
	/// <param name="millisecondsTimeout">
	///    The number of milliseconds to wait, <see cref="Timeout.Infinite"/> to wait indefinitely,
	///    or <c>0</c> to test the state of the semaphore and return immediately.
	/// </param>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="millisecondsTimeout"/> expires.
	/// </exception>
	public IDisposable Wait( int millisecondsTimeout )
	{
		ThrowIfDisposed();

		if( !_semaphore.Wait( millisecondsTimeout ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered or the specified timeout expires,
	///    while observing a <see cref="CancellationToken"/>, and returns a disposable handle that releases
	///    the semaphore when disposed.
	/// </summary>
	/// <param name="millisecondsTimeout">
	///    The number of milliseconds to wait, <see cref="Timeout.Infinite"/> to wait indefinitely,
	///    or <c>0</c> to test the state of the semaphore and return immediately.
	/// </param>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="millisecondsTimeout"/> expires.
	/// </exception>
	public IDisposable Wait( int millisecondsTimeout, CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		if( !_semaphore.Wait( millisecondsTimeout, cancellationToken ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Blocks the current thread until the semaphore has been entered or the specified timeout expires,
	///    while observing a <see cref="CancellationToken"/>, and returns a disposable handle that releases
	///    the semaphore when disposed.
	/// </summary>
	/// <param name="timeout">
	///    The amount of time to wait for the semaphore.
	/// </param>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    An <see cref="IDisposable"/> handle representing the acquired semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="timeout"/> expires.
	/// </exception>
	public IDisposable Wait( TimeSpan timeout, CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		if( !_semaphore.Wait( timeout, cancellationToken ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore, and returns a disposable handle that releases
	///    the semaphore when disposed.
	/// </summary>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public async Task< IDisposable > WaitAsync()
	{
		ThrowIfDisposed();

		await _semaphore.WaitAsync();

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore while observing a <see cref="CancellationToken"/>,
	///    and returns a disposable handle that releases the semaphore when disposed.
	/// </summary>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="OperationCanceledException">
	///    Thrown when <paramref name="cancellationToken"/> is canceled before the semaphore is entered.
	/// </exception>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public async Task< IDisposable > WaitAsync( CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		await _semaphore.WaitAsync( cancellationToken );

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore until the specified timeout expires, and returns a
	///    disposable handle that releases the semaphore when disposed.
	/// </summary>
	/// <param name="timeout">
	///    The amount of time to wait for the semaphore.
	/// </param>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="timeout"/> expires.
	/// </exception>
	/// <exception cref="ObjectDisposedException">
	///    Thrown when this wrapper or the underlying semaphore has been disposed.
	/// </exception>
	public async Task< IDisposable > WaitAsync( TimeSpan timeout )
	{
		ThrowIfDisposed();

		if( !await _semaphore.WaitAsync( timeout ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore until the specified timeout expires, and returns a
	///    disposable handle that releases the semaphore when disposed.
	/// </summary>
	/// <param name="millisecondsTimeout">
	///    The number of milliseconds to wait, <see cref="Timeout.Infinite"/> to wait indefinitely,
	///    or <c>0</c> to test the state of the semaphore and return immediately.
	/// </param>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="millisecondsTimeout"/> expires.
	/// </exception>
	public async Task< IDisposable > WaitAsync( int millisecondsTimeout )
	{
		ThrowIfDisposed();

		if( !await _semaphore.WaitAsync( millisecondsTimeout ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore until the specified timeout expires, while observing
	///    a <see cref="CancellationToken"/>, and returns a disposable handle that releases the semaphore
	///    when disposed.
	/// </summary>
	/// <param name="millisecondsTimeout">
	///    The number of milliseconds to wait, <see cref="Timeout.Infinite"/> to wait indefinitely,
	///    or <c>0</c> to test the state of the semaphore and return immediately.
	/// </param>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="millisecondsTimeout"/> expires.
	/// </exception>
	public async Task< IDisposable > WaitAsync( int millisecondsTimeout, CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		if( !await _semaphore.WaitAsync( millisecondsTimeout, cancellationToken ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Asynchronously waits to enter the semaphore until the specified timeout expires, while observing
	///    a <see cref="CancellationToken"/>, and returns a disposable handle that releases the semaphore
	///    when disposed.
	/// </summary>
	/// <param name="timeout">
	///    The amount of time to wait for the semaphore.
	/// </param>
	/// <param name="cancellationToken">
	///    The cancellation token to observe.
	/// </param>
	/// <returns>
	///    A task that completes with an <see cref="IDisposable"/> handle representing the acquired
	///    semaphore slot.
	/// </returns>
	/// <exception cref="TimeoutException">
	///    Thrown when the semaphore cannot be entered before <paramref name="timeout"/> expires.
	/// </exception>
	public async Task< IDisposable > WaitAsync( TimeSpan timeout, CancellationToken cancellationToken )
	{
		ThrowIfDisposed();

		if( !await _semaphore.WaitAsync( timeout, cancellationToken ) )
		{
			throw new TimeoutException( "The semaphore could not be entered before the timeout expired." );
		}

		return new Releaser( this );
	}

	/// <summary>
	///    Releases the semaphore once.
	/// </summary>
	/// <returns>
	///    The previous count of the semaphore.
	/// </returns>
	public int Release()
	{
		ThrowIfDisposed();

		return _semaphore.Release();
	}

	/// <summary>
	///    Releases the semaphore a specified number of times.
	/// </summary>
	/// <param name="releaseCount">
	///    The number of times to release the semaphore.
	/// </param>
	/// <returns>
	///    The previous count of the semaphore.
	/// </returns>
	public int Release( int releaseCount )
	{
		ThrowIfDisposed();

		return _semaphore.Release( releaseCount );
	}

	/// <summary>
	///    Releases the <see cref="SemaphoreSlim"/> resources owned by this wrapper.
	/// </summary>
	/// <remarks>
	///    If this wrapper was constructed with an existing semaphore and ownership was not requested,
	///    the underlying semaphore is not disposed.
	/// </remarks>
	public void Dispose()
	{
		if( _disposed )
		{
			return;
		}

		_disposed = true;
		_semaphore.Dispose();
	}

	private void ThrowIfDisposed()
	{
		ObjectDisposedException.ThrowIf( _disposed, this );
	}

	private sealed class Releaser
	(
		AutoSemaphore semaphore
	) : IDisposable
	{
		private AutoSemaphore? _semaphore = semaphore;

		public void Dispose()
		{
			AutoSemaphore? semaphore = Interlocked.Exchange( ref _semaphore, null );

			semaphore?.Release();
		}
	}
}
