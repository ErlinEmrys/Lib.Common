using System.Diagnostics;
using System.Runtime;

using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common;

/// <summary>
///    Helper class for environment variables
/// </summary>
public static class EnvHelper
{
	/// <summary>
	///    All variants of standard line breakers
	/// </summary>
	public static string[] NewLineBreakers { get; } =
	{
		"\r\n", "\n", "\r"
	};

	/// <summary>
	///    Provider of date and time
	/// </summary>
	public static IDateTimeProvider DateTime { get; set; } = new SystemDateTimeProvider();

	/// <summary>
	///    Call .net garbage collector
	/// </summary>
	public static void CallGarbageCollector()
	{
		GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}

	/// <summary>
	///    Call to power off PC, call before program exit
	/// </summary>
	public static void PowerOffPc()
	{
		Log.Wrn( "Shutting down PC in 3 seconds!" );
		Process.Start( "shutdown", "/s /t 3" );
	}

	/// <summary>
	///    Returns current stack trace (without this method call)
	/// </summary>
	/// <returns>Current stack trace</returns>
	public static string GetStackTrace()
	{
		StackTrace trace = new( 2, true );
		return trace.ToString();
	}
}
