using System.Diagnostics.CodeAnalysis;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Erlin.Lib.Common;

/// <summary>
///    Logger implementation that logs to underlying logger
/// </summary>
[SuppressMessage( "ReSharper", "TemplateIsNotCompileTimeConstantProblem" )]
public class UnderlyingLog
(
	ILogger underlyingLogger
) : ILog
{
	/// <summary>
	///    Underlying logger
	/// </summary>
	public ILogger Logger { get; } = underlyingLogger;

	/// <summary>
	///    Write to underlying logging system
	/// </summary>
	/// <param name="level">Event level</param>
	/// <param name="ex">Exception</param>
	/// <param name="messageTemplate">Message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	private void WriteToUnderlying(
		LogEventLevel level, Exception? ex, string? messageTemplate, params object?[]? values )
	{
		if( messageTemplate.IsEmpty() )
		{
			if( ex != null )
			{
				messageTemplate = string.Empty;
			}
			else
			{
				messageTemplate = "Logging of empty {Level}{NewLine}{Stack}";
				values = new object?[]
				{
					level, Environment.NewLine, EnvHelper.GetStackTrace()
				};

				level = LogEventLevel.Warning;
			}
		}

		Logger.Write( level, ex, messageTemplate, values );
	}

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
		return new UnderlyingLog( Logger.ForContext( propertyName, value, destructureObjects ) );
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
		WriteToUnderlying( LogEventLevel.Verbose, null, messageTemplate, values );
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
		if( condition )
		{
			Vrb( messageTemplate, values );
		}
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
		WriteToUnderlying( LogEventLevel.Debug, null, messageTemplate, values );
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
		if( condition )
		{
			Dbg( messageTemplate, values );
		}
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
		WriteToUnderlying( LogEventLevel.Information, null, messageTemplate, values );
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
		if( condition )
		{
			Inf( messageTemplate, values );
		}
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
		WriteToUnderlying( LogEventLevel.Warning, null, messageTemplate, values );
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
		if( condition )
		{
			Wrn( messageTemplate, values );
		}
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( Exception ex, string? messageTemplate = null, params object?[] values )
	{
		WriteToUnderlying( LogEventLevel.Warning, ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception as warning
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Wrn( bool condition, Exception ex, string? messageTemplate = null, params object?[] values )
	{
		if( condition )
		{
			Wrn( ex, messageTemplate, values );
		}
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
		WriteToUnderlying( LogEventLevel.Error, null, messageTemplate, values );
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
		if( condition )
		{
			Err( messageTemplate, values );
		}
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( Exception ex, string? messageTemplate = null, params object?[] values )
	{
		WriteToUnderlying( LogEventLevel.Error, ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Err( bool condition, Exception ex, string? messageTemplate = null, params object?[] values )
	{
		if( condition )
		{
			Err( ex, messageTemplate, values );
		}
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
		WriteToUnderlying( LogEventLevel.Fatal, null, messageTemplate, values );
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
		if( condition )
		{
			Fatal( messageTemplate, values );
		}
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( Exception ex, string? messageTemplate = null, params object?[] values )
	{
		WriteToUnderlying( LogEventLevel.Fatal, ex, messageTemplate, values );
	}

	/// <summary>
	///    Log any fatal exception
	/// </summary>
	/// <param name="condition">Dynamic condition if log this message</param>
	/// <param name="ex">Exception to log</param>
	/// <param name="messageTemplate">Additional message</param>
	/// <param name="values">Additional properties</param>
	[MessageTemplateFormatMethod( nameof( messageTemplate ) )]
	public void Fatal( bool condition, Exception ex, string? messageTemplate = null, params object?[] values )
	{
		if( condition )
		{
			Fatal( ex, messageTemplate, values );
		}
	}

#endregion
}
