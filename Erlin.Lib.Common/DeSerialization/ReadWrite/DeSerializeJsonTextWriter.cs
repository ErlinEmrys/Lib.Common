using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    JSON writer of primitive data
/// </summary>
public class DeSerializeJsonTextWriter : IDeSerializeWriter
{
	/// <summary>
	///    Internal writer
	/// </summary>
	private StreamWriter Writer { get; set; }

	/// <summary>
	///    Serialization options
	/// </summary>
	private DeSerializeOptions Options { get; }

	/// <summary>
	///    Current level of indentation
	/// </summary>
	private int CurrentIndentation { get; set; } = 1;

	/// <summary>
	///    Sign, that we are about to write first item inside another or to collection
	/// </summary>
	private bool FirstValue { get; set; } = true;

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
	/// <param name="options"></param>
	public DeSerializeJsonTextWriter( Stream stream, DeSerializeOptions options )
	{
		Options = options;
		SwitchStream( stream );
	}

	/// <summary>
	///    Switches underlying stream to write
	/// </summary>
	/// <param name="stream"></param>
	[MemberNotNull( nameof( DeSerializeJsonTextWriter.Writer ) )]
	public void SwitchStream( Stream stream )
	{
		Dispose();
		Writer = new StreamWriter( stream, Encoding.UTF8, -1, true );
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
	///    Write indentation characters
	/// </summary>
	private void WriteIndent()
	{
		if( Options.JsonFormatted )
		{
			for( int i = 0; i < CurrentIndentation; i++ )
			{
				Writer.Write( Options.JsonIndentation );
			}
		}
	}

	/// <summary>
	///    Write new line characters
	/// </summary>
	public void WriteNewLine()
	{
		Writer.Write( Environment.NewLine );
	}

	/// <summary>
	///    Write NULL value
	/// </summary>
	private void WriteNull()
	{
		Writer.Write( "null" );
	}

	/// <summary>
	///    Write array of data
	/// </summary>
	/// <param name="data"></param>
	/// <param name="allowNull">Sign, whether array items can be NULL</param>
	/// <typeparam name="T"></typeparam>
	/// <exception cref="DeSerializeException"></exception>
	private void WriteArray<T>( IReadOnlyList<T?>? data, bool allowNull )
	{
		if( data == null )
		{
			WriteNull();
			return;
		}

		Writer.Write( "[" );
		if( Options.JsonFormatted && ( data.Count > 0 ) )
		{
			Writer.Write( " " );
		}

		bool isString = typeof( T ) == TypeHelper.TypeString;
		for( int i = 0; i < data.Count; i++ )
		{
			if( !allowNull && data[ i ] is null )
			{
				throw new DeSerializeException( "Attempt to write NULL to non-null field!" );
			}

			if( isString )
			{
				WriteText( data[ i ] as string );
			}
			else
			{
				Writer.Write( data[ i ] );
			}

			if( i != data.Count - 1 )
			{
				Writer.Write( "," );
			}
		}

		if( Options.JsonFormatted && ( data.Count > 0 ) )
		{
			Writer.Write( " " );
		}

		Writer.Write( "]" );
	}

	/// <summary>
	///    Write text value
	/// </summary>
	/// <param name="text"></param>
	private void WriteText( string? text )
	{
		if( text == null )
		{
			WriteNull();
			return;
		}

		Writer.Write( "\"" );
		Writer.Write( DeSerializeJsonTextWriter.EncodeJsonString( text ) );
		Writer.Write( "\"" );
	}

	/// <summary>
	///    Write JSON key part of value
	/// </summary>
	/// <param name="fieldName"></param>
	/// <param name="sameLine"></param>
	private void WriteFieldStart( string? fieldName, bool sameLine = true )
	{
		if( !FirstValue )
		{
			Writer.Write( "," );
		}

		if( !string.IsNullOrEmpty( fieldName ) )
		{
			if( !FirstValue && Options.JsonFormatted )
			{
				WriteNewLine();
			}

			WriteIndent();
			WriteText( fieldName );
			Writer.Write( ":" );
			if( sameLine && Options.JsonFormatted )
			{
				Writer.Write( " " );
			}
		}

		FirstValue = false;
	}

	/// <summary>
	///    Encodes string for storing in JSON file
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public static string EncodeJsonString( string text )
	{
		if( string.IsNullOrEmpty( text ) )
		{
			return text;
		}

		StringBuilder result = new( text.Length );
		foreach( char c in text )
		{
			switch( c )
			{
				case '\\':
				case '"':
				case '/':
					result.Append( '\\' );
					result.Append( c );
					break;

				case '\b':
					result.Append( "\\b" );
					break;

				case '\t':
					result.Append( "\\t" );
					break;

				case '\n':
					result.Append( "\\n" );
					break;

				case '\f':
					result.Append( "\\f" );
					break;

				case '\r':
					result.Append( "\\r" );
					break;

				default:
					result.Append( c );
					break;
			}
		}

		return result.ToString();
	}

	public void WriteBool( string? fieldName, bool value )
	{
		WriteBoolN( fieldName, value );
	}

	public void WriteBoolN( string? fieldName, bool? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write(
				value.Value.ToString()
					.ToLower() );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteSByte( string? fieldName, sbyte value )
	{
		WriteSByteN( fieldName, value );
	}

	public void WriteSByteN( string? fieldName, sbyte? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteSByteArr( string? fieldName, sbyte[] value )
	{
		WriteSByteArrN( fieldName, value );
	}

	public void WriteSByteArrN( string? fieldName, sbyte[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteSByteNArr( string? fieldName, sbyte?[] value )
	{
		WriteSByteNArrN( fieldName, value );
	}

	public void WriteSByteNArrN( string? fieldName, sbyte?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteByte( string? fieldName, byte value )
	{
		WriteByteN( fieldName, value );
	}

	public void WriteByteN( string? fieldName, byte? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteByteArr( string? fieldName, byte[] value )
	{
		WriteByteArrN( fieldName, value );
	}

	public void WriteByteArrN( string? fieldName, byte[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteByteNArr( string? fieldName, byte?[] value )
	{
		WriteByteNArrN( fieldName, value );
	}

	public void WriteByteNArrN( string? fieldName, byte?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteInt16( string? fieldName, short value )
	{
		WriteInt16N( fieldName, value );
	}

	public void WriteInt16N( string? fieldName, short? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteInt16Arr( string? fieldName, short[] value )
	{
		WriteInt16ArrN( fieldName, value );
	}

	public void WriteInt16ArrN( string? fieldName, short[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteInt16NArr( string? fieldName, short?[] value )
	{
		WriteInt16NArrN( fieldName, value );
	}

	public void WriteInt16NArrN( string? fieldName, short?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteUInt16( string? fieldName, ushort value )
	{
		WriteUInt16N( fieldName, value );
	}

	public void WriteUInt16N( string? fieldName, ushort? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteUInt16Arr( string? fieldName, ushort[] value )
	{
		WriteUInt16ArrN( fieldName, value );
	}

	public void WriteUInt16ArrN( string? fieldName, ushort[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteUInt16NArr( string? fieldName, ushort?[] value )
	{
		WriteUInt16NArrN( fieldName, value );
	}

	public void WriteUInt16NArrN( string? fieldName, ushort?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteInt32( string? fieldName, int value )
	{
		WriteInt32N( fieldName, value );
	}

	public void WriteInt32N( string? fieldName, int? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteInt32Arr( string? fieldName, int[] value )
	{
		WriteInt32ArrN( fieldName, value );
	}

	public void WriteInt32ArrN( string? fieldName, int[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteInt32NArr( string? fieldName, int?[] value )
	{
		WriteInt32NArrN( fieldName, value );
	}

	public void WriteInt32NArrN( string? fieldName, int?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteUInt32( string? fieldName, uint value )
	{
		WriteUInt32N( fieldName, value );
	}

	public void WriteUInt32N( string? fieldName, uint? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteUInt32Arr( string? fieldName, uint[] value )
	{
		WriteUInt32ArrN( fieldName, value );
	}

	public void WriteUInt32ArrN( string? fieldName, uint[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteUInt32NArr( string? fieldName, uint?[] value )
	{
		WriteUInt32NArrN( fieldName, value );
	}

	public void WriteUInt32NArrN( string? fieldName, uint?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteInt64( string? fieldName, long value )
	{
		WriteInt64N( fieldName, value );
	}

	public void WriteInt64N( string? fieldName, long? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteInt64Arr( string? fieldName, long[] value )
	{
		WriteInt64ArrN( fieldName, value );
	}

	public void WriteInt64ArrN( string? fieldName, long[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteInt64NArr( string? fieldName, long?[] value )
	{
		WriteInt64NArrN( fieldName, value );
	}

	public void WriteInt64NArrN( string? fieldName, long?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteUInt64( string? fieldName, ulong value )
	{
		WriteUInt64N( fieldName, value );
	}

	public void WriteUInt64N( string? fieldName, ulong? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteUInt64Arr( string? fieldName, ulong[] value )
	{
		WriteUInt64ArrN( fieldName, value );
	}

	public void WriteUInt64ArrN( string? fieldName, ulong[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteUInt64NArr( string? fieldName, ulong?[] value )
	{
		WriteUInt64NArrN( fieldName, value );
	}

	public void WriteUInt64NArrN( string? fieldName, ulong?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteFloat( string? fieldName, float value )
	{
		WriteFloatN( fieldName, value );
	}

	public void WriteFloatN( string? fieldName, float? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteFloatArr( string? fieldName, float[] value )
	{
		WriteFloatArrN( fieldName, value );
	}

	public void WriteFloatArrN( string? fieldName, float[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteFloatNArr( string? fieldName, float?[] value )
	{
		WriteFloatNArrN( fieldName, value );
	}

	public void WriteFloatNArrN( string? fieldName, float?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteDouble( string? fieldName, double value )
	{
		WriteDoubleN( fieldName, value );
	}

	public void WriteDoubleN( string? fieldName, double? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteDoubleArr( string? fieldName, double[] value )
	{
		WriteDoubleArrN( fieldName, value );
	}

	public void WriteDoubleArrN( string? fieldName, double[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteDoubleNArr( string? fieldName, double?[] value )
	{
		WriteDoubleNArrN( fieldName, value );
	}

	public void WriteDoubleNArrN( string? fieldName, double?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteDecimal( string? fieldName, decimal value )
	{
		WriteDecimalN( fieldName, value );
	}

	public void WriteDecimalN( string? fieldName, decimal? value )
	{
		WriteFieldStart( fieldName );

		if( value.HasValue )
		{
			Writer.Write( value.Value );
		}
		else
		{
			WriteNull();
		}
	}

	public void WriteDecimalArr( string? fieldName, decimal[] value )
	{
		WriteDecimalArrN( fieldName, value );
	}

	public void WriteDecimalArrN( string? fieldName, decimal[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteDecimalNArr( string? fieldName, decimal?[] value )
	{
		WriteDecimalNArrN( fieldName, value );
	}

	public void WriteDecimalNArrN( string? fieldName, decimal?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteGuid( string? fieldName, Guid value )
	{
		WriteGuidN( fieldName, value );
	}

	public void WriteGuidN( string? fieldName, Guid? value )
	{
		WriteFieldStart( fieldName );
		WriteText( value?.ToString() );
	}

	public void WriteDateTime( string? fieldName, DateTime value )
	{
		WriteDateTimeN( fieldName, value );
	}

	public void WriteDateTimeN( string? fieldName, DateTime? value )
	{
		WriteFieldStart( fieldName );
		WriteText( value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteDateTimeOffset( string? fieldName, DateTimeOffset value )
	{
		WriteDateTimeOffsetN( fieldName, value );
	}

	public void WriteDateTimeOffsetN( string? fieldName, DateTimeOffset? value )
	{
		WriteFieldStart( fieldName );
		WriteText( value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteTimeSpan( string? fieldName, TimeSpan value )
	{
		WriteTimeSpanN( fieldName, value );
	}

	public void WriteTimeSpanN( string? fieldName, TimeSpan? value )
	{
		WriteFieldStart( fieldName );
		WriteText( value?.ToString( "o", CultureInfo.InvariantCulture ) );
	}

	public void WriteString( string? fieldName, string value )
	{
		WriteStringN( fieldName, value );
	}

	public void WriteStringN( string? fieldName, string? value )
	{
		WriteFieldStart( fieldName );
		WriteText( value );
	}

	public void WriteStringArr( string? fieldName, string[] value )
	{
		WriteStringArrN( fieldName, value );
	}

	public void WriteStringArrN( string? fieldName, string[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, false );
	}

	public void WriteStringNArr( string? fieldName, string?[] value )
	{
		WriteStringNArrN( fieldName, value );
	}

	public void WriteStringNArrN( string? fieldName, string?[]? value )
	{
		WriteFieldStart( fieldName );
		WriteArray( value, true );
	}

	public void WriteObjectEmpty( string? fieldName )
	{
		if( fieldName.IsNotEmpty() )
		{
			WriteFieldStart( fieldName );
		}

		WriteNull();
	}

	public void WriteObjectStart( string? fieldName, ushort shortTypeId )
	{
		WriteFieldStart( fieldName, false );

		if( Options.JsonFormatted )
		{
			WriteNewLine();
			WriteIndent();
		}

		Writer.Write( "{" );

		FirstValue = true;
		if( Options.JsonFormatted )
		{
			WriteNewLine();
			CurrentIndentation++;
		}

		WriteUInt16( DeSerializeConstants.FIELD_OBJECT_TYPE_ID, shortTypeId );
	}

	public void WriteObjectEnd()
	{
		if( Options.JsonFormatted )
		{
			WriteNewLine();
			CurrentIndentation--;
			WriteIndent();
		}

		Writer.Write( "}" );
	}

	public void WriteCollectionStart( string? fieldName, int count, bool isPrimitive = false )
	{
		WriteFieldStart( fieldName, isPrimitive || ( count < 1 ) );

		if( count < 0 )
		{
			WriteNull();
			return;
		}

		if( !isPrimitive && ( count > 0 ) && Options.JsonFormatted )
		{
			WriteNewLine();
			WriteIndent();
		}

		Writer.Write( "[" );
		FirstValue = true;

		if( Options.JsonFormatted )
		{
			if( isPrimitive && ( count > 0 ) )
			{
				Writer.Write( " " );
			}

			CurrentIndentation++;
		}
	}

	public void WriteCollectionEnd( bool isPrimitive = false )
	{
		if( Options.JsonFormatted )
		{
			CurrentIndentation--;
			if( !FirstValue )
			{
				if( isPrimitive )
				{
					Writer.Write( " " );
				}
				else
				{
					WriteNewLine();
					WriteIndent();
				}
			}
		}

		Writer.Write( "]" );
		FirstValue = false;
	}
}
