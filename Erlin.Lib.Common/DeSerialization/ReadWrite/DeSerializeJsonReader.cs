using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    JSON reader of primitive data
/// </summary>
public class DeSerializeJsonReader : IDeSerializeReader
{
	/// <summary>
	///    Current JSON node
	/// </summary>
	private JContainer Current { get; set; }

	/// <summary>
	///    Saved Parents of current JSON node
	/// </summary>
	private Stack< JContainer > Parents { get; } = new();

	/// <summary>
	///    Sign, that current JSON node is collection, that contains only primitive data
	/// </summary>
	private bool ReadingPrimitiveCollection { get; set; }

	/// <summary>
	///    Reading index of primitive data from collection
	/// </summary>
	private int PrimitiveCollectionReadingIndex { get; set; }

	/// <summary>
	///    Sign, that this reader actually reads data
	/// </summary>
	public bool ImplementsRead
	{
		get { return true; }
	}

	public DeSerializeJsonReader( Stream stream )
	{
		JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

		using StreamReader reader = new( stream, Encoding.UTF8, true, -1, true );
		using JsonTextReader jsonReader = new( reader );

		JObject? parsed = ( JObject? )jsonSerializer.Deserialize( reader, typeof( JObject ) );

		Current = parsed ?? throw new DeSerializeException( "Parsing of JSON failed!" );
	}

	/// <summary>
	///    Check if current JSON node contains key
	/// </summary>
	private bool ContainsKey( [ NotNullWhen( true ) ]string? key )
	{
		if( Current is JObject obj )
		{
			if( key.IsEmpty() )
			{
				throw new DeSerializeException( "Attempt to read empty key on keyed container!" );
			}

			return obj.ContainsKey( key );
		}

		throw new DeSerializeException( $"Attempt to read key {key} on non-key container!" );
	}

	/// <summary>
	///    Read non-null value from current JSON node
	/// </summary>
	private T ReadValue< T >( string? fieldName )
		where T : struct
	{
		T? value = ReadValueN< T >( fieldName );
		if( !value.HasValue )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value.Value;
	}

	/// <summary>
	///    Read nullable string from current JSON node
	/// </summary>
	private string? ReadValueStringN( string? fieldName )
	{
		string value = string.Empty;
		return ReadValueN( fieldName, ref value ) ? value : null;
	}

	/// <summary>
	///    Read nullable value from current JSON node
	/// </summary>
	private T? ReadValueN< T >( string? fieldName )
		where T : struct
	{
		T? value = null;
		return ReadValueN( fieldName, ref value ) ? value : null;
	}

	/// <summary>
	///    Read value from current JSON node
	/// </summary>
	private bool ReadValueN< T >( string? fieldName, ref T value )
	{
		JToken? token = null;
		if( ReadingPrimitiveCollection )
		{
			if( Current is JArray jArr )
			{
				token = jArr[ PrimitiveCollectionReadingIndex++ ];
			}
		}
		else if( ContainsKey( fieldName ) )
		{
			token = Current[ fieldName ];
		}

		if( ( token != null ) && ( token.Type != JTokenType.Null ) && ( token.Type != JTokenType.Undefined ) )
		{
			T? nullableValue = token.Value< T >();
			if( nullableValue is null )
			{
				return false;
			}

			value = nullableValue;
			return true;
		}

		return false;
	}

	/// <summary>
	///    Read non-null array of data from current JSON node
	/// </summary>
	/// <param name="fieldName">Name of the DeSerialized field</param>
	/// <param name="allowNull">Sign whether the array items can be null</param>
	private T[] ReadArray< T >( string? fieldName, bool allowNull )
	{
		T[]? value = ReadArrayN< T >( fieldName, allowNull );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value;
	}

	/// <summary>
	///    Reads nullable array of data from current JSON node
	/// </summary>
	/// <param name="fieldName">Name of the DeSerialized field</param>
	/// <param name="allowNull">Sign whether the array items can be null</param>
	private T[]? ReadArrayN< T >( string? fieldName, bool allowNull )
	{
		if( ContainsKey( fieldName ) )
		{
			JToken? token = Current[ fieldName ];
			if( token is JArray jArr
				&& ( jArr.Type != JTokenType.Null )
				&& ( jArr.Type != JTokenType.Undefined ) )
			{
				T[] result = new T[jArr.Count];
				for( int i = 0; i < jArr.Count; i++ )
				{
					T? value = jArr[ i ].Value< T >();
					if( !allowNull && value is null )
					{
						throw new DeSerializeException( "Reading NULL value as one of the item in array!" );
					}

#pragma warning disable CS8601
					result[ i ] = value;
#pragma warning restore CS8601
				}

				return result;
			}
		}

		return null;
	}

	public bool ReadBool( string? fieldName )
	{
		return ReadValue< bool >( fieldName );
	}

	public bool? ReadBoolN( string? fieldName )
	{
		return ReadValueN< bool >( fieldName );
	}

	public sbyte ReadSByte( string? fieldName )
	{
		return ReadValue< sbyte >( fieldName );
	}

	public sbyte? ReadSByteN( string? fieldName )
	{
		return ReadValueN< sbyte >( fieldName );
	}

	public sbyte[] ReadSByteArr( string? fieldName )
	{
		return ReadArray< sbyte >( fieldName, false );
	}

	public sbyte[]? ReadSByteArrN( string? fieldName )
	{
		return ReadArrayN< sbyte >( fieldName, false );
	}

	public sbyte?[] ReadSByteNArr( string? fieldName )
	{
		return ReadArray< sbyte? >( fieldName, true );
	}

	public sbyte?[]? ReadSByteNArrN( string? fieldName )
	{
		return ReadArrayN< sbyte? >( fieldName, true );
	}

	public byte ReadByte( string? fieldName )
	{
		return ReadValue< byte >( fieldName );
	}

	public byte? ReadByteN( string? fieldName )
	{
		return ReadValueN< byte >( fieldName );
	}

	public byte[] ReadByteArr( string? fieldName )
	{
		return ReadArray< byte >( fieldName, false );
	}

	public byte[]? ReadByteArrN( string? fieldName )
	{
		return ReadArrayN< byte >( fieldName, false );
	}

	public byte?[] ReadByteNArr( string? fieldName )
	{
		return ReadArray< byte? >( fieldName, true );
	}

	public byte?[]? ReadByteNArrN( string? fieldName )
	{
		return ReadArrayN< byte? >( fieldName, true );
	}

	public short ReadInt16( string? fieldName )
	{
		return ReadValue< short >( fieldName );
	}

	public short? ReadInt16N( string? fieldName )
	{
		return ReadValueN< short >( fieldName );
	}

	public short[] ReadInt16Arr( string? fieldName )
	{
		return ReadArray< short >( fieldName, false );
	}

	public short[]? ReadInt16ArrN( string? fieldName )
	{
		return ReadArrayN< short >( fieldName, false );
	}

	public short?[] ReadInt16NArr( string? fieldName )
	{
		return ReadArray< short? >( fieldName, true );
	}

	public short?[]? ReadInt16NArrN( string? fieldName )
	{
		return ReadArrayN< short? >( fieldName, true );
	}

	public ushort ReadUInt16( string? fieldName )
	{
		return ReadValue< ushort >( fieldName );
	}

	public ushort? ReadUInt16N( string? fieldName )
	{
		return ReadValueN< ushort >( fieldName );
	}

	public ushort[] ReadUInt16Arr( string? fieldName )
	{
		return ReadArray< ushort >( fieldName, false );
	}

	public ushort[]? ReadUInt16ArrN( string? fieldName )
	{
		return ReadArrayN< ushort >( fieldName, false );
	}

	public ushort?[] ReadUInt16NArr( string? fieldName )
	{
		return ReadArray< ushort? >( fieldName, true );
	}

	public ushort?[]? ReadUInt16NArrN( string? fieldName )
	{
		return ReadArrayN< ushort? >( fieldName, true );
	}

	public int ReadInt32( string? fieldName )
	{
		return ReadValue< int >( fieldName );
	}

	public int? ReadInt32N( string? fieldName )
	{
		return ReadValueN< int >( fieldName );
	}

	public int[] ReadInt32Arr( string? fieldName )
	{
		return ReadArray< int >( fieldName, false );
	}

	public int[]? ReadInt32ArrN( string? fieldName )
	{
		return ReadArrayN< int >( fieldName, false );
	}

	public int?[] ReadInt32NArr( string? fieldName )
	{
		return ReadArray< int? >( fieldName, true );
	}

	public int?[]? ReadInt32NArrN( string? fieldName )
	{
		return ReadArrayN< int? >( fieldName, true );
	}

	public uint ReadUInt32( string? fieldName )
	{
		return ReadValue< uint >( fieldName );
	}

	public uint? ReadUInt32N( string? fieldName )
	{
		return ReadValueN< uint >( fieldName );
	}

	public uint[] ReadUInt32Arr( string? fieldName )
	{
		return ReadArray< uint >( fieldName, false );
	}

	public uint[]? ReadUInt32ArrN( string? fieldName )
	{
		return ReadArrayN< uint >( fieldName, false );
	}

	public uint?[] ReadUInt32NArr( string? fieldName )
	{
		return ReadArray< uint? >( fieldName, true );
	}

	public uint?[]? ReadUInt32NArrN( string? fieldName )
	{
		return ReadArrayN< uint? >( fieldName, true );
	}

	public long ReadInt64( string? fieldName )
	{
		return ReadValue< long >( fieldName );
	}

	public long? ReadInt64N( string? fieldName )
	{
		return ReadValueN< long >( fieldName );
	}

	public long[] ReadInt64Arr( string? fieldName )
	{
		return ReadArray< long >( fieldName, false );
	}

	public long[]? ReadInt64ArrN( string? fieldName )
	{
		return ReadArrayN< long >( fieldName, false );
	}

	public long?[] ReadInt64NArr( string? fieldName )
	{
		return ReadArray< long? >( fieldName, true );
	}

	public long?[]? ReadInt64NArrN( string? fieldName )
	{
		return ReadArrayN< long? >( fieldName, true );
	}

	public ulong ReadUInt64( string? fieldName )
	{
		return ReadValue< ulong >( fieldName );
	}

	public ulong? ReadUInt64N( string? fieldName )
	{
		return ReadValueN< ulong >( fieldName );
	}

	public ulong[] ReadUInt64Arr( string? fieldName )
	{
		return ReadArray< ulong >( fieldName, false );
	}

	public ulong[]? ReadUInt64ArrN( string? fieldName )
	{
		return ReadArrayN< ulong >( fieldName, false );
	}

	public ulong?[] ReadUInt64NArr( string? fieldName )
	{
		return ReadArray< ulong? >( fieldName, true );
	}

	public ulong?[]? ReadUInt64NArrN( string? fieldName )
	{
		return ReadArrayN< ulong? >( fieldName, true );
	}

	public float ReadFloat( string? fieldName )
	{
		return ReadValue< float >( fieldName );
	}

	public float? ReadFloatN( string? fieldName )
	{
		return ReadValueN< float >( fieldName );
	}

	public float[] ReadFloatArr( string? fieldName )
	{
		return ReadArray< float >( fieldName, false );
	}

	public float[]? ReadFloatArrN( string? fieldName )
	{
		return ReadArrayN< float >( fieldName, false );
	}

	public float?[] ReadFloatNArr( string? fieldName )
	{
		return ReadArray< float? >( fieldName, true );
	}

	public float?[]? ReadFloatNArrN( string? fieldName )
	{
		return ReadArrayN< float? >( fieldName, true );
	}

	public double ReadDouble( string? fieldName )
	{
		return ReadValue< double >( fieldName );
	}

	public double? ReadDoubleN( string? fieldName )
	{
		return ReadValueN< double >( fieldName );
	}

	public double[] ReadDoubleArr( string? fieldName )
	{
		return ReadArray< double >( fieldName, false );
	}

	public double[]? ReadDoubleArrN( string? fieldName )
	{
		return ReadArrayN< double >( fieldName, false );
	}

	public double?[] ReadDoubleNArr( string? fieldName )
	{
		return ReadArray< double? >( fieldName, true );
	}

	public double?[]? ReadDoubleNArrN( string? fieldName )
	{
		return ReadArrayN< double? >( fieldName, true );
	}

	public decimal ReadDecimal( string? fieldName )
	{
		return ReadValue< decimal >( fieldName );
	}

	public decimal? ReadDecimalN( string? fieldName )
	{
		return ReadValueN< decimal >( fieldName );
	}

	public decimal[] ReadDecimalArr( string? fieldName )
	{
		return ReadArray< decimal >( fieldName, false );
	}

	public decimal[]? ReadDecimalArrN( string? fieldName )
	{
		return ReadArrayN< decimal >( fieldName, false );
	}

	public decimal?[] ReadDecimalNArr( string? fieldName )
	{
		return ReadArray< decimal? >( fieldName, true );
	}

	public decimal?[]? ReadDecimalNArrN( string? fieldName )
	{
		return ReadArrayN< decimal? >( fieldName, true );
	}

	public Guid ReadGuid( string? fieldName )
	{
		Guid? value = ReadGuidN( fieldName );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value.Value;
	}

	public Guid? ReadGuidN( string? fieldName )
	{
		string? identifier = ReadValueStringN( fieldName );
		if( identifier.IsNotEmpty() )
		{
			if( Guid.TryParse( identifier, out Guid result ) )
			{
				return result;
			}

			throw new DeSerializeException( $"Could not parse Guid value: {identifier} for field: {fieldName}" );
		}

		return null;
	}

	public DateTime ReadDateTime( string? fieldName )
	{
		DateTime? value = ReadDateTimeN( fieldName );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value.Value;
	}

	public DateTime? ReadDateTimeN( string? fieldName )
	{
		string? text = ReadValueStringN( fieldName );
		if( text.IsNotEmpty() )
		{
			if( DateTime.TryParse( text, null, DateTimeStyles.None, out DateTime result ) )
			{
				return result;
			}

			throw new DeSerializeException( $"Could not parse DateTime value: {text} for field: {fieldName}" );
		}

		return null;
	}

	public DateTimeOffset ReadDateTimeOffset( string? fieldName )
	{
		DateTimeOffset? value = ReadDateTimeOffsetN( fieldName );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value.Value;
	}

	public DateTimeOffset? ReadDateTimeOffsetN( string? fieldName )
	{
		string? text = ReadValueStringN( fieldName );
		if( text.IsNotEmpty() )
		{
			if( DateTimeOffset.TryParse( text, out DateTimeOffset result ) )
			{
				return result;
			}

			throw new DeSerializeException( $"Could not parse DateTimeOffset value: {text} for field: {fieldName}" );
		}

		return null;
	}

	public TimeSpan ReadTimeSpan( string? fieldName )
	{
		TimeSpan? value = ReadTimeSpanN( fieldName );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value.Value;
	}

	public TimeSpan? ReadTimeSpanN( string? fieldName )
	{
		string? text = ReadValueStringN( fieldName );
		if( text.IsNotEmpty() )
		{
			if( TimeSpan.TryParse( text, out TimeSpan result ) )
			{
				return result;
			}

			throw new DeSerializeException( $"Could not parse TimeSpan value: {text} for field: {fieldName}" );
		}

		return null;
	}

	public string ReadString( string? fieldName )
	{
		string? value = ReadStringN( fieldName );
		if( value is null )
		{
			throw new DeSerializeException( $"Reading NULL on expected value {fieldName}" );
		}

		return value;
	}

	public string? ReadStringN( string? fieldName )
	{
		return ReadValueStringN( fieldName );
	}

	public string[] ReadStringArr( string? fieldName )
	{
		return ReadArray< string >( fieldName, false );
	}

	public string[]? ReadStringArrN( string? fieldName )
	{
		return ReadArrayN< string >( fieldName, false );
	}

	public string?[] ReadStringNArr( string? fieldName )
	{
		return ReadArray< string? >( fieldName, true );
	}

	public string?[]? ReadStringNArrN( string? fieldName )
	{
		return ReadArrayN< string? >( fieldName, true );
	}

	public ushort ReadObjectStart( string? fieldName, int? itemIndex )
	{
		JToken? token = null;
		if( itemIndex.HasValue )
		{
			token = Current[ itemIndex ];
		}
		else if( fieldName.IsNotEmpty() && ContainsKey( fieldName ) )
		{
			token = Current[ fieldName ];
		}

		if( token is JObject obj )
		{
			Parents.Push( Current );
			Current = obj;

			return ReadUInt16( DeSerializeConstants.FIELD_OBJECT_TYPE_ID );
		}

		return 0;
	}

	public void ReadObjectEnd( string? fieldName, Type objectType )
	{
		Current = Parents.Pop();
	}

	public int ReadCollectionStart( string? fieldName, bool isPrimitive = false )
	{
		if( ContainsKey( fieldName ) )
		{
			JToken? token = Current[ fieldName ];
			if( token is JArray arr )
			{
				Parents.Push( Current );
				Current = arr;

				if( isPrimitive )
				{
					ReadingPrimitiveCollection = true;
					PrimitiveCollectionReadingIndex = 0;
				}

				return arr.Count;
			}
		}

		return -1;
	}

	public void ReadCollectionEnd( string? fieldName, Type objectType, bool isPrimitive = false )
	{
		if( isPrimitive )
		{
			ReadingPrimitiveCollection = false;
		}

		Current = Parents.Pop();
	}
}
