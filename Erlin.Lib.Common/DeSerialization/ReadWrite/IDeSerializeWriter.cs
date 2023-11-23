namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Writer of primitive data
/// </summary>
public interface IDeSerializeWriter : IDisposable
{
	/// <summary>
	///    Indicates that this object implements writing
	/// </summary>
	bool ImplementsWrite { get; }

	/// <summary>
	///    Switches underlying stream to write
	/// </summary>
	/// <param name="stream"></param>
	void SwitchStream( Stream stream );

	/// <summary>
	///    Forces all in-memory written values to by flushed to desired destination
	/// </summary>
	void Flush();

	void WriteBool( string? fieldName, bool value );

	void WriteBoolN( string? fieldName, bool? value );

	void WriteSByte( string? fieldName, sbyte value );

	void WriteSByteN( string? fieldName, sbyte? value );

	void WriteSByteArr( string? fieldName, sbyte[] value );

	void WriteSByteArrN( string? fieldName, sbyte[]? value );

	void WriteSByteNArr( string? fieldName, sbyte?[] value );

	void WriteSByteNArrN( string? fieldName, sbyte?[]? value );

	void WriteByte( string? fieldName, byte value );

	void WriteByteN( string? fieldName, byte? value );

	void WriteByteArr( string? fieldName, byte[] value );

	void WriteByteArrN( string? fieldName, byte[]? value );

	void WriteByteNArr( string? fieldName, byte?[] value );

	void WriteByteNArrN( string? fieldName, byte?[]? value );

	void WriteInt16( string? fieldName, short value );

	void WriteInt16N( string? fieldName, short? value );

	void WriteInt16Arr( string? fieldName, short[] value );

	void WriteInt16ArrN( string? fieldName, short[]? value );

	void WriteInt16NArr( string? fieldName, short?[] value );

	void WriteInt16NArrN( string? fieldName, short?[]? value );

	void WriteUInt16( string? fieldName, ushort value );

	void WriteUInt16N( string? fieldName, ushort? value );

	void WriteUInt16Arr( string? fieldName, ushort[] value );

	void WriteUInt16ArrN( string? fieldName, ushort[]? value );

	void WriteUInt16NArr( string? fieldName, ushort?[] value );

	void WriteUInt16NArrN( string? fieldName, ushort?[]? value );

	void WriteInt32( string? fieldName, int value );

	void WriteInt32N( string? fieldName, int? value );

	void WriteInt32Arr( string? fieldName, int[] value );

	void WriteInt32ArrN( string? fieldName, int[]? value );

	void WriteInt32NArr( string? fieldName, int?[] value );

	void WriteInt32NArrN( string? fieldName, int?[]? value );

	void WriteUInt32( string? fieldName, uint value );

	void WriteUInt32N( string? fieldName, uint? value );

	void WriteUInt32Arr( string? fieldName, uint[] value );

	void WriteUInt32ArrN( string? fieldName, uint[]? value );

	void WriteUInt32NArr( string? fieldName, uint?[] value );

	void WriteUInt32NArrN( string? fieldName, uint?[]? value );

	void WriteInt64( string? fieldName, long value );

	void WriteInt64N( string? fieldName, long? value );

	void WriteInt64Arr( string? fieldName, long[] value );

	void WriteInt64ArrN( string? fieldName, long[]? value );

	void WriteInt64NArr( string? fieldName, long?[] value );

	void WriteInt64NArrN( string? fieldName, long?[]? value );

	void WriteUInt64( string? fieldName, ulong value );

	void WriteUInt64N( string? fieldName, ulong? value );

	void WriteUInt64Arr( string? fieldName, ulong[] value );

	void WriteUInt64ArrN( string? fieldName, ulong[]? value );

	void WriteUInt64NArr( string? fieldName, ulong?[] value );

	void WriteUInt64NArrN( string? fieldName, ulong?[]? value );

	void WriteFloat( string? fieldName, float value );

	void WriteFloatN( string? fieldName, float? value );

	void WriteFloatArr( string? fieldName, float[] value );

	void WriteFloatArrN( string? fieldName, float[]? value );

	void WriteFloatNArr( string? fieldName, float?[] value );

	void WriteFloatNArrN( string? fieldName, float?[]? value );

	void WriteDouble( string? fieldName, double value );

	void WriteDoubleN( string? fieldName, double? value );

	void WriteDoubleArr( string? fieldName, double[] value );

	void WriteDoubleArrN( string? fieldName, double[]? value );

	void WriteDoubleNArr( string? fieldName, double?[] value );

	void WriteDoubleNArrN( string? fieldName, double?[]? value );

	void WriteDecimal( string? fieldName, decimal value );

	void WriteDecimalN( string? fieldName, decimal? value );

	void WriteDecimalArr( string? fieldName, decimal[] value );

	void WriteDecimalArrN( string? fieldName, decimal[]? value );

	void WriteDecimalNArr( string? fieldName, decimal?[] value );

	void WriteDecimalNArrN( string? fieldName, decimal?[]? value );

	void WriteGuid( string? fieldName, Guid value );

	void WriteGuidN( string? fieldName, Guid? value );

	void WriteDateTime( string? fieldName, DateTime value );

	void WriteDateTimeN( string? fieldName, DateTime? value );

	void WriteDateTimeOffset( string? fieldName, DateTimeOffset value );

	void WriteDateTimeOffsetN( string? fieldName, DateTimeOffset? value );

	void WriteTimeSpan( string? fieldName, TimeSpan value );

	void WriteTimeSpanN( string? fieldName, TimeSpan? value );

	void WriteString( string? fieldName, string value );

	void WriteStringN( string? fieldName, string? value );

	void WriteStringArr( string? fieldName, string[] value );

	void WriteStringArrN( string? fieldName, string[]? value );

	void WriteStringNArr( string? fieldName, string?[] value );

	void WriteStringNArrN( string? fieldName, string?[]? value );

	void WriteObjectEmpty( string? fieldName );

	void WriteObjectStart( string? fieldName, ushort shortTypeId );

	void WriteObjectEnd();

	void WriteCollectionStart( string? fieldName, int count, bool isPrimitive = false );

	void WriteCollectionEnd( bool isPrimitive = false );
}
