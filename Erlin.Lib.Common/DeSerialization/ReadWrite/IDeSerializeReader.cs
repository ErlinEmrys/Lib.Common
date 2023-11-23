namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Reader of primitive data
/// </summary>
public interface IDeSerializeReader
{
	/// <summary>
	///    Indicates that this object implements reading
	/// </summary>
	bool ImplementsRead { get; }

	bool ReadBool( string? fieldName );

	bool? ReadBoolN( string? fieldName );

	sbyte ReadSByte( string? fieldName );

	sbyte? ReadSByteN( string? fieldName );

	sbyte[] ReadSByteArr( string? fieldName );

	sbyte[]? ReadSByteArrN( string? fieldName );

	sbyte?[] ReadSByteNArr( string? fieldName );

	sbyte?[]? ReadSByteNArrN( string? fieldName );

	byte ReadByte( string? fieldName );

	byte? ReadByteN( string? fieldName );

	byte[] ReadByteArr( string? fieldName );

	byte[]? ReadByteArrN( string? fieldName );

	byte?[] ReadByteNArr( string? fieldName );

	byte?[]? ReadByteNArrN( string? fieldName );

	short ReadInt16( string? fieldName );

	short? ReadInt16N( string? fieldName );

	short[] ReadInt16Arr( string? fieldName );

	short[]? ReadInt16ArrN( string? fieldName );

	short?[] ReadInt16NArr( string? fieldName );

	short?[]? ReadInt16NArrN( string? fieldName );

	ushort ReadUInt16( string? fieldName );

	ushort? ReadUInt16N( string? fieldName );

	ushort[] ReadUInt16Arr( string? fieldName );

	ushort[]? ReadUInt16ArrN( string? fieldName );

	ushort?[] ReadUInt16NArr( string? fieldName );

	ushort?[]? ReadUInt16NArrN( string? fieldName );

	int ReadInt32( string? fieldName );

	int? ReadInt32N( string? fieldName );

	int[] ReadInt32Arr( string? fieldName );

	int[]? ReadInt32ArrN( string? fieldName );

	int?[] ReadInt32NArr( string? fieldName );

	int?[]? ReadInt32NArrN( string? fieldName );

	uint ReadUInt32( string? fieldName );

	uint? ReadUInt32N( string? fieldName );

	uint[] ReadUInt32Arr( string? fieldName );

	uint[]? ReadUInt32ArrN( string? fieldName );

	uint?[] ReadUInt32NArr( string? fieldName );

	uint?[]? ReadUInt32NArrN( string? fieldName );

	long ReadInt64( string? fieldName );

	long? ReadInt64N( string? fieldName );

	long[] ReadInt64Arr( string? fieldName );

	long[]? ReadInt64ArrN( string? fieldName );

	long?[] ReadInt64NArr( string? fieldName );

	long?[]? ReadInt64NArrN( string? fieldName );

	ulong ReadUInt64( string? fieldName );

	ulong? ReadUInt64N( string? fieldName );

	ulong[] ReadUInt64Arr( string? fieldName );

	ulong[]? ReadUInt64ArrN( string? fieldName );

	ulong?[] ReadUInt64NArr( string? fieldName );

	ulong?[]? ReadUInt64NArrN( string? fieldName );

	float ReadFloat( string? fieldName );

	float? ReadFloatN( string? fieldName );

	float[] ReadFloatArr( string? fieldName );

	float[]? ReadFloatArrN( string? fieldName );

	float?[] ReadFloatNArr( string? fieldName );

	float?[]? ReadFloatNArrN( string? fieldName );

	double ReadDouble( string? fieldName );

	double? ReadDoubleN( string? fieldName );

	double[] ReadDoubleArr( string? fieldName );

	double[]? ReadDoubleArrN( string? fieldName );

	double?[] ReadDoubleNArr( string? fieldName );

	double?[]? ReadDoubleNArrN( string? fieldName );

	decimal ReadDecimal( string? fieldName );

	decimal? ReadDecimalN( string? fieldName );

	decimal[] ReadDecimalArr( string? fieldName );

	decimal[]? ReadDecimalArrN( string? fieldName );

	decimal?[] ReadDecimalNArr( string? fieldName );

	decimal?[]? ReadDecimalNArrN( string? fieldName );

	Guid ReadGuid( string? fieldName );

	Guid? ReadGuidN( string? fieldName );

	DateTime ReadDateTime( string? fieldName );

	DateTime? ReadDateTimeN( string? fieldName );

	DateTimeOffset ReadDateTimeOffset( string? fieldName );

	DateTimeOffset? ReadDateTimeOffsetN( string? fieldName );

	TimeSpan ReadTimeSpan( string? fieldName );

	TimeSpan? ReadTimeSpanN( string? fieldName );

	string ReadString( string? fieldName );

	string? ReadStringN( string? fieldName );

	string[] ReadStringArr( string? fieldName );

	string[]? ReadStringArrN( string? fieldName );

	string?[] ReadStringNArr( string? fieldName );

	string?[]? ReadStringNArrN( string? fieldName );

	ushort ReadObjectStart( string? fieldName, int? itemIndex );

	void ReadObjectEnd( string? fieldName, Type objectType );

	int ReadCollectionStart( string? fieldName, bool isPrimitive = false );

	void ReadCollectionEnd( string? fieldName, Type objectType, bool isPrimitive = false );
}
