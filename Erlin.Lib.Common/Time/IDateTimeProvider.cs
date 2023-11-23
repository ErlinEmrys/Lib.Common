namespace Erlin.Lib.Common.Time;

/// <summary>
///    Interface for providing date and time
/// </summary>
public interface IDateTimeProvider
{
	/// <summary>
	///    Full formatted DateTime (2017.05.30 23:58:33)
	/// </summary>
	public const string FORMAT_FULL = "yyyy.MM.dd HH:mm:ss";

	/// <summary>
	///    Full formatted DateTime with ticks (2017.05.30 23:58:33:8798)
	/// </summary>
	public const string FORMAT_FULL_TICKS = "yyyy.MM.dd HH:mm:ss:ffff";

	/// <summary>
	///    Full formatted Date (2017.05.30)
	/// </summary>
	public const string FORMAT_DATE = "yyyy.MM.dd";

	/// <summary>
	///    Time formatted without ticks (23:58:33)
	/// </summary>
	public const string FORMAT_TIME = "HH:mm:ss";

	/// <summary>
	///    Full formatted Time (23:58:33:4478)
	/// </summary>
	public const string FORMAT_TIME_TICKS = "HH:mm:ss:ffff";

	/// <summary>
	///    1 millisecond
	/// </summary>
	public static readonly TimeSpan TS_MINUS_MILLISECONDS_001 = TimeSpan.FromMilliseconds( -1 );

	/// <summary>
	///    1 millisecond
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_001 = TimeSpan.FromMilliseconds( 1 );

	/// <summary>
	///    2 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_002 = TimeSpan.FromMilliseconds( 2 );

	/// <summary>
	///    5 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_005 = TimeSpan.FromMilliseconds( 5 );

	/// <summary>
	///    10 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_010 = TimeSpan.FromMilliseconds( 10 );

	/// <summary>
	///    20 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_020 = TimeSpan.FromMilliseconds( 20 );

	/// <summary>
	///    30 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_030 = TimeSpan.FromMilliseconds( 30 );

	/// <summary>
	///    50 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_050 = TimeSpan.FromMilliseconds( 50 );

	/// <summary>
	///    60 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_060 = TimeSpan.FromMilliseconds( 60 );

	/// <summary>
	///    100 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_100 = TimeSpan.FromMilliseconds( 100 );

	/// <summary>
	///    200 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_200 = TimeSpan.FromMilliseconds( 200 );

	/// <summary>
	///    500 milliseconds
	/// </summary>
	public static readonly TimeSpan TS_MILLISECONDS_500 = TimeSpan.FromMilliseconds( 500 );

	/// <summary>
	///    1 second
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_01 = TimeSpan.FromSeconds( 1 );

	/// <summary>
	///    2 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_02 = TimeSpan.FromSeconds( 2 );

	/// <summary>
	///    3 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_03 = TimeSpan.FromSeconds( 3 );

	/// <summary>
	///    4 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_04 = TimeSpan.FromSeconds( 4 );

	/// <summary>
	///    5 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_05 = TimeSpan.FromSeconds( 5 );

	/// <summary>
	///    10 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_10 = TimeSpan.FromSeconds( 10 );

	/// <summary>
	///    20 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_20 = TimeSpan.FromSeconds( 20 );

	/// <summary>
	///    30 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_30 = TimeSpan.FromSeconds( 30 );

	/// <summary>
	///    60 seconds
	/// </summary>
	public static readonly TimeSpan TS_SECONDS_60 = TimeSpan.FromSeconds( 60 );

	/// <summary>
	///    1 minute
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_01 = TimeSpan.FromMinutes( 1 );

	/// <summary>
	///    2 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_02 = TimeSpan.FromMinutes( 2 );

	/// <summary>
	///    5 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_05 = TimeSpan.FromMinutes( 5 );

	/// <summary>
	///    10 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_10 = TimeSpan.FromMinutes( 10 );

	/// <summary>
	///    20 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_20 = TimeSpan.FromMinutes( 20 );

	/// <summary>
	///    30 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_30 = TimeSpan.FromMinutes( 30 );

	/// <summary>
	///    60 minutes
	/// </summary>
	public static readonly TimeSpan TS_MINUTES_60 = TimeSpan.FromMinutes( 60 );

	/// <summary>
	///    1 hour
	/// </summary>
	public static readonly TimeSpan TS_HOURS_01 = TimeSpan.FromHours( 1 );

	/// <summary>
	///    2 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_02 = TimeSpan.FromHours( 2 );

	/// <summary>
	///    4 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_04 = TimeSpan.FromHours( 4 );

	/// <summary>
	///    5 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_05 = TimeSpan.FromHours( 5 );

	/// <summary>
	///    6 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_06 = TimeSpan.FromHours( 6 );

	/// <summary>
	///    12 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_12 = TimeSpan.FromHours( 12 );

	/// <summary>
	///    24 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_24 = TimeSpan.FromHours( 24 );

	/// <summary>
	///    48 hours
	/// </summary>
	public static readonly TimeSpan TS_HOURS_48 = TimeSpan.FromHours( 48 );

	/// <summary>
	///    One week
	/// </summary>
	public static readonly TimeSpan TS_WEEK = TimeSpan.FromDays( 7 );

	/// <summary>
	///    Returns current date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	DateTime Now { get; }

	/// <summary>
	///    Returns current UTC date and time
	/// </summary>
	/// <returns>Current date and time</returns>
	DateTime UtcNow { get; }
}
