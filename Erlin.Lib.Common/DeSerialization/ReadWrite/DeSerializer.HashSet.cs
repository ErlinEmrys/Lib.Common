using System.Runtime.CompilerServices;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    DeSerializer for both complex and primitive data - HashSet support
/// </summary>
partial class DeSerializer
{
	private HashSet<T> ReadWriteHashSetImpl<T>(
		HashSet<T> value, Func<DeSerializeContext<T>, T> itemDeSerialization, bool isPrimitive = false,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		if( IsWrite && value is null )
		{
			throw new DeSerializeException( $"Writing NULL value on expected instance! ArgName: {argumentName}" );
		}

		return ReadWriteHashSetNImpl( value, itemDeSerialization, isPrimitive, argumentName )
			?? throw new DeSerializeException( "Reading NULL value on expected instance!" );
	}

	private HashSet<T>? ReadWriteHashSetNImpl<T>(
		HashSet<T>? value, Func<DeSerializeContext<T>, T> itemDeSerialization, bool isPrimitive = false,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		int count;
		if( IsWrite )
		{
			count = value?.Count ?? -1;
			Writer.WriteCollectionStart( argumentName, count, isPrimitive );
			if( value != null )
			{
				int i = 0;
				foreach( T fValue in value )
				{
					itemDeSerialization(
						new DeSerializeContext<T>( this, fValue, i, fValue?.GetType() ?? typeof( T ), null ) );

					i++;
				}

				Writer.WriteCollectionEnd( isPrimitive );
			}

			return value;
		}

		count = Reader.ReadCollectionStart( argumentName, isPrimitive );
		if( count >= 0 )
		{
			Type valueType = typeof( T );
			value = new HashSet<T>( count );
			for( int i = 0; i < count; i++ )
			{
				T item = itemDeSerialization(
					new DeSerializeContext<T>( this, default, i, valueType, argumentName ) );

				if( !value.Add( item ) )
				{
					throw new DeSerializeException(
						$"Attempt to add existing value {item} to HashSet during deserialization! " );
				}
			}

			Reader.ReadCollectionEnd( argumentName, value.GetType(), isPrimitive );
		}
		else
		{
			value = null;
		}

		return value;
	}

	public HashSet<T> ReadWriteHashSet<T>(
		HashSet<T> value, Func<DeSerializeContext<T>, T> itemDeSerialization, bool isPrimitive = false,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		return ReadWriteHashSetImpl(
			value,
			isPrimitive ? itemDeSerialization : c => ReadWrite(
				c.GetValue(), itemDeSerialization, c.ArgumentName, c.ValueIndex ),
			isPrimitive,
			argumentName );
	}

	public HashSet<T>? ReadWriteHashSetN<T>(
		HashSet<T>? value, Func<DeSerializeContext<T>, T> itemDeSerialization, bool isPrimitive = false,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		return ReadWriteHashSetNImpl(
			value,
			isPrimitive ? itemDeSerialization : c => ReadWrite(
				c.GetValue(), itemDeSerialization, c.ArgumentName, c.ValueIndex ),
			isPrimitive,
			argumentName );
	}

	public HashSet<T> ReadWriteHashSet<T>( DeSerializeContext<HashSet<T>> context )
		where T : IDeSerializable
	{
		return ReadWriteHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<T> ReadWriteHashSet<T>(
		HashSet<T> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
		where T : IDeSerializable
	{
		return ReadWriteHashSetImpl( value, ReadWrite, false, argumentName );
	}

	public HashSet<T>? ReadWriteHashSetN<T>( DeSerializeContext<HashSet<T>?> context )
		where T : IDeSerializable
	{
		return ReadWriteHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<T>? ReadWriteHashSetN<T>(
		HashSet<T>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
		where T : IDeSerializable
	{
		return ReadWriteHashSetNImpl( value, ReadWrite, false, argumentName );
	}

	public HashSet<string> ReadWriteStringHashSet( DeSerializeContext<HashSet<string>> context )
	{
		return ReadWriteStringHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<string> ReadWriteStringHashSet(
		HashSet<string> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteString, true, argumentName );
	}

	public HashSet<string>? ReadWriteStringHashSetN( DeSerializeContext<HashSet<string>?> context )
	{
		return ReadWriteStringHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<string>? ReadWriteStringHashSetN(
		HashSet<string>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteString, true, argumentName );
	}

	public HashSet<string?> ReadWriteStringNHashSet( DeSerializeContext<HashSet<string?>> context )
	{
		return ReadWriteStringNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<string?> ReadWriteStringNHashSet(
		HashSet<string?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteStringN, true, argumentName );
	}

	public HashSet<string?>? ReadWriteStringNHashSetN( DeSerializeContext<HashSet<string?>?> context )
	{
		return ReadWriteStringNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<string?>? ReadWriteStringNHashSetN(
		HashSet<string?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteStringN, true, argumentName );
	}

	public HashSet<sbyte> ReadWriteSByteHashSet( DeSerializeContext<HashSet<sbyte>> context )
	{
		return ReadWriteSByteHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<sbyte> ReadWriteSByteHashSet(
		HashSet<sbyte> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteSByte, true, argumentName );
	}

	public HashSet<sbyte>? ReadWriteSByteHashSetN( DeSerializeContext<HashSet<sbyte>?> context )
	{
		return ReadWriteSByteHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<sbyte>? ReadWriteSByteHashSetN(
		HashSet<sbyte>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteSByte, true, argumentName );
	}

	public HashSet<sbyte?> ReadWriteSByteNHashSet( DeSerializeContext<HashSet<sbyte?>> context )
	{
		return ReadWriteSByteNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<sbyte?> ReadWriteSByteNHashSet(
		HashSet<sbyte?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteSByteN, true, argumentName );
	}

	public HashSet<sbyte?>? ReadWriteSByteNHashSetN( DeSerializeContext<HashSet<sbyte?>?> context )
	{
		return ReadWriteSByteNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<sbyte?>? ReadWriteSByteNHashSetN(
		HashSet<sbyte?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteSByteN, true, argumentName );
	}

	public HashSet<byte> ReadWriteByteHashSet( DeSerializeContext<HashSet<byte>> context )
	{
		return ReadWriteByteHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<byte> ReadWriteByteHashSet(
		HashSet<byte> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteByte, true, argumentName );
	}

	public HashSet<byte>? ReadWriteByteHashSetN( DeSerializeContext<HashSet<byte>?> context )
	{
		return ReadWriteByteHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<byte>? ReadWriteByteHashSetN(
		HashSet<byte>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteByte, true, argumentName );
	}

	public HashSet<byte?> ReadWriteByteNHashSet( DeSerializeContext<HashSet<byte?>> context )
	{
		return ReadWriteByteNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<byte?> ReadWriteByteNHashSet(
		HashSet<byte?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteByteN, true, argumentName );
	}

	public HashSet<byte?>? ReadWriteByteNHashSetN( DeSerializeContext<HashSet<byte?>?> context )
	{
		return ReadWriteByteNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<byte?>? ReadWriteByteNHashSetN(
		HashSet<byte?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteByteN, true, argumentName );
	}

	public HashSet<short> ReadWriteInt16HashSet( DeSerializeContext<HashSet<short>> context )
	{
		return ReadWriteInt16HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<short> ReadWriteInt16HashSet(
		HashSet<short> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt16, true, argumentName );
	}

	public HashSet<short>? ReadWriteInt16HashSetN( DeSerializeContext<HashSet<short>?> context )
	{
		return ReadWriteInt16HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<short>? ReadWriteInt16HashSetN(
		HashSet<short>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt16, true, argumentName );
	}

	public HashSet<short?> ReadWriteInt16NHashSet( DeSerializeContext<HashSet<short?>> context )
	{
		return ReadWriteInt16NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<short?> ReadWriteInt16NHashSet(
		HashSet<short?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt16N, true, argumentName );
	}

	public HashSet<short?>? ReadWriteInt16NHashSetN( DeSerializeContext<HashSet<short?>?> context )
	{
		return ReadWriteInt16NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<short?>? ReadWriteInt16NHashSetN(
		HashSet<short?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt16N, true, argumentName );
	}

	public HashSet<ushort> ReadWriteUInt16HashSet( DeSerializeContext<HashSet<ushort>> context )
	{
		return ReadWriteUInt16HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ushort> ReadWriteUInt16HashSet(
		HashSet<ushort> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt16, true, argumentName );
	}

	public HashSet<ushort>? ReadWriteUInt16HashSetN( DeSerializeContext<HashSet<ushort>?> context )
	{
		return ReadWriteUInt16HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ushort>? ReadWriteUInt16HashSetN(
		HashSet<ushort>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt16, true, argumentName );
	}

	public HashSet<ushort?> ReadWriteUInt16NHashSet( DeSerializeContext<HashSet<ushort?>> context )
	{
		return ReadWriteUInt16NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ushort?> ReadWriteUInt16NHashSet(
		HashSet<ushort?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt16N, true, argumentName );
	}

	public HashSet<ushort?>? ReadWriteUInt16NHashSetN( DeSerializeContext<HashSet<ushort?>?> context )
	{
		return ReadWriteUInt16NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ushort?>? ReadWriteUInt16NHashSetN(
		HashSet<ushort?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt16N, true, argumentName );
	}

	public HashSet<int> ReadWriteInt32HashSet( DeSerializeContext<HashSet<int>> context )
	{
		return ReadWriteInt32HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<int> ReadWriteInt32HashSet(
		HashSet<int> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt32, true, argumentName );
	}

	public HashSet<int>? ReadWriteInt32HashSetN( DeSerializeContext<HashSet<int>?> context )
	{
		return ReadWriteInt32HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<int>? ReadWriteInt32HashSetN(
		HashSet<int>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt32, true, argumentName );
	}

	public HashSet<int?> ReadWriteInt32NHashSet( DeSerializeContext<HashSet<int?>> context )
	{
		return ReadWriteInt32NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<int?> ReadWriteInt32NHashSet(
		HashSet<int?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt32N, true, argumentName );
	}

	public HashSet<int?>? ReadWriteInt32NHashSetN( DeSerializeContext<HashSet<int?>?> context )
	{
		return ReadWriteInt32NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<int?>? ReadWriteInt32NHashSetN(
		HashSet<int?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt32N, true, argumentName );
	}

	public HashSet<uint> ReadWriteUInt32HashSet( DeSerializeContext<HashSet<uint>> context )
	{
		return ReadWriteUInt32HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<uint> ReadWriteUInt32HashSet(
		HashSet<uint> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt32, true, argumentName );
	}

	public HashSet<uint>? ReadWriteUInt32HashSetN( DeSerializeContext<HashSet<uint>?> context )
	{
		return ReadWriteUInt32HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<uint>? ReadWriteUInt32HashSetN(
		HashSet<uint>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt32, true, argumentName );
	}

	public HashSet<uint?> ReadWriteUInt32NHashSet( DeSerializeContext<HashSet<uint?>> context )
	{
		return ReadWriteUInt32NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<uint?> ReadWriteUInt32NHashSet(
		HashSet<uint?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt32N, true, argumentName );
	}

	public HashSet<uint?>? ReadWriteUInt32NHashSetN( DeSerializeContext<HashSet<uint?>?> context )
	{
		return ReadWriteUInt32NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<uint?>? ReadWriteUInt32NHashSetN(
		HashSet<uint?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt32N, true, argumentName );
	}

	public HashSet<long> ReadWriteInt64HashSet( DeSerializeContext<HashSet<long>> context )
	{
		return ReadWriteInt64HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<long> ReadWriteInt64HashSet(
		HashSet<long> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt64, true, argumentName );
	}

	public HashSet<long>? ReadWriteInt64HashSetN( DeSerializeContext<HashSet<long>?> context )
	{
		return ReadWriteInt64HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<long>? ReadWriteInt64HashSetN(
		HashSet<long>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt64, true, argumentName );
	}

	public HashSet<long?> ReadWriteInt64NHashSet( DeSerializeContext<HashSet<long?>> context )
	{
		return ReadWriteInt64NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<long?> ReadWriteInt64NHashSet(
		HashSet<long?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteInt64N, true, argumentName );
	}

	public HashSet<long?>? ReadWriteInt64NHashSetN( DeSerializeContext<HashSet<long?>?> context )
	{
		return ReadWriteInt64NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<long?>? ReadWriteInt64NHashSetN(
		HashSet<long?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteInt64N, true, argumentName );
	}

	public HashSet<ulong> ReadWriteUInt64HashSet( DeSerializeContext<HashSet<ulong>> context )
	{
		return ReadWriteUInt64HashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ulong> ReadWriteUInt64HashSet(
		HashSet<ulong> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt64, true, argumentName );
	}

	public HashSet<ulong>? ReadWriteUInt64HashSetN( DeSerializeContext<HashSet<ulong>?> context )
	{
		return ReadWriteUInt64HashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ulong>? ReadWriteUInt64HashSetN(
		HashSet<ulong>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt64, true, argumentName );
	}

	public HashSet<ulong?> ReadWriteUInt64NHashSet( DeSerializeContext<HashSet<ulong?>> context )
	{
		return ReadWriteUInt64NHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ulong?> ReadWriteUInt64NHashSet(
		HashSet<ulong?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteUInt64N, true, argumentName );
	}

	public HashSet<ulong?>? ReadWriteUInt64NHashSetN( DeSerializeContext<HashSet<ulong?>?> context )
	{
		return ReadWriteUInt64NHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<ulong?>? ReadWriteUInt64NHashSetN(
		HashSet<ulong?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteUInt64N, true, argumentName );
	}

	public HashSet<float> ReadWriteFloatHashSet( DeSerializeContext<HashSet<float>> context )
	{
		return ReadWriteFloatHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<float> ReadWriteFloatHashSet(
		HashSet<float> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteFloat, true, argumentName );
	}

	public HashSet<float>? ReadWriteFloatHashSetN( DeSerializeContext<HashSet<float>?> context )
	{
		return ReadWriteFloatHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<float>? ReadWriteFloatHashSetN(
		HashSet<float>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteFloat, true, argumentName );
	}

	public HashSet<float?> ReadWriteFloatNHashSet( DeSerializeContext<HashSet<float?>> context )
	{
		return ReadWriteFloatNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<float?> ReadWriteFloatNHashSet(
		HashSet<float?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteFloatN, true, argumentName );
	}

	public HashSet<float?>? ReadWriteFloatNHashSetN( DeSerializeContext<HashSet<float?>?> context )
	{
		return ReadWriteFloatNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<float?>? ReadWriteFloatNHashSetN(
		HashSet<float?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteFloatN, true, argumentName );
	}

	public HashSet<double> ReadWriteDoubleHashSet( DeSerializeContext<HashSet<double>> context )
	{
		return ReadWriteDoubleHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<double> ReadWriteDoubleHashSet(
		HashSet<double> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteDouble, true, argumentName );
	}

	public HashSet<double>? ReadWriteDoubleHashSetN( DeSerializeContext<HashSet<double>?> context )
	{
		return ReadWriteDoubleHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<double>? ReadWriteDoubleHashSetN(
		HashSet<double>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteDouble, true, argumentName );
	}

	public HashSet<double?> ReadWriteDoubleNHashSet( DeSerializeContext<HashSet<double?>> context )
	{
		return ReadWriteDoubleNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<double?> ReadWriteDoubleNHashSet(
		HashSet<double?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteDoubleN, true, argumentName );
	}

	public HashSet<double?>? ReadWriteDoubleNHashSetN( DeSerializeContext<HashSet<double?>?> context )
	{
		return ReadWriteDoubleNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<double?>? ReadWriteDoubleNHashSetN(
		HashSet<double?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteDoubleN, true, argumentName );
	}

	public HashSet<decimal> ReadWriteDecimalHashSet( DeSerializeContext<HashSet<decimal>> context )
	{
		return ReadWriteDecimalHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<decimal> ReadWriteDecimalHashSet(
		HashSet<decimal> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteDecimal, true, argumentName );
	}

	public HashSet<decimal>? ReadWriteDecimalHashSetN( DeSerializeContext<HashSet<decimal>?> context )
	{
		return ReadWriteDecimalHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<decimal>? ReadWriteDecimalHashSetN(
		HashSet<decimal>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteDecimal, true, argumentName );
	}

	public HashSet<decimal?> ReadWriteDecimalNHashSet( DeSerializeContext<HashSet<decimal?>> context )
	{
		return ReadWriteDecimalNHashSet( context.GetValue(), context.ArgumentName );
	}

	public HashSet<decimal?> ReadWriteDecimalNHashSet(
		HashSet<decimal?> value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetImpl( value, ReadWriteDecimalN, true, argumentName );
	}

	public HashSet<decimal?>? ReadWriteDecimalNHashSetN( DeSerializeContext<HashSet<decimal?>?> context )
	{
		return ReadWriteDecimalNHashSetN( context.GetValue(), context.ArgumentName );
	}

	public HashSet<decimal?>? ReadWriteDecimalNHashSetN(
		HashSet<decimal?>? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		return ReadWriteHashSetNImpl( value, ReadWriteDecimalN, true, argumentName );
	}
}
