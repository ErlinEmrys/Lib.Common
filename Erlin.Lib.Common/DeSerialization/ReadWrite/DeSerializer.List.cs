using System.Runtime.CompilerServices;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    DeSerializer for both complex and primitive data - List support
/// </summary>
partial class DeSerializer
{
	private List< T > ReadWriteListImpl< T >( List< T > value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		if( IsWrite && value is null )
		{
			throw new DeSerializeException( $"Writing NULL value on expected instance! ArgName: {argumentName}" );
		}

		return ReadWriteListNImpl( value, itemDeSerialization, isPrimitive, argumentName ) ?? throw new DeSerializeException( "Reading NULL value on expected instance!" );
	}

	private List< T >? ReadWriteListNImpl< T >( List< T >? value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		int count;
		if( IsWrite )
		{
			count = value?.Count ?? -1;
			Writer.WriteCollectionStart( argumentName, count, isPrimitive );
			if( value != null )
			{
				for( int i = 0; i < count; i++ )
				{
					T item = value[ i ];
					itemDeSerialization( new DeSerializeContext< T >( this, item, i, item?.GetType() ?? typeof( T ), null ) );
				}

				Writer.WriteCollectionEnd( isPrimitive );
			}

			return value;
		}

		count = Reader.ReadCollectionStart( argumentName, isPrimitive );
		if( count >= 0 )
		{
			Type valueType = typeof( T );
			value = new List< T >( count );
			for( int i = 0; i < count; i++ )
			{
				T item = itemDeSerialization( new DeSerializeContext< T >( this, default, i, valueType, null ) );
				value.Add( item );
			}

			Reader.ReadCollectionEnd( argumentName, value.GetType(), isPrimitive );
		}
		else
		{
			value = null;
		}

		return value;
	}

	public List< T > ReadWriteList< T >( List< T > value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, isPrimitive ? itemDeSerialization : c => ReadWrite( c.GetValue(), itemDeSerialization, c.ArgumentName, c.ValueIndex ), isPrimitive, argumentName );
	}

	public List< T >? ReadWriteListN< T >( List< T >? value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, isPrimitive ? itemDeSerialization : c => ReadWrite( c.GetValue(), itemDeSerialization, c.ArgumentName, c.ValueIndex ), isPrimitive, argumentName );
	}

	public List< T > ReadWriteList< T >( DeSerializeContext< List< T > > context )
		where T : IDeSerializable
	{
		return ReadWriteList( context.GetValue(), context.ArgumentName );
	}

	public List< T > ReadWriteList< T >( List< T > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable
	{
		return ReadWriteListImpl( value, ReadWrite, false, argumentName );
	}

	public List< T >? ReadWriteListN< T >( DeSerializeContext< List< T >? > context )
		where T : IDeSerializable
	{
		return ReadWriteListN( context.GetValue(), context.ArgumentName );
	}

	public List< T >? ReadWriteListN< T >( List< T >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable
	{
		return ReadWriteListNImpl( value, ReadWrite, false, argumentName );
	}

	public List< string > ReadWriteStringList( DeSerializeContext< List< string > > context )
	{
		return ReadWriteStringList( context.GetValue(), context.ArgumentName );
	}

	public List< string > ReadWriteStringList( List< string > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteString, true, argumentName );
	}

	public List< string >? ReadWriteStringListN( DeSerializeContext< List< string >? > context )
	{
		return ReadWriteStringListN( context.GetValue(), context.ArgumentName );
	}

	public List< string >? ReadWriteStringListN( List< string >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteString, true, argumentName );
	}

	public List< string? > ReadWriteStringNList( DeSerializeContext< List< string? > > context )
	{
		return ReadWriteStringNList( context.GetValue(), context.ArgumentName );
	}

	public List< string? > ReadWriteStringNList( List< string? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteStringN, true, argumentName );
	}

	public List< string? >? ReadWriteStringNListN( DeSerializeContext< List< string? >? > context )
	{
		return ReadWriteStringNListN( context.GetValue(), context.ArgumentName );
	}

	public List< string? >? ReadWriteStringNListN( List< string? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteStringN, true, argumentName );
	}

	public List< sbyte > ReadWriteSByteList( DeSerializeContext< List< sbyte > > context )
	{
		return ReadWriteSByteList( context.GetValue(), context.ArgumentName );
	}

	public List< sbyte > ReadWriteSByteList( List< sbyte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteSByte, true, argumentName );
	}

	public List< sbyte >? ReadWriteSByteListN( DeSerializeContext< List< sbyte >? > context )
	{
		return ReadWriteSByteListN( context.GetValue(), context.ArgumentName );
	}

	public List< sbyte >? ReadWriteSByteListN( List< sbyte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteSByte, true, argumentName );
	}

	public List< sbyte? > ReadWriteSByteNList( DeSerializeContext< List< sbyte? > > context )
	{
		return ReadWriteSByteNList( context.GetValue(), context.ArgumentName );
	}

	public List< sbyte? > ReadWriteSByteNList( List< sbyte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteSByteN, true, argumentName );
	}

	public List< sbyte? >? ReadWriteSByteNListN( DeSerializeContext< List< sbyte? >? > context )
	{
		return ReadWriteSByteNListN( context.GetValue(), context.ArgumentName );
	}

	public List< sbyte? >? ReadWriteSByteNListN( List< sbyte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteSByteN, true, argumentName );
	}

	public List< byte > ReadWriteByteList( DeSerializeContext< List< byte > > context )
	{
		return ReadWriteByteList( context.GetValue(), context.ArgumentName );
	}

	public List< byte > ReadWriteByteList( List< byte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteByte, true, argumentName );
	}

	public List< byte >? ReadWriteByteListN( DeSerializeContext< List< byte >? > context )
	{
		return ReadWriteByteListN( context.GetValue(), context.ArgumentName );
	}

	public List< byte >? ReadWriteByteListN( List< byte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteByte, true, argumentName );
	}

	public List< byte? > ReadWriteByteNList( DeSerializeContext< List< byte? > > context )
	{
		return ReadWriteByteNList( context.GetValue(), context.ArgumentName );
	}

	public List< byte? > ReadWriteByteNList( List< byte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteByteN, true, argumentName );
	}

	public List< byte? >? ReadWriteByteNListN( DeSerializeContext< List< byte? >? > context )
	{
		return ReadWriteByteNListN( context.GetValue(), context.ArgumentName );
	}

	public List< byte? >? ReadWriteByteNListN( List< byte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteByteN, true, argumentName );
	}

	public List< short > ReadWriteInt16List( DeSerializeContext< List< short > > context )
	{
		return ReadWriteInt16List( context.GetValue(), context.ArgumentName );
	}

	public List< short > ReadWriteInt16List( List< short > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt16, true, argumentName );
	}

	public List< short >? ReadWriteInt16ListN( DeSerializeContext< List< short >? > context )
	{
		return ReadWriteInt16ListN( context.GetValue(), context.ArgumentName );
	}

	public List< short >? ReadWriteInt16ListN( List< short >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt16, true, argumentName );
	}

	public List< short? > ReadWriteInt16NList( DeSerializeContext< List< short? > > context )
	{
		return ReadWriteInt16NList( context.GetValue(), context.ArgumentName );
	}

	public List< short? > ReadWriteInt16NList( List< short? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt16N, true, argumentName );
	}

	public List< short? >? ReadWriteInt16NListN( DeSerializeContext< List< short? >? > context )
	{
		return ReadWriteInt16NListN( context.GetValue(), context.ArgumentName );
	}

	public List< short? >? ReadWriteInt16NListN( List< short? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt16N, true, argumentName );
	}

	public List< ushort > ReadWriteUInt16List( DeSerializeContext< List< ushort > > context )
	{
		return ReadWriteUInt16List( context.GetValue(), context.ArgumentName );
	}

	public List< ushort > ReadWriteUInt16List( List< ushort > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt16, true, argumentName );
	}

	public List< ushort >? ReadWriteUInt16ListN( DeSerializeContext< List< ushort >? > context )
	{
		return ReadWriteUInt16ListN( context.GetValue(), context.ArgumentName );
	}

	public List< ushort >? ReadWriteUInt16ListN( List< ushort >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt16, true, argumentName );
	}

	public List< ushort? > ReadWriteUInt16NList( DeSerializeContext< List< ushort? > > context )
	{
		return ReadWriteUInt16NList( context.GetValue(), context.ArgumentName );
	}

	public List< ushort? > ReadWriteUInt16NList( List< ushort? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt16N, true, argumentName );
	}

	public List< ushort? >? ReadWriteUInt16NListN( DeSerializeContext< List< ushort? >? > context )
	{
		return ReadWriteUInt16NListN( context.GetValue(), context.ArgumentName );
	}

	public List< ushort? >? ReadWriteUInt16NListN( List< ushort? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt16N, true, argumentName );
	}

	public List< int > ReadWriteInt32List( DeSerializeContext< List< int > > context )
	{
		return ReadWriteInt32List( context.GetValue(), context.ArgumentName );
	}

	public List< int > ReadWriteInt32List( List< int > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt32, true, argumentName );
	}

	public List< int >? ReadWriteInt32ListN( DeSerializeContext< List< int >? > context )
	{
		return ReadWriteInt32ListN( context.GetValue(), context.ArgumentName );
	}

	public List< int >? ReadWriteInt32ListN( List< int >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt32, true, argumentName );
	}

	public List< int? > ReadWriteInt32NList( DeSerializeContext< List< int? > > context )
	{
		return ReadWriteInt32NList( context.GetValue(), context.ArgumentName );
	}

	public List< int? > ReadWriteInt32NList( List< int? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt32N, true, argumentName );
	}

	public List< int? >? ReadWriteInt32NListN( DeSerializeContext< List< int? >? > context )
	{
		return ReadWriteInt32NListN( context.GetValue(), context.ArgumentName );
	}

	public List< int? >? ReadWriteInt32NListN( List< int? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt32N, true, argumentName );
	}

	public List< uint > ReadWriteUInt32List( DeSerializeContext< List< uint > > context )
	{
		return ReadWriteUInt32List( context.GetValue(), context.ArgumentName );
	}

	public List< uint > ReadWriteUInt32List( List< uint > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt32, true, argumentName );
	}

	public List< uint >? ReadWriteUInt32ListN( DeSerializeContext< List< uint >? > context )
	{
		return ReadWriteUInt32ListN( context.GetValue(), context.ArgumentName );
	}

	public List< uint >? ReadWriteUInt32ListN( List< uint >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt32, true, argumentName );
	}

	public List< uint? > ReadWriteUInt32NList( DeSerializeContext< List< uint? > > context )
	{
		return ReadWriteUInt32NList( context.GetValue(), context.ArgumentName );
	}

	public List< uint? > ReadWriteUInt32NList( List< uint? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt32N, true, argumentName );
	}

	public List< uint? >? ReadWriteUInt32NListN( DeSerializeContext< List< uint? >? > context )
	{
		return ReadWriteUInt32NListN( context.GetValue(), context.ArgumentName );
	}

	public List< uint? >? ReadWriteUInt32NListN( List< uint? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt32N, true, argumentName );
	}

	public List< long > ReadWriteInt64List( DeSerializeContext< List< long > > context )
	{
		return ReadWriteInt64List( context.GetValue(), context.ArgumentName );
	}

	public List< long > ReadWriteInt64List( List< long > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt64, true, argumentName );
	}

	public List< long >? ReadWriteInt64ListN( DeSerializeContext< List< long >? > context )
	{
		return ReadWriteInt64ListN( context.GetValue(), context.ArgumentName );
	}

	public List< long >? ReadWriteInt64ListN( List< long >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt64, true, argumentName );
	}

	public List< long? > ReadWriteInt64NList( DeSerializeContext< List< long? > > context )
	{
		return ReadWriteInt64NList( context.GetValue(), context.ArgumentName );
	}

	public List< long? > ReadWriteInt64NList( List< long? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteInt64N, true, argumentName );
	}

	public List< long? >? ReadWriteInt64NListN( DeSerializeContext< List< long? >? > context )
	{
		return ReadWriteInt64NListN( context.GetValue(), context.ArgumentName );
	}

	public List< long? >? ReadWriteInt64NListN( List< long? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteInt64N, true, argumentName );
	}

	public List< ulong > ReadWriteUInt64List( DeSerializeContext< List< ulong > > context )
	{
		return ReadWriteUInt64List( context.GetValue(), context.ArgumentName );
	}

	public List< ulong > ReadWriteUInt64List( List< ulong > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt64, true, argumentName );
	}

	public List< ulong >? ReadWriteUInt64ListN( DeSerializeContext< List< ulong >? > context )
	{
		return ReadWriteUInt64ListN( context.GetValue(), context.ArgumentName );
	}

	public List< ulong >? ReadWriteUInt64ListN( List< ulong >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt64, true, argumentName );
	}

	public List< ulong? > ReadWriteUInt64NList( DeSerializeContext< List< ulong? > > context )
	{
		return ReadWriteUInt64NList( context.GetValue(), context.ArgumentName );
	}

	public List< ulong? > ReadWriteUInt64NList( List< ulong? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteUInt64N, true, argumentName );
	}

	public List< ulong? >? ReadWriteUInt64NListN( DeSerializeContext< List< ulong? >? > context )
	{
		return ReadWriteUInt64NListN( context.GetValue(), context.ArgumentName );
	}

	public List< ulong? >? ReadWriteUInt64NListN( List< ulong? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteUInt64N, true, argumentName );
	}

	public List< float > ReadWriteFloatList( DeSerializeContext< List< float > > context )
	{
		return ReadWriteFloatList( context.GetValue(), context.ArgumentName );
	}

	public List< float > ReadWriteFloatList( List< float > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteFloat, true, argumentName );
	}

	public List< float >? ReadWriteFloatListN( DeSerializeContext< List< float >? > context )
	{
		return ReadWriteFloatListN( context.GetValue(), context.ArgumentName );
	}

	public List< float >? ReadWriteFloatListN( List< float >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteFloat, true, argumentName );
	}

	public List< float? > ReadWriteFloatNList( DeSerializeContext< List< float? > > context )
	{
		return ReadWriteFloatNList( context.GetValue(), context.ArgumentName );
	}

	public List< float? > ReadWriteFloatNList( List< float? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteFloatN, true, argumentName );
	}

	public List< float? >? ReadWriteFloatNListN( DeSerializeContext< List< float? >? > context )
	{
		return ReadWriteFloatNListN( context.GetValue(), context.ArgumentName );
	}

	public List< float? >? ReadWriteFloatNListN( List< float? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteFloatN, true, argumentName );
	}

	public List< double > ReadWriteDoubleList( DeSerializeContext< List< double > > context )
	{
		return ReadWriteDoubleList( context.GetValue(), context.ArgumentName );
	}

	public List< double > ReadWriteDoubleList( List< double > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteDouble, true, argumentName );
	}

	public List< double >? ReadWriteDoubleListN( DeSerializeContext< List< double >? > context )
	{
		return ReadWriteDoubleListN( context.GetValue(), context.ArgumentName );
	}

	public List< double >? ReadWriteDoubleListN( List< double >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteDouble, true, argumentName );
	}

	public List< double? > ReadWriteDoubleNList( DeSerializeContext< List< double? > > context )
	{
		return ReadWriteDoubleNList( context.GetValue(), context.ArgumentName );
	}

	public List< double? > ReadWriteDoubleNList( List< double? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteDoubleN, true, argumentName );
	}

	public List< double? >? ReadWriteDoubleNListN( DeSerializeContext< List< double? >? > context )
	{
		return ReadWriteDoubleNListN( context.GetValue(), context.ArgumentName );
	}

	public List< double? >? ReadWriteDoubleNListN( List< double? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteDoubleN, true, argumentName );
	}

	public List< decimal > ReadWriteDecimalList( DeSerializeContext< List< decimal > > context )
	{
		return ReadWriteDecimalList( context.GetValue(), context.ArgumentName );
	}

	public List< decimal > ReadWriteDecimalList( List< decimal > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteDecimal, true, argumentName );
	}

	public List< decimal >? ReadWriteDecimalListN( DeSerializeContext< List< decimal >? > context )
	{
		return ReadWriteDecimalListN( context.GetValue(), context.ArgumentName );
	}

	public List< decimal >? ReadWriteDecimalListN( List< decimal >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteDecimal, true, argumentName );
	}

	public List< decimal? > ReadWriteDecimalNList( DeSerializeContext< List< decimal? > > context )
	{
		return ReadWriteDecimalNList( context.GetValue(), context.ArgumentName );
	}

	public List< decimal? > ReadWriteDecimalNList( List< decimal? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListImpl( value, ReadWriteDecimalN, true, argumentName );
	}

	public List< decimal? >? ReadWriteDecimalNListN( DeSerializeContext< List< decimal? >? > context )
	{
		return ReadWriteDecimalNListN( context.GetValue(), context.ArgumentName );
	}

	public List< decimal? >? ReadWriteDecimalNListN( List< decimal? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
	{
		return ReadWriteListNImpl( value, ReadWriteDecimalN, true, argumentName );
	}
}
