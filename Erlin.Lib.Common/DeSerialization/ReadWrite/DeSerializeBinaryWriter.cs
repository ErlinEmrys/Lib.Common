using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Binary writer of primitives
/// </summary>
public class DeSerializeBinaryWriter : IDeSerializeWriter
{
	/// <summary>
	///    Internal writer
	/// </summary>
	private BinaryWriter Writer { get; set; }

	/// <summary>
	///    Sign, that this writer actually writes data
	/// </summary>
	public bool ImplementsWrite
	{
		get { return true; }
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="stream">Output stream</param>
	public DeSerializeBinaryWriter( Stream stream )
	{
		SwitchStream( stream );
	}

	/// <summary>
	///    Switches underlying stream to write
	/// </summary>
	/// <param name="stream"></param>
	[MemberNotNull( nameof( DeSerializeBinaryWriter.Writer ) )]
	public void SwitchStream( Stream stream )
	{
		Dispose();
		Writer = new BinaryWriter( stream, Encoding.UTF8, true );
	}

	/// <summary>
	///    Release all resources
	/// </summary>
	public void Dispose()
	{
		Flush();

		// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
		Writer?.Dispose();
	}

	/// <summary>
	///    Flush all in-memory data to stream
	/// </summary>
	public void Flush()
	{
		// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
		Writer?.Flush();
	}

	/// <summary>
	///    Write existence of next value
	/// </summary>
	/// <param name="fieldName">Field name</param>
	/// <param name="hasValue">Value exist</param>
	private void WriteValueExist( string? fieldName, bool hasValue )
	{
		WriteBool( fieldName, hasValue );
	}

	/// <summary>
	///    Writes nullable array of data
	/// </summary>
	/// <param name="fieldName"></param>
	/// <param name="value"></param>
	/// <param name="serializer"></param>
	/// <typeparam name="T"></typeparam>
	public void WriteArrN<T>( string? fieldName, T[]? value, Action<string, T> serializer )
	{
		int length = value?.Length ?? DeSerializeConstants.FLAG_LENGTH_NULL;
		Writer.Write( length );
		if( value != null )
		{
			for( int i = 0; i < length; i++ )
			{
				serializer( $"{fieldName}#{i}#", value[ i ] );
			}
		}
	}

	public void WriteBool( string? fieldName, bool value )
	{
		Writer.Write( value );
	}

	public void WriteBoolN( string? fieldName, bool? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteBool( fieldName, value.Value );
		}
	}

	public void WriteSByte( string? fieldName, sbyte value )
	{
		Writer.Write( value );
	}

	public void WriteSByteN( string? fieldName, sbyte? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteSByte( fieldName, value.Value );
		}
	}

	public void WriteSByteArr( string? fieldName, sbyte[] value )
	{
		WriteSByteArrN( fieldName, value );
	}

	public void WriteSByteArrN( string? fieldName, sbyte[]? value )
	{
		WriteArrN( fieldName, value, WriteSByte );
	}

	public void WriteSByteNArr( string? fieldName, sbyte?[] value )
	{
		WriteSByteNArrN( fieldName, value );
	}

	public void WriteSByteNArrN( string? fieldName, sbyte?[]? value )
	{
		WriteArrN( fieldName, value, WriteSByteN );
	}

	public void WriteByte( string? fieldName, byte value )
	{
		Writer.Write( value );
	}

	public void WriteByteN( string? fieldName, byte? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteByte( fieldName, value.Value );
		}
	}

	public void WriteByteArr( string? fieldName, byte[] value )
	{
		WriteByteArrN( fieldName, value );
	}

	public void WriteByteArrN( string? fieldName, byte[]? value )
	{
		WriteArrN( fieldName, value, WriteByte );
	}

	public void WriteByteNArr( string? fieldName, byte?[] value )
	{
		WriteByteNArrN( fieldName, value );
	}

	public void WriteByteNArrN( string? fieldName, byte?[]? value )
	{
		WriteArrN( fieldName, value, WriteByteN );
	}

	public void WriteInt16( string? fieldName, short value )
	{
		Writer.Write( value );
	}

	public void WriteInt16N( string? fieldName, short? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteInt16( fieldName, value.Value );
		}
	}

	public void WriteInt16Arr( string? fieldName, short[] value )
	{
		WriteInt16ArrN( fieldName, value );
	}

	public void WriteInt16ArrN( string? fieldName, short[]? value )
	{
		WriteArrN( fieldName, value, WriteInt16 );
	}

	public void WriteInt16NArr( string? fieldName, short?[] value )
	{
		WriteInt16NArrN( fieldName, value );
	}

	public void WriteInt16NArrN( string? fieldName, short?[]? value )
	{
		WriteArrN( fieldName, value, WriteInt16N );
	}

	public void WriteUInt16( string? fieldName, ushort value )
	{
		Writer.Write( value );
	}

	public void WriteUInt16N( string? fieldName, ushort? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteUInt16( fieldName, value.Value );
		}
	}

	public void WriteUInt16Arr( string? fieldName, ushort[] value )
	{
		WriteUInt16ArrN( fieldName, value );
	}

	public void WriteUInt16ArrN( string? fieldName, ushort[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt16 );
	}

	public void WriteUInt16NArr( string? fieldName, ushort?[] value )
	{
		WriteUInt16NArrN( fieldName, value );
	}

	public void WriteUInt16NArrN( string? fieldName, ushort?[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt16N );
	}

	public void WriteInt32( string? fieldName, int value )
	{
		Writer.Write( value );
	}

	public void WriteInt32N( string? fieldName, int? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteInt32( fieldName, value.Value );
		}
	}

	public void WriteInt32Arr( string? fieldName, int[] value )
	{
		WriteInt32ArrN( fieldName, value );
	}

	public void WriteInt32ArrN( string? fieldName, int[]? value )
	{
		WriteArrN( fieldName, value, WriteInt32 );
	}

	public void WriteInt32NArr( string? fieldName, int?[] value )
	{
		WriteInt32NArrN( fieldName, value );
	}

	public void WriteInt32NArrN( string? fieldName, int?[]? value )
	{
		WriteArrN( fieldName, value, WriteInt32N );
	}

	public void WriteUInt32( string? fieldName, uint value )
	{
		Writer.Write( value );
	}

	public void WriteUInt32N( string? fieldName, uint? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteUInt32( fieldName, value.Value );
		}
	}

	public void WriteUInt32Arr( string? fieldName, uint[] value )
	{
		WriteUInt32ArrN( fieldName, value );
	}

	public void WriteUInt32ArrN( string? fieldName, uint[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt32 );
	}

	public void WriteUInt32NArr( string? fieldName, uint?[] value )
	{
		WriteUInt32NArrN( fieldName, value );
	}

	public void WriteUInt32NArrN( string? fieldName, uint?[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt32N );
	}

	public void WriteInt64( string? fieldName, long value )
	{
		Writer.Write( value );
	}

	public void WriteInt64N( string? fieldName, long? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteInt64( fieldName, value.Value );
		}
	}

	public void WriteInt64Arr( string? fieldName, long[] value )
	{
		WriteInt64ArrN( fieldName, value );
	}

	public void WriteInt64ArrN( string? fieldName, long[]? value )
	{
		WriteArrN( fieldName, value, WriteInt64 );
	}

	public void WriteInt64NArr( string? fieldName, long?[] value )
	{
		WriteInt64NArrN( fieldName, value );
	}

	public void WriteInt64NArrN( string? fieldName, long?[]? value )
	{
		WriteArrN( fieldName, value, WriteInt64N );
	}

	public void WriteUInt64( string? fieldName, ulong value )
	{
		Writer.Write( value );
	}

	public void WriteUInt64N( string? fieldName, ulong? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteUInt64( fieldName, value.Value );
		}
	}

	public void WriteUInt64Arr( string? fieldName, ulong[] value )
	{
		WriteUInt64ArrN( fieldName, value );
	}

	public void WriteUInt64ArrN( string? fieldName, ulong[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt64 );
	}

	public void WriteUInt64NArr( string? fieldName, ulong?[] value )
	{
		WriteUInt64NArrN( fieldName, value );
	}

	public void WriteUInt64NArrN( string? fieldName, ulong?[]? value )
	{
		WriteArrN( fieldName, value, WriteUInt64N );
	}

	public void WriteFloat( string? fieldName, float value )
	{
		Writer.Write( value );
	}

	public void WriteFloatN( string? fieldName, float? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteFloat( fieldName, value.Value );
		}
	}

	public void WriteFloatArr( string? fieldName, float[] value )
	{
		WriteFloatArrN( fieldName, value );
	}

	public void WriteFloatArrN( string? fieldName, float[]? value )
	{
		WriteArrN( fieldName, value, WriteFloat );
	}

	public void WriteFloatNArr( string? fieldName, float?[] value )
	{
		WriteFloatNArrN( fieldName, value );
	}

	public void WriteFloatNArrN( string? fieldName, float?[]? value )
	{
		WriteArrN( fieldName, value, WriteFloatN );
	}

	public void WriteDouble( string? fieldName, double value )
	{
		Writer.Write( value );
	}

	public void WriteDoubleN( string? fieldName, double? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteDouble( fieldName, value.Value );
		}
	}

	public void WriteDoubleArr( string? fieldName, double[] value )
	{
		WriteDoubleArrN( fieldName, value );
	}

	public void WriteDoubleArrN( string? fieldName, double[]? value )
	{
		WriteArrN( fieldName, value, WriteDouble );
	}

	public void WriteDoubleNArr( string? fieldName, double?[] value )
	{
		WriteDoubleNArrN( fieldName, value );
	}

	public void WriteDoubleNArrN( string? fieldName, double?[]? value )
	{
		WriteArrN( fieldName, value, WriteDoubleN );
	}

	public void WriteDecimal( string? fieldName, decimal value )
	{
		Writer.Write( value );
	}

	public void WriteDecimalN( string? fieldName, decimal? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteDecimal( fieldName, value.Value );
		}
	}

	public void WriteDecimalArr( string? fieldName, decimal[] value )
	{
		WriteDecimalArrN( fieldName, value );
	}

	public void WriteDecimalArrN( string? fieldName, decimal[]? value )
	{
		WriteArrN( fieldName, value, WriteDecimal );
	}

	public void WriteDecimalNArr( string? fieldName, decimal?[] value )
	{
		WriteDecimalNArrN( fieldName, value );
	}

	public void WriteDecimalNArrN( string? fieldName, decimal?[]? value )
	{
		WriteArrN( fieldName, value, WriteDecimalN );
	}

	public void WriteGuid( string? fieldName, Guid value )
	{
		WriteByteArr( fieldName, value.ToByteArray() );
	}

	public void WriteGuidN( string? fieldName, Guid? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteGuid( fieldName, value.Value );
		}
	}

	public void WriteDateTime( string? fieldName, DateTime value )
	{
		Writer.Write( value.Ticks );
		Writer.Write( ( byte )value.Kind );
	}

	public void WriteDateTimeN( string? fieldName, DateTime? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteDateTime( fieldName, value.Value );
		}
	}

	public void WriteDateTimeOffset( string? fieldName, DateTimeOffset value )
	{
		Writer.Write( value.Ticks );
		Writer.Write( value.Offset.Ticks );
	}

	public void WriteDateTimeOffsetN( string? fieldName, DateTimeOffset? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteDateTimeOffset( fieldName, value.Value );
		}
	}

	public void WriteTimeSpan( string? fieldName, TimeSpan value )
	{
		Writer.Write( value.Ticks );
	}

	public void WriteTimeSpanN( string? fieldName, TimeSpan? value )
	{
		WriteValueExist( fieldName, value.HasValue );
		if( value.HasValue )
		{
			WriteTimeSpan( fieldName, value.Value );
		}
	}

	public void WriteString( string? fieldName, string value )
	{
		WriteStringN( fieldName, value );
	}

	public void WriteStringN( string? fieldName, string? value )
	{
		switch( value )
		{
			case null:
				Writer.Write( DeSerializeConstants.FLAG_LENGTH_NULL );
				break;

			case "":
				Writer.Write( DeSerializeConstants.FLAG_STRING_EMPTY );
				break;

			default:
			{
				byte[] array = Encoding.UTF8.GetBytes( value );
				Writer.Write( array.Length );
				Writer.Write( array );
				break;
			}
		}
	}

	public void WriteStringArr( string? fieldName, string[] value )
	{
		WriteStringArrN( fieldName, value );
	}

	public void WriteStringArrN( string? fieldName, string[]? value )
	{
		WriteArrN( fieldName, value, WriteString );
	}

	public void WriteStringNArr( string? fieldName, string?[] value )
	{
		WriteStringNArrN( fieldName, value );
	}

	public void WriteStringNArrN( string? fieldName, string?[]? value )
	{
		WriteArrN( fieldName, value, WriteStringN );
	}

	public void WriteObjectEmpty( string? fieldName )
	{
		Writer.Write( DeSerializeConstants.TYPE_ID_OBJECT_NULL );
	}

	public void WriteObjectStart( string? fieldName, ushort shortTypeId )
	{
		Writer.Write( shortTypeId );
	}

	public void WriteObjectEnd()
	{
		Writer.Write( DeSerializeConstants.FLAG_OBJECT_END );
	}

	public void WriteCollectionStart( string? fieldName, int count, bool isPrimitive = false )
	{
		Writer.Write( count );
	}

	public void WriteCollectionEnd( bool isPrimitive = false )
	{
		Writer.Write( DeSerializeConstants.FLAG_COLLECTION_END );
	}
}
