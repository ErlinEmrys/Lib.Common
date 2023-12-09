using System.Globalization;

using Newtonsoft.Json.Linq;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

public class DeSerializeJsonObjectWriter : IDeSerializeWriter
{
	/// <summary>
	///    Root JSON document
	/// </summary>
	public JObject RootDoc { get; } = new();

	/// <summary>
	///    Current JSON token
	/// </summary>
	private JContainer Current { get; set; }

	/// <summary>
	///    Saved Parents of current JSON node
	/// </summary>
	private Stack<JContainer> Parents { get; } = new();

	public DeSerializeJsonObjectWriter()
	{
		Current = RootDoc;
	}

	public void Dispose()
	{
	}

	public bool ImplementsWrite { get; } = true;

	public void SwitchStream( Stream stream )
	{
	}

	public void Flush()
	{
	}

	private void WriteValue( string? fieldName, object? value )
	{
		if( Current is JArray arr )
		{
			if( value is JToken )
			{
				arr.Add( value );
			}
			else
			{
				arr.Add( new JValue( value ) );
			}
		}
		else
		{
			if( fieldName.IsEmpty() )
			{
				throw new DeSerializeException( "Could not serialize to JSON with empty fieldName" );
			}

			Current.Add( new JProperty( fieldName, value ) );
		}
	}

	public void WriteBool( string? fieldName, bool value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteBoolN( string? fieldName, bool? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByte( string? fieldName, sbyte value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByteN( string? fieldName, sbyte? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByteArr( string? fieldName, sbyte[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByteArrN( string? fieldName, sbyte[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByteNArr( string? fieldName, sbyte?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteSByteNArrN( string? fieldName, sbyte?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByte( string? fieldName, byte value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByteN( string? fieldName, byte? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByteArr( string? fieldName, byte[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByteArrN( string? fieldName, byte[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByteNArr( string? fieldName, byte?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteByteNArrN( string? fieldName, byte?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16( string? fieldName, short value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16N( string? fieldName, short? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16Arr( string? fieldName, short[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16ArrN( string? fieldName, short[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16NArr( string? fieldName, short?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt16NArrN( string? fieldName, short?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16( string? fieldName, ushort value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16N( string? fieldName, ushort? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16Arr( string? fieldName, ushort[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16ArrN( string? fieldName, ushort[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16NArr( string? fieldName, ushort?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt16NArrN( string? fieldName, ushort?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32( string? fieldName, int value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32N( string? fieldName, int? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32Arr( string? fieldName, int[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32ArrN( string? fieldName, int[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32NArr( string? fieldName, int?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt32NArrN( string? fieldName, int?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32( string? fieldName, uint value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32N( string? fieldName, uint? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32Arr( string? fieldName, uint[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32ArrN( string? fieldName, uint[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32NArr( string? fieldName, uint?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt32NArrN( string? fieldName, uint?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64( string? fieldName, long value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64N( string? fieldName, long? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64Arr( string? fieldName, long[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64ArrN( string? fieldName, long[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64NArr( string? fieldName, long?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteInt64NArrN( string? fieldName, long?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64( string? fieldName, ulong value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64N( string? fieldName, ulong? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64Arr( string? fieldName, ulong[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64ArrN( string? fieldName, ulong[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64NArr( string? fieldName, ulong?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteUInt64NArrN( string? fieldName, ulong?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloat( string? fieldName, float value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloatN( string? fieldName, float? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloatArr( string? fieldName, float[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloatArrN( string? fieldName, float[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloatNArr( string? fieldName, float?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteFloatNArrN( string? fieldName, float?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDouble( string? fieldName, double value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDoubleN( string? fieldName, double? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDoubleArr( string? fieldName, double[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDoubleArrN( string? fieldName, double[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDoubleNArr( string? fieldName, double?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDoubleNArrN( string? fieldName, double?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimal( string? fieldName, decimal value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimalN( string? fieldName, decimal? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimalArr( string? fieldName, decimal[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimalArrN( string? fieldName, decimal[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimalNArr( string? fieldName, decimal?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteDecimalNArrN( string? fieldName, decimal?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteGuid( string? fieldName, Guid value )
	{
		WriteGuidN( fieldName, value );
	}

	public void WriteGuidN( string? fieldName, Guid? value )
	{
		WriteValue( fieldName, value?.ToString() );
	}

	public void WriteDateTime( string? fieldName, DateTime value )
	{
		WriteDateTimeN( fieldName, value );
	}

	public void WriteDateTimeN( string? fieldName, DateTime? value )
	{
		WriteValue( fieldName, value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteDateTimeOffset( string? fieldName, DateTimeOffset value )
	{
		WriteDateTimeOffsetN( fieldName, value );
	}

	public void WriteDateTimeOffsetN( string? fieldName, DateTimeOffset? value )
	{
		WriteValue( fieldName, value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteTimeSpan( string? fieldName, TimeSpan value )
	{
		WriteTimeSpanN( fieldName, value );
	}

	public void WriteTimeSpanN( string? fieldName, TimeSpan? value )
	{
		WriteValue( fieldName, value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteString( string? fieldName, string value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteStringN( string? fieldName, string? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteStringArr( string? fieldName, string[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteStringArrN( string? fieldName, string[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteStringNArr( string? fieldName, string?[] value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteStringNArrN( string? fieldName, string?[]? value )
	{
		WriteValue( fieldName, value );
	}

	public void WriteObjectEmpty( string? fieldName )
	{
		WriteValue( fieldName, null );
	}

	public void WriteObjectStart( string? fieldName, ushort shortTypeId )
	{
		JObject obj = new();
		WriteValue( fieldName, obj );

		Parents.Push( Current );
		Current = obj;

		WriteUInt16( DeSerializeConstants.FIELD_OBJECT_TYPE_ID, shortTypeId );
	}

	public void WriteObjectEnd()
	{
		Current = Parents.Pop();
	}

	public void WriteCollectionStart( string? fieldName, int count, bool isPrimitive = false )
	{
		JArray arr = [];
		WriteValue( fieldName, arr );

		Parents.Push( Current );
		Current = arr;
	}

	public void WriteCollectionEnd( bool isPrimitive = false )
	{
		Current = Parents.Pop();
	}
}
