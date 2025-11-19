using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Binary reader of primitive data
/// </summary>
public class DeSerializeBinaryReader : IDeSerializeReader
{
	/// <summary>
	///    Internal reader
	/// </summary>
	private BinaryReader Reader { get; set; }

	/// <summary>
	///    Sign, that this reader actually reads data
	/// </summary>
	public bool ImplementsRead
	{
		get { return true; }
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="stream">Input stream</param>
	public DeSerializeBinaryReader( Stream stream )
	{
		SwitchStream( stream );
	}

	/// <summary>
	///    Switches underlying stream to read
	/// </summary>
	/// <param name="stream"></param>
	[ MemberNotNull( nameof( DeSerializeBinaryReader.Reader ) ) ]
	public void SwitchStream( Stream stream )
	{
		Reader = new BinaryReader( stream, Encoding.UTF8, true );
	}

	/// <summary>
	///    Reads array of data
	/// </summary>
	/// <param name="fieldName"></param>
	/// <param name="reader"></param>
	/// <param name="allowNull"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	/// <exception cref="DeSerializeException"></exception>
	private T[] ReadArr< T >( string? fieldName, Func< string, T > reader, bool allowNull )
	{
		T[]? reading = ReadArrN( fieldName, reader, allowNull );
		if( reading == null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return reading;
	}

	/// <summary>
	///    Reads nullable array of data
	/// </summary>
	/// <param name="fieldName"></param>
	/// <param name="reader"></param>
	/// <param name="allowNull"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	/// <exception cref="DeSerializeException"></exception>
	private T[]? ReadArrN< T >( string? fieldName, Func< string, T > reader, bool allowNull )
	{
		int length = Reader.ReadInt32();
		if( length >= 0 )
		{
			T[] result = new T[length];
			for( int i = 0; i < length; i++ )
			{
				T value = reader( string.Empty );
				if( !allowNull && value is null )
				{
					throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
				}

				result[ i ] = value;
			}

			return result;
		}

		if( length != DeSerializeConstants.FLAG_LENGTH_NULL )
		{
			throw new DeSerializeException( $"Unexpected length {length} of collection {fieldName}" );
		}

		return null;
	}

	/// <summary>
	///    Read existence of next value
	/// </summary>
	/// <param name="fieldName">Field name</param>
	/// <returns>Value exist</returns>
	private bool ReadValueExist( string? fieldName )
	{
		return ReadBool( fieldName );
	}

	public bool ReadBool( string? fieldName )
	{
		return Reader.ReadBoolean();
	}

	public bool? ReadBoolN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadBool( fieldName );
		}

		return null;
	}

	public sbyte ReadSByte( string? fieldName )
	{
		return Reader.ReadSByte();
	}

	public sbyte? ReadSByteN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadSByte( fieldName );
		}

		return null;
	}

	public sbyte[] ReadSByteArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadSByte, false );
	}

	public sbyte[]? ReadSByteArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadSByte, false );
	}

	public sbyte?[] ReadSByteNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadSByteN, true );
	}

	public sbyte?[]? ReadSByteNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadSByteN, true );
	}

	public byte ReadByte( string? fieldName )
	{
		return Reader.ReadByte();
	}

	public byte? ReadByteN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadByte( fieldName );
		}

		return null;
	}

	public byte[] ReadByteArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadByte, false );
	}

	public byte[]? ReadByteArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadByte, false );
	}

	public byte?[] ReadByteNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadByteN, true );
	}

	public byte?[]? ReadByteNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadByteN, true );
	}

	public short ReadInt16( string? fieldName )
	{
		return Reader.ReadInt16();
	}

	public short? ReadInt16N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadInt16( fieldName );
		}

		return null;
	}

	public short[] ReadInt16Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt16, false );
	}

	public short[]? ReadInt16ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt16, false );
	}

	public short?[] ReadInt16NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt16N, true );
	}

	public short?[]? ReadInt16NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt16N, true );
	}

	public ushort ReadUInt16( string? fieldName )
	{
		return Reader.ReadUInt16();
	}

	public ushort? ReadUInt16N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadUInt16( fieldName );
		}

		return null;
	}

	public ushort[] ReadUInt16Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt16, false );
	}

	public ushort[]? ReadUInt16ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt16, false );
	}

	public ushort?[] ReadUInt16NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt16N, true );
	}

	public ushort?[]? ReadUInt16NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt16N, true );
	}

	public int ReadInt32( string? fieldName )
	{
		return Reader.ReadInt32();
	}

	public int? ReadInt32N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadInt32( fieldName );
		}

		return null;
	}

	public int[] ReadInt32Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt32, false );
	}

	public int[]? ReadInt32ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt32, false );
	}

	public int?[] ReadInt32NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt32N, true );
	}

	public int?[]? ReadInt32NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt32N, true );
	}

	public uint ReadUInt32( string? fieldName )
	{
		return Reader.ReadUInt32();
	}

	public uint? ReadUInt32N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadUInt32( fieldName );
		}

		return null;
	}

	public uint[] ReadUInt32Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt32, false );
	}

	public uint[]? ReadUInt32ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt32, false );
	}

	public uint?[] ReadUInt32NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt32N, true );
	}

	public uint?[]? ReadUInt32NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt32N, true );
	}

	public long ReadInt64( string? fieldName )
	{
		return Reader.ReadInt64();
	}

	public long? ReadInt64N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadInt64( fieldName );
		}

		return null;
	}

	public long[] ReadInt64Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt64, false );
	}

	public long[]? ReadInt64ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt64, false );
	}

	public long?[] ReadInt64NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadInt64N, true );
	}

	public long?[]? ReadInt64NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadInt64N, true );
	}

	public ulong ReadUInt64( string? fieldName )
	{
		return Reader.ReadUInt64();
	}

	public ulong? ReadUInt64N( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadUInt64( fieldName );
		}

		return null;
	}

	public ulong[] ReadUInt64Arr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt64, false );
	}

	public ulong[]? ReadUInt64ArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt64, false );
	}

	public ulong?[] ReadUInt64NArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadUInt64N, true );
	}

	public ulong?[]? ReadUInt64NArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadUInt64N, true );
	}

	public float ReadFloat( string? fieldName )
	{
		return Reader.ReadSingle();
	}

	public float? ReadFloatN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadFloat( fieldName );
		}

		return null;
	}

	public float[] ReadFloatArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadFloat, false );
	}

	public float[]? ReadFloatArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadFloat, false );
	}

	public float?[] ReadFloatNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadFloatN, true );
	}

	public float?[]? ReadFloatNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadFloatN, true );
	}

	public double ReadDouble( string? fieldName )
	{
		return Reader.ReadDouble();
	}

	public double? ReadDoubleN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadDouble( fieldName );
		}

		return null;
	}

	public double[] ReadDoubleArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadDouble, false );
	}

	public double[]? ReadDoubleArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadDouble, false );
	}

	public double?[] ReadDoubleNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadDoubleN, true );
	}

	public double?[]? ReadDoubleNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadDoubleN, true );
	}

	public decimal ReadDecimal( string? fieldName )
	{
		return Reader.ReadDecimal();
	}

	public decimal? ReadDecimalN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadDecimal( fieldName );
		}

		return null;
	}

	public decimal[] ReadDecimalArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadDecimal, false );
	}

	public decimal[]? ReadDecimalArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadDecimal, false );
	}

	public decimal?[] ReadDecimalNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadDecimalN, true );
	}

	public decimal?[]? ReadDecimalNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadDecimalN, true );
	}

	public Guid ReadGuid( string? fieldName )
	{
		byte[] arr = ReadByteArr( fieldName );
		return new Guid( arr );
	}

	public Guid? ReadGuidN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadGuid( fieldName );
		}

		return null;
	}

	public DateTime ReadDateTime( string? fieldName )
	{
		long ticks = ReadInt64( fieldName );
		DateTimeKind kind = ( DateTimeKind )ReadByte( fieldName );
		return new DateTime( ticks, kind );
	}

	public DateTime? ReadDateTimeN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadDateTime( fieldName );
		}

		return null;
	}

	public DateTimeOffset ReadDateTimeOffset( string? fieldName )
	{
		long ticks = ReadInt64( fieldName );
		long offsetTicks = ReadInt64( fieldName );
		return new DateTimeOffset( ticks, new TimeSpan( offsetTicks ) );
	}

	public DateTimeOffset? ReadDateTimeOffsetN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadDateTimeOffset( fieldName );
		}

		return null;
	}

	public TimeSpan ReadTimeSpan( string? fieldName )
	{
		long ticks = ReadInt64( fieldName );
		return new TimeSpan( ticks );
	}

	public TimeSpan? ReadTimeSpanN( string? fieldName )
	{
		if( ReadValueExist( fieldName ) )
		{
			return ReadTimeSpan( fieldName );
		}

		return null;
	}

	public string ReadString( string? fieldName )
	{
		string? reading = ReadStringN( fieldName );
		if( reading == null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return reading;
	}

	public string? ReadStringN( string? fieldName )
	{
		int length = Reader.ReadInt32();
		switch( length )
		{
			case DeSerializeConstants.FLAG_LENGTH_NULL:
				return null;

			case DeSerializeConstants.FLAG_STRING_EMPTY:
				return string.Empty;

			default:
			{
				byte[] array = Reader.ReadBytes( length );
				return Encoding.UTF8.GetString( array );
			}
		}
	}

	public string[] ReadStringArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadString, false );
	}

	public string[]? ReadStringArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadString, false );
	}

	public string?[] ReadStringNArr( string? fieldName )
	{
		return ReadArr( fieldName, ReadStringN, true );
	}

	public string?[]? ReadStringNArrN( string? fieldName )
	{
		return ReadArrN( fieldName, ReadStringN, true );
	}

	public ushort ReadObjectStart( string? fieldName, int? itemIndex )
	{
		return Reader.ReadUInt16();
	}

	public void ReadObjectEnd( string? fieldName, Type objectType )
	{
		byte value = Reader.ReadByte();
		if( value != DeSerializeConstants.FLAG_OBJECT_END )
		{
			throw new DeSerializeException( $"Invalid deserialized object '{objectType.FullName}' End of object \"{fieldName}\" expected!" );
		}
	}

	public int ReadCollectionStart( string? fieldName, bool isPrimitive = false )
	{
		int count = ReadInt32( fieldName );
		return count;
	}

	public void ReadCollectionEnd( string? fieldName, Type objectType, bool isPrimitive = false )
	{
		byte value = ReadByte( fieldName );
		if( value != DeSerializeConstants.FLAG_COLLECTION_END )
		{
			throw new DeSerializeException( $"Invalid deserialized object '{objectType.FullName}' End of collection \"{fieldName}\" expected!" );
		}
	}
}
