using Serilog.Events;

namespace Erlin.Lib.Common;

/// <summary>
///    Simple helper class for measuring time with logging
/// </summary>
public class LogWatch : StopWatch, IDisposable
{
	/// <summary>
	///    Level of logging message
	/// </summary>
	public LogEventLevel LogLevel { get; }

	/// <summary>
	///    Associated message
	/// </summary>
	public string Message { get; }

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Associated message</param>
	/// <param name="logLevel">Level of logging message</param>
	public LogWatch( string message, LogEventLevel logLevel = LogEventLevel.Debug )
	{
		Message = message;
		LogLevel = logLevel;
	}

	/// <summary>
	///    Stops the watch and write log
	/// </summary>
	public void Dispose()
	{
		Log.Any( LogLevel, "{Message} [{Duration}ms]", Message, GetElapsed().TotalMilliseconds );
	}
}
