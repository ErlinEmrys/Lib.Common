using Serilog.Core;
using Serilog.Events;

namespace Erlin.Lib.Common;

/// <summary>
///    Enricher adding full exception description
/// </summary>
public class ExceptionLogEnricher : ILogEventEnricher
{
	public const string PROP_FULL_EXCEPTION = "FullException";

	public void Enrich( LogEvent logEvent, ILogEventPropertyFactory propertyFactory )
	{
		if( logEvent.Exception != null )
		{
			logEvent.AddPropertyIfAbsent(
				propertyFactory.CreateProperty( PROP_FULL_EXCEPTION, logEvent.Exception.ToJson() ) );
		}
	}
}
