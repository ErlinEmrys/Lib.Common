using System.Diagnostics.CodeAnalysis;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

using SLog = Serilog.Log;

namespace Erlin.Lib.Common;

#pragma warning disable CA2254 // Template should be a static expression

/// <summary>
///    Logging class
/// </summary>
[ SuppressMessage( "ReSharper", "TemplateIsNotCompileTimeConstantProblem" ) ]
public static class Log
{
	#region Defaults

	/// <summary>
	///    Default file path to log files
	/// </summary>
	public static string DefaultLogFilePath
	{
		get { return Path.Combine( AppContext.BaseDirectory, "Log", "Log_.txt" ); }
	}

	/// <summary>
	///    Default output template
	/// </summary>
	public static string DefaultOutputTemplate { get; } = $"[{{Level:u3}} {{Timestamp:HH:mm:ss}}] {{Message:lj}}{{NewLine}}{{{ExceptionLogEnricher.PROP_FULL_EXCEPTION}}}";

	/// <summary>
	///    Default console color theme for logging
	/// </summary>
	public static SystemConsoleTheme DefaultConsoleColorTheme { get; } = new( new Dictionary< ConsoleThemeStyle, SystemConsoleThemeStyle >
	{
		[ ConsoleThemeStyle.Text ] = new() { Foreground = ConsoleColor.White },
		[ ConsoleThemeStyle.SecondaryText ] = new() { Foreground = ConsoleColor.Gray },
		[ ConsoleThemeStyle.TertiaryText ] = new() { Foreground = ConsoleColor.DarkGray },
		[ ConsoleThemeStyle.Invalid ] = new() { Foreground = ConsoleColor.Yellow },
		[ ConsoleThemeStyle.Null ] = new() { Foreground = ConsoleColor.Blue },
		[ ConsoleThemeStyle.Name ] = new() { Foreground = ConsoleColor.Gray },
		[ ConsoleThemeStyle.String ] = new() { Foreground = ConsoleColor.Cyan },
		[ ConsoleThemeStyle.Number ] = new() { Foreground = ConsoleColor.Magenta },
		[ ConsoleThemeStyle.Boolean ] = new() { Foreground = ConsoleColor.Blue },
		[ ConsoleThemeStyle.Scalar ] = new() { Foreground = ConsoleColor.Green },
		[ ConsoleThemeStyle.LevelVerbose ] = new() { Foreground = ConsoleColor.Gray },
		[ ConsoleThemeStyle.LevelDebug ] = new() { Foreground = ConsoleColor.Blue },
		[ ConsoleThemeStyle.LevelInformation ] = new() { Foreground = ConsoleColor.DarkGreen },
		[ ConsoleThemeStyle.LevelWarning ] = new() { Foreground = ConsoleColor.DarkYellow },
		[ ConsoleThemeStyle.LevelError ] = new() { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
		[ ConsoleThemeStyle.LevelFatal ] = new() { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkRed }
	} );

	#endregion

	/// <summary>
	///    The globally-shared logger.
	/// </summary>
	public static ILog Logger { get; private set; } = new EmptyLog();

	/// <summary>
	/// </summary>
	/// <param name="underlyingLogger"></param>
	public static void Initialize( ILogger underlyingLogger )
	{
		SLog.Logger = underlyingLogger;
		Log.Logger = new UnderlyingLog( underlyingLogger );
	}

	/// <summary>
	///    Ends all logging
	/// </summary>
	public static void Dispose()
	{
		SLog.CloseAndFlush();
	}

	/// <summary>
	///    Ends all logging
	/// </summary>
	public static async Task DisposeAsync()
	{
		await SLog.CloseAndFlushAsync();
	}

	/// <summary>
	///    Create a logger that enriches log events with the specified property.
	/// </summary>
	/// <param name="propertyName">The name of the property. Must be non-empty.</param>
	/// <param name="value">The property value.</param>
	/// <param name="destructureObjects">
	///    If <see langword="true"/>, the value will be serialized as a structured
	///    object if possible; if <see langword="false"/>, the object will be recorded as a scalar or
	///    simple array.
	/// </param>
	/// <returns>A logger that will enrich log events as specified.</returns>
	public static ILog ForContext( string propertyName, object? value, bool destructureObjects = false )
	{
		return Log.Logger.ForContext( propertyName, value, destructureObjects );
	}

	#region Verbose

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Vrb( string messageTemplate, params object?[] values )
	{
		Log.Logger.Vrb( messageTemplate, values );
	}

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Vrb( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Vrb( condition, messageTemplate, values );
	}

	#endregion

	#region Debug

	/// <summary>
	///    Log custom debug message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Dbg( string messageTemplate, params object?[] values )
	{
		Log.Logger.Dbg( messageTemplate, values );
	}

	/// <summary>
	///    Log custom debug message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Dbg( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Dbg( condition, messageTemplate, values );
	}

	#endregion

	#region Info

	/// <summary>
	///    Log custom info message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Inf( string messageTemplate, params object?[] values )
	{
		Log.Logger.Inf( messageTemplate, values );
	}

	/// <summary>
	///    Log custom info message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Inf( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Inf( condition, messageTemplate, values );
	}

	#endregion

	#region Warning

	/// <summary>
	///    Log custom warning message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Wrn( string messageTemplate, params object?[] values )
	{
		Log.Logger.Wrn( messageTemplate, values );
	}

	/// <summary>
	///    Log custom warning message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Wrn( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Wrn( condition, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Wrn( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Wrn( ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Wrn( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Wrn( condition, ex, messageTemplate, values );
	}

	#endregion

	#region Error

	/// <summary>
	///    Log custom error message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Err( string messageTemplate, params object?[] values )
	{
		Log.Logger.Err( messageTemplate, values );
	}

	/// <summary>
	///    Log custom error message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Err( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Err( condition, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Err( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Err( ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Err( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Err( condition, ex, messageTemplate, values );
	}

	#endregion

	#region Fatal

	/// <summary>
	///    Log custom fatal message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Fatal( string messageTemplate, params object?[] values )
	{
		Log.Logger.Fatal( messageTemplate, values );
	}

	/// <summary>
	///    Log custom fatal message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Fatal( bool condition, string messageTemplate, params object?[] values )
	{
		Log.Logger.Fatal( condition, messageTemplate, values );
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Fatal( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Fatal( ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Fatal( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Fatal( condition, ex, messageTemplate, values );
	}

	#endregion

	#region Any

	/// <summary>
	///    Log custom message with selected level
	/// </summary>
	/// <param name="level">Message event level</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Any( LogEventLevel level, string messageTemplate, params object?[] values )
	{
		Log.Logger.Any( level, messageTemplate, values );
	}

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="level">Message event level</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Any( bool condition, LogEventLevel level, string messageTemplate, params object?[] values )
	{
		Log.Logger.Any( condition, level, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="level">Message event level</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Any( LogEventLevel level, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Any( level, ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="level">Message event level</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[ MessageTemplateFormatMethod( nameof( messageTemplate ) ) ]
	public static void Any( bool condition, LogEventLevel level, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
		Log.Logger.Any( condition, level, ex, messageTemplate, values );
	}

	#endregion
}
