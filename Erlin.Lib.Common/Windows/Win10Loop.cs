using System.Runtime.InteropServices;

namespace Erlin.Lib.Common.Windows;

/// <summary>
///    Main program loop for WIN 10 - keep PC awake, monitor awake and do GC
/// </summary>
public partial class Win10Loop : IDisposable
{
	private readonly Timer _timer;

	/// <summary>
	///    Current loop
	/// </summary>
	public static Win10Loop? Instance { get; private set; }

	/// <summary>
	///    Loop will keep this PC awake
	/// </summary>
	public bool KeepPcAwake { get; set; }

	/// <summary>
	///    Loop will keep this PC monitor awake
	/// </summary>
	public bool KeepMonitorAwake { get; set; }

	/// <summary>
	///    Do automatic Garbage collection
	/// </summary>
	public bool DoGarbage { get; set; }

	/// <summary>
	///    Ctor
	/// </summary>
	public Win10Loop( bool keepPcAwake, bool keepMonitorAwake, bool doGarbage )
	{
		if( Win10Loop.Instance != null )
		{
			throw new InvalidOperationException( "Second Win10Loop object at same moment!" );
		}

		Win10Loop.Instance = this;

		KeepMonitorAwake = keepMonitorAwake;
		KeepPcAwake = keepPcAwake;
		DoGarbage = doGarbage;

		_timer = new Timer(
			Win10Loop.OnTimerElapsed, this, TimeSpan.FromMinutes( 1 ), TimeSpan.FromMinutes( 1 ) );
	}

	/// <summary>
	///    Dispose all resources
	/// </summary>
	public void Dispose()
	{
		_timer.Dispose();
		Win10Loop.Instance = null;
	}

	/// <summary>
	///    Action when timer elapsed
	/// </summary>
	/// <param name="state">Timer state object</param>
	private static void OnTimerElapsed( object? state )
	{
		Log.Dbg( "Win10Loop.Tick" );

		try
		{
			if( state is not Win10Loop loop )
			{
				throw new InvalidOperationException();
			}

			if( loop.DoGarbage )
			{
				EnvHelper.CallGarbageCollector();
			}

			if( loop.KeepMonitorAwake || loop.KeepPcAwake )
			{
				ExecutionState flags = ExecutionState.ES_SYSTEM_REQUIRED;
				if( loop.KeepMonitorAwake )
				{
					flags |= ExecutionState.ES_DISPLAY_REQUIRED;
				}

				_ = Win10Loop.SetThreadExecutionState( flags );
			}
		}
		catch( Exception ex )
		{
			Log.Err( ex, "Win10Loop.Tick" );
		}
	}

	/// <summary>
	///    Tells the system, that this program is working and pc should not go into sleep, or turn off
	///    monitor
	/// </summary>
	/// <param name="esFlags">Flags of options</param>
	/// <returns>Result state</returns>
	[LibraryImport( "kernel32.dll", SetLastError = true )]
	private static partial ExecutionState SetThreadExecutionState( ExecutionState esFlags );

	/// <summary>
	///    P/Invoke states
	/// </summary>
	[Flags]
	private enum ExecutionState : uint
	{
		//ES_CONTINUOUS = 0x80000000,
		/// <summary>
		///    System is required
		/// </summary>
		ES_SYSTEM_REQUIRED = 0x00000001,

		/// <summary>
		///    Display is required
		/// </summary>
		ES_DISPLAY_REQUIRED = 0x00000002,

		//ES_AWAY_MODE_REQUIRED = 0x00000040,
	}
}
