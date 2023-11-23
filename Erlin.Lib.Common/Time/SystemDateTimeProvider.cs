namespace Erlin.Lib.Common.Time;

/// <summary>
///    Provider of date and time based on system time
/// </summary>
public class SystemDateTimeProvider : IDateTimeProvider
{
	/// <summary>
	///    Returns current date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	public DateTime Now { get { return DateTime.Now; } }

	/// <summary>
	///    Returns current UTC date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	public DateTime UtcNow { get { return DateTime.UtcNow; } }
}
