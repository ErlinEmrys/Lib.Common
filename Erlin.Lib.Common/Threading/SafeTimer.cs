using System.ComponentModel;
using System.Timers;

using Timer = System.Timers.Timer;

namespace Erlin.Lib.Common.Threading;

/// <summary>
///    Safe version of <see cref="System.Threading.Timer"/>
/// </summary>
public class SafeTimer : IDisposable
{
	private readonly Timer _timer = new();
	private ElapsedEventHandler? _elapsedHandler;
	private EventHandler? _disposedHandler;

	/// <summary>
	///    Name of the timer
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	///    Occurs when the interval elapses.
	/// </summary>
	public event ElapsedEventHandler Elapsed
	{
		add { _elapsedHandler += value; }
		remove { _elapsedHandler -= value; }
	}

	/// <summary>
	///    Occurs when the timer is disposed.
	/// </summary>
	public event EventHandler Disposed
	{
		add { _disposedHandler += value; }
		remove { _disposedHandler -= value; }
	}

	public SafeTimer( string name, TimeSpan interval, bool autoReset = false, ElapsedEventHandler? onElapsed = null, EventHandler? onDisposed = null ) : this( name )
	{
		_timer.Interval = interval.TotalMilliseconds;
		_timer.AutoReset = autoReset;

		if( onElapsed != null )
		{
			Elapsed += onElapsed;
		}

		if( onDisposed != null )
		{
			Disposed += onDisposed;
		}
	}

	public SafeTimer( string name )
	{
		Name = name;

		_timer.Elapsed += OnElapsed;
		_timer.Disposed += OnDisposed;
	}

	/// <summary>
	///    Event handler for original <see cref="Elapsed"/> event
	/// </summary>
	private void OnElapsed( object? sender, ElapsedEventArgs args )
	{
		try
		{
			_elapsedHandler?.Invoke( sender, args );
		}
		catch( Exception ex )
		{
			Log.Err( ex, "SafeTimer[{Name}]: Elapsed event failed!", Name );
			throw;
		}
	}

	/// <summary>
	///    EventHandler for original <see cref="Disposed"/> event
	/// </summary>
	private void OnDisposed( object? sender, EventArgs e )
	{
		try
		{
			_disposedHandler?.Invoke( sender, e );
		}
		catch( Exception ex )
		{
			Log.Err( ex, "SafeTimer[{Name}]: Disposed event failed!", Name );
			throw;
		}
	}

	/// <summary>
	///    Resets timer
	/// </summary>
	public void Reset()
	{
		Stop();
		Start();
	}

	#region Proxy members

	/// <summary>
	///    Gets or sets whether the timer should raise the <see cref="Elapsed"/> event each time the interval elapses, or only once.
	/// </summary>
	public bool AutoReset
	{
		get { return _timer.AutoReset; }
		set { _timer.AutoReset = value; }
	}

	/// <summary>
	///    Gets or sets whether the timer is enabled.
	/// </summary>
	public bool Enabled
	{
		get { return _timer.Enabled; }
		set { _timer.Enabled = value; }
	}

	/// <summary>
	///    Gets or sets the interval, in milliseconds, at which the <see cref="Elapsed"/> event is raised.
	/// </summary>
	public double Interval
	{
		get { return _timer.Interval; }
		set { _timer.Interval = value; }
	}

	/// <summary>
	///    Gets or sets the site that binds this timer to its container.
	/// </summary>
	public ISite? Site
	{
		get { return _timer.Site; }
		set { _timer.Site = value; }
	}

	/// <summary>
	///    Gets or sets the object used to marshal event handler calls issued when an interval has elapsed.
	/// </summary>
	public ISynchronizeInvoke? SynchronizingObject
	{
		get { return _timer.SynchronizingObject; }
		set { _timer.SynchronizingObject = value; }
	}

	/// <summary>
	///    Gets the container that contains this timer.
	/// </summary>
	public IContainer? Container
	{
		get { return _timer.Container; }
	}

	/// <summary>
	///    Starts raising the <see cref="Elapsed"/> event by setting <see cref="Enabled"/> to <see langword="true"/>.
	/// </summary>
	public void Start()
	{
		_timer.Start();
	}

	/// <summary>
	///    Stops raising the <see cref="Elapsed"/> event by setting <see cref="Enabled"/> to <see langword="false"/>.
	/// </summary>
	public void Stop()
	{
		_timer.Stop();
	}

	/// <summary>
	///    Releases the resources used by the timer.
	/// </summary>
	public void Close()
	{
		_timer.Close();
	}

	/// <summary>
	///    Begins the initialization of the timer.
	/// </summary>
	public void BeginInit()
	{
		_timer.BeginInit();
	}

	/// <summary>
	///    Ends the initialization of the timer.
	/// </summary>
	public void EndInit()
	{
		_timer.EndInit();
	}

	/// <summary>
	///    Returns a string that represents the current timer.
	/// </summary>
	/// <returns>A string that represents the current timer.</returns>
	public override string ToString()
	{
		return _timer.ToString();
	}

	/// <summary>
	///    Releases all resources used by the timer.
	/// </summary>
	public void Dispose()
	{
		_timer.Dispose();
	}

	#endregion
}
