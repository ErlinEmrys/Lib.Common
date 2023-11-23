namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Unusable writer of primitive data
/// </summary>
public class DeSerializeEmptyWriter : IDeSerializeWriter
{
	public void Dispose()
	{
	}

	public bool ImplementsWrite
	{
		get { return false; }
	}

	public void SwitchStream( Stream stream )
	{
	}

	public void Flush()
	{
	}

	public void WriteBool( string? fieldName, bool value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteBoolN( string? fieldName, bool? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByteNArrN( string? fieldName, sbyte?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByte( string? fieldName, byte value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByteN( string? fieldName, byte? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByte( string? fieldName, sbyte value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByteN( string? fieldName, sbyte? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByteArr( string? fieldName, byte[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByteArrN( string? fieldName, byte[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByteNArr( string? fieldName, byte?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteByteNArrN( string? fieldName, byte?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByteArr( string? fieldName, sbyte[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByteArrN( string? fieldName, sbyte[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteSByteNArr( string? fieldName, sbyte?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16NArrN( string? fieldName, short?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16( string? fieldName, ushort value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16N( string? fieldName, ushort? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16Arr( string? fieldName, ushort[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16ArrN( string? fieldName, ushort[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16NArr( string? fieldName, ushort?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt16NArrN( string? fieldName, ushort?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16( string? fieldName, short value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16N( string? fieldName, short? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16Arr( string? fieldName, short[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16ArrN( string? fieldName, short[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt16NArr( string? fieldName, short?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64ArrN( string? fieldName, ulong[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64NArr( string? fieldName, ulong?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64NArrN( string? fieldName, ulong?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloat( string? fieldName, float value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloatN( string? fieldName, float? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloatArr( string? fieldName, float[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloatArrN( string? fieldName, float[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloatNArr( string? fieldName, float?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteFloatNArrN( string? fieldName, float?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDouble( string? fieldName, double value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDoubleN( string? fieldName, double? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDoubleArr( string? fieldName, double[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDoubleArrN( string? fieldName, double[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDoubleNArr( string? fieldName, double?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDoubleNArrN( string? fieldName, double?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32( string? fieldName, int value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32N( string? fieldName, int? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32Arr( string? fieldName, int[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32ArrN( string? fieldName, int[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32NArr( string? fieldName, int?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt32NArrN( string? fieldName, int?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32( string? fieldName, uint value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32N( string? fieldName, uint? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32Arr( string? fieldName, uint[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32ArrN( string? fieldName, uint[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32NArr( string? fieldName, uint?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt32NArrN( string? fieldName, uint?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64( string? fieldName, long value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64N( string? fieldName, long? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64Arr( string? fieldName, long[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64ArrN( string? fieldName, long[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64NArr( string? fieldName, long?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteInt64NArrN( string? fieldName, long?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64( string? fieldName, ulong value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64N( string? fieldName, ulong? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteUInt64Arr( string? fieldName, ulong[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimal( string? fieldName, decimal value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimalN( string? fieldName, decimal? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimalArr( string? fieldName, decimal[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimalArrN( string? fieldName, decimal[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimalNArr( string? fieldName, decimal?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDecimalNArrN( string? fieldName, decimal?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteTimeSpanN( string? fieldName, TimeSpan? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteString( string? fieldName, string value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteStringN( string? fieldName, string? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteStringArr( string? fieldName, string[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteStringArrN( string? fieldName, string[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteStringNArr( string? fieldName, string?[] value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteStringNArrN( string? fieldName, string?[]? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteGuid( string? fieldName, Guid value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteGuidN( string? fieldName, Guid? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDateTime( string? fieldName, DateTime value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDateTimeN( string? fieldName, DateTime? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDateTimeOffset( string? fieldName, DateTimeOffset value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteDateTimeOffsetN( string? fieldName, DateTimeOffset? value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteTimeSpan( string? fieldName, TimeSpan value )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteObjectEmpty( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteObjectStart( string? fieldName, ushort shortTypeId )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteObjectEnd()
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteCollectionStart( string? fieldName, int count, bool isPrimitive = false )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}

	public void WriteCollectionEnd( bool isPrimitive = false )
	{
		throw new DeSerializeException( "Attempt to write during deserialization!" );
	}
}
