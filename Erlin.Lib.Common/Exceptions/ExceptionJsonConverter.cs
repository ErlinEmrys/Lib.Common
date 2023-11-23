using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

using Erlin.Lib.Common.Threading;

using Newtonsoft.Json;

namespace Erlin.Lib.Common.Exceptions;

/// <summary>
///    Exception object to JSON string converter
/// </summary>
public class ExceptionJsonConverter : JsonConverter<Exception>
{
	/// <summary>
	///    Exception properties, that should be ignored from reflection reading
	/// </summary>
	private static HashSet<string> IgnoredProps { get; } = new()
	{
		"HasBeenThrown",
		"Source",
		"TargetSite",
		"Message",
		"InnerException",
		"StackTrace",
		"Data",
		"WaitHandle",
	};

	/// <summary>Writes the JSON representation of the object.</summary>
	/// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
	/// <param name="value">The value.</param>
	/// <param name="serializer">The calling serializer.</param>
	public override void WriteJson( JsonWriter writer, Exception? value, JsonSerializer serializer )
	{
		if( value is null )
		{
			return;
		}

		writer.WriteStartObject();

		if( value.InnerException is not null )
		{
			writer.WritePropertyName( "InnerException" );
			serializer.Serialize( writer, value.InnerException, value.InnerException.GetType() );
		}

		Type exceptionType = value.GetType();
		writer.WritePropertyName( "ExceptionType" );
		writer.WriteValue( exceptionType.FullName );

		writer.WritePropertyName( "Message" );
		writer.WriteValue( value.Message );

		writer.WritePropertyName( "StackTrace" );
		StringBuilder stack = new();
		stack.Append( value.StackTrace );

		IDictionary data = value.Data;
		if( value.Data.Contains( ParallelHelper.STACKTRACE_TASK ) )
		{
			_ = stack.AppendLine();
			_ = stack.Append( value.Data[ ParallelHelper.STACKTRACE_TASK ] as string );

			//Clone the data dic
			data = new ListDictionary();
			foreach( object fKey in value.Data.Keys )
			{
				if( ParallelHelper.STACKTRACE_TASK != fKey as string )
				{
					data[ fKey ] = value.Data[ fKey ];
				}
			}
		}

		string[] stackArr = stack.ToString()
										.Split(
												Environment.NewLine,
												StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries );

		serializer.Serialize( writer, stackArr, stackArr.GetType() );

		writer.WritePropertyName( "Data" );
		serializer.Serialize( writer, data, data.GetType() );

		List<PropertyInfo> properties = exceptionType.GetProperties().ToList();

		foreach( PropertyInfo? fProperty in properties )
		{
			if( ExceptionJsonConverter.IgnoredProps.Contains( fProperty.Name ) )
			{
				continue;
			}

			object? propertyValue = fProperty.GetValue( value, null );
			if( ( serializer.NullValueHandling == NullValueHandling.Ignore )
				&& propertyValue is null )
			{
				continue;
			}

			writer.WritePropertyName( fProperty.Name, true );
			serializer.Serialize( writer, propertyValue, fProperty.PropertyType );
		}

		writer.WriteEndObject();
	}

	/// <summary>Reads the JSON representation of the object.</summary>
	/// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
	/// <param name="objectType">Type of the object.</param>
	/// <param name="existingValue">
	///    The existing value of object being read. If there is no existing value
	///    then <c>null</c> will be used.
	/// </param>
	/// <param name="hasExistingValue">The existing value has a value.</param>
	/// <param name="serializer">The calling serializer.</param>
	/// <returns>The object value.</returns>
	public override Exception ReadJson(
		JsonReader reader, Type objectType, Exception? existingValue, bool hasExistingValue,
		JsonSerializer serializer )
	{
		throw new NotImplementedException();
	}
}
