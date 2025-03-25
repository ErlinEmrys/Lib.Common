using Serilog.Core;
using Serilog.Events;

namespace Erlin.Lib.Common;

/// <summary>
///    No logging implementation of logger
/// </summary>
public class EmptyLog : ILog
{
#region ForContext

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
	public ILog ForContext( string propertyName, object? value, bool destructureObjects = false )
	{
		return this;
	}

#endregion

#region Verbose

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Vrb( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Vrb( bool condition, string messageTemplate, params object?[] values )
	{
	}

#endregion

#region Debug

	/// <summary>
	///    Log custom debug message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Dbg( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom debug message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Dbg( bool condition, string messageTemplate, params object?[] values )
	{
	}

#endregion

#region Info

	/// <summary>
	///    Log custom info message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Inf( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom info message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Inf( bool condition, string messageTemplate, params object?[] values )
	{
	}

#endregion

#region Warning

	/// <summary>
	///    Log custom warning message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom warning message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( bool condition, string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

#endregion

#region Error

	/// <summary>
	///    Log custom error message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom error message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( bool condition, string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

#endregion

#region Fatal

	/// <summary>
	///    Log custom fatal message
	/// </summary>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom fatal message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( bool condition, string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( bool condition, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

#endregion

#region Any

	/// <summary>
	///    Log custom message with selected level
	/// </summary>
	/// <param name="level">Message event level</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Any( LogEventLevel level, string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log custom verbose message
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="level">Message event level</param>
	/// <param name="messageTemplate">Message to log</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Any( bool condition, LogEventLevel level, string messageTemplate, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="level">Message event level</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Any(
		LogEventLevel level, Exception? ex, string? messageTemplate = null, params object?[] values )
	{
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="level">Message event level</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Any(
		bool condition, LogEventLevel level, Exception? ex, string? messageTemplate = null,
		params object?[] values )
	{
	}

#endregion
}
