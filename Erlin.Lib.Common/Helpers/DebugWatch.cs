namespace Erlin.Lib.Common;

/// <summary>
///    Simple debug class for measuring time
/// </summary>
public class DebugWatch : IDisposable
{
	/// <summary>
	///    Associated message
	/// </summary>
	public string Message { get; }

	/// <summary>
	///    Start time
	/// </summary>
	public DateTime Start { get; }

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Associated message</param>
	public DebugWatch( string message )
	{
		Start = EnvHelper.DateTime.UtcNow;
		Message = message;
	}

	/// <summary>
	///    Stops the watch and log info
	/// </summary>
	public void Dispose()
	{
		TimeSpan duration = EnvHelper.DateTime.UtcNow - Start;
		Log.Dbg( "{Message} [{Duration}]", Message, duration );
	}
}
