using System.Diagnostics;

namespace Erlin.Lib.Common;

/// <summary>
///    Simple helper class for measuring time spans
/// </summary>
public class StopWatch
{
	private long _start;

	/// <summary>
	///    Ctor
	/// </summary>
	public StopWatch()
	{
		Reset();
	}

	/// <summary>
	///    Resets this watch
	/// </summary>
	public void Reset()
	{
		_start = Stopwatch.GetTimestamp();
	}

	/// <summary>
	///    Get elapsed time from last reset
	/// </summary>
	public TimeSpan GetElapsed()
	{
		return Stopwatch.GetElapsedTime( _start );
	}
}
