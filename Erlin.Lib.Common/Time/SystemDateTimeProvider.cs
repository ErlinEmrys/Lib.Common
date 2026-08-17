namespace Erlin.Lib.Common.Time;

/// <summary>
///    Provider of date and time based on system time
/// </summary>
public class SystemDateTimeProvider : IDateTimeProvider
{
	/// <summary>
	///    Returns the provider object for the time
	/// </summary>
	public TimeProvider Provider { get; } = TimeProvider.System;

	/// <summary>
	///    Returns current date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	public DateTime Now
	{
		get { return Provider.GetLocalNow().LocalDateTime; }
	}

	/// <summary>
	///    Returns current UTC date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	public DateTime UtcNow
	{
		get { return Provider.GetUtcNow().UtcDateTime; }
	}
}
