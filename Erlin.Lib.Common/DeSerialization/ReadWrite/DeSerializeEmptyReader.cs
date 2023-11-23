namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Unusable reader of primitive data
/// </summary>
public class DeSerializeEmptyReader : IDeSerializeReader
{
	public bool ImplementsRead
	{
		get { return false; }
	}

	public bool ReadBool( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public bool? ReadBoolN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte?[] ReadSByteNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte ReadByte( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte? ReadByteN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte ReadSByte( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte? ReadSByteN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte[] ReadByteArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte[] ReadByteArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte?[] ReadByteNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public byte?[] ReadByteNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte[] ReadSByteArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte[] ReadSByteArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public sbyte?[] ReadSByteNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short?[] ReadInt16NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort ReadUInt16( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort? ReadUInt16N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort[] ReadUInt16Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort[] ReadUInt16ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort?[] ReadUInt16NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort?[] ReadUInt16NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short ReadInt16( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short? ReadInt16N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short[] ReadInt16Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short[] ReadInt16ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public short?[] ReadInt16NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong[] ReadUInt64ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong?[] ReadUInt64NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong?[] ReadUInt64NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float ReadFloat( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float? ReadFloatN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float[] ReadFloatArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float[] ReadFloatArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float?[] ReadFloatNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public float?[] ReadFloatNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double ReadDouble( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double? ReadDoubleN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double[] ReadDoubleArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double[] ReadDoubleArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double?[] ReadDoubleNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public double?[] ReadDoubleNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int ReadInt32( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int? ReadInt32N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int[] ReadInt32Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int[] ReadInt32ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int?[] ReadInt32NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int?[] ReadInt32NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint ReadUInt32( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint? ReadUInt32N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint[] ReadUInt32Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint[] ReadUInt32ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint?[] ReadUInt32NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public uint?[] ReadUInt32NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long ReadInt64( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long? ReadInt64N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long[] ReadInt64Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long[] ReadInt64ArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long?[] ReadInt64NArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public long?[] ReadInt64NArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong ReadUInt64( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong? ReadUInt64N( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ulong[] ReadUInt64Arr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal ReadDecimal( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal? ReadDecimalN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal[] ReadDecimalArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal[] ReadDecimalArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal?[] ReadDecimalNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public decimal?[] ReadDecimalNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public TimeSpan? ReadTimeSpanN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string ReadString( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string ReadStringN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string[] ReadStringArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string[] ReadStringArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string?[] ReadStringNArr( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public string?[] ReadStringNArrN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public Guid ReadGuid( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public Guid? ReadGuidN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public DateTime ReadDateTime( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public DateTime? ReadDateTimeN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public DateTimeOffset ReadDateTimeOffset( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public DateTimeOffset? ReadDateTimeOffsetN( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public TimeSpan ReadTimeSpan( string? fieldName )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public ushort ReadObjectStart( string? fieldName, int? itemIndex )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public void ReadObjectEnd( string? fieldName, Type objectType )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public int ReadCollectionStart( string? fieldName, bool isPrimitive = false )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}

	public void ReadCollectionEnd( string? fieldName, Type objectType, bool isPrimitive = false )
	{
		throw new DeSerializeException( "Attempt to read during serialization!" );
	}
}
