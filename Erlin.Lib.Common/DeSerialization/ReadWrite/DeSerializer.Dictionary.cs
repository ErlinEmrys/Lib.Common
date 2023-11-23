using System.Runtime.CompilerServices;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    DeSerializer for both complex and primitive data - Dictionary support
/// </summary>
partial class DeSerializer
{
	private KeyValuePair<TKey, TValue> ReadWriteKvPair<TKey, TValue>(
		KeyValuePair<TKey, TValue> pair, Func<DeSerializeContext<TKey>, TKey> keyDeSerialization,
		Func<DeSerializeContext<TValue>, TValue> valueDeSerialization, int valueIndex )
		where TKey : notnull
	{
		if( IsWrite )
		{
			Writer.WriteObjectStart( string.Empty, DeSerializeConstants.TYPE_ID_KEY_VALUE_PAIR );

			keyDeSerialization(
				new DeSerializeContext<TKey>(
					this, pair.Key, valueIndex, pair.Key.GetType(), nameof( pair.Key ) ) );

			valueDeSerialization(
				new DeSerializeContext<TValue>(
					this, pair.Value, valueIndex, pair.Value?.GetType() ?? typeof( TValue ),
					nameof( pair.Value ) ) );

			Writer.WriteObjectEnd();
			return pair;
		}

		ushort typeId = Reader.ReadObjectStart( string.Empty, valueIndex );
		if( typeId != DeSerializeConstants.TYPE_ID_KEY_VALUE_PAIR )
		{
			throw new DeSerializeException( "Unexpected type ID for KeyValuePair object!" );
		}

		Type keyType = typeof( TKey );
		Type valueType = typeof( TValue );

		TKey key = keyDeSerialization(
			new DeSerializeContext<TKey>( this, default, valueIndex, keyType, nameof( pair.Key ) ) );

		TValue value = valueDeSerialization(
			new DeSerializeContext<TValue>(
				this, default, valueIndex, valueType,
				nameof( pair.Value ) ) );

		pair = new KeyValuePair<TKey, TValue>( key, value );

		Reader.ReadObjectEnd( string.Empty, typeof( KeyValuePair<TKey, TValue> ) );

		return pair;
	}

	public Dictionary<TKey, TValue> ReadWriteDic<TKey, TValue>(
		Dictionary<TKey, TValue> value, Func<DeSerializeContext<TKey>, TKey> keyDeSerialization,
		Func<DeSerializeContext<TValue>, TValue> valueDeSerialization,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
		where TKey : notnull
	{
		if( IsWrite && value is null )
		{
			throw new DeSerializeException( $"Writing NULL value on expected instance! ArgName: {argumentName}" );
		}

		return ReadWriteDicN( value, keyDeSerialization, valueDeSerialization, argumentName )
			?? throw new DeSerializeException( "Reading NULL value on expected instance!" );
	}

	public Dictionary<TKey, TValue>? ReadWriteDicN<TKey, TValue>(
		Dictionary<TKey, TValue>? value, Func<DeSerializeContext<TKey>, TKey> keyDeSerialization,
		Func<DeSerializeContext<TValue>, TValue> valueDeSerialization,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
		where TKey : notnull
	{
		int count;
		if( IsWrite )
		{
			count = value?.Count ?? -1;
			Writer.WriteCollectionStart( argumentName, count );
			if( value != null )
			{
				int i = 0;
				foreach( KeyValuePair<TKey, TValue> fPair in value )
				{
					ReadWriteKvPair( fPair, keyDeSerialization, valueDeSerialization, i );
					i++;
				}

				Writer.WriteCollectionEnd();
			}

			return value;
		}

		count = Reader.ReadCollectionStart( argumentName );
		if( count >= 0 )
		{
			value = new Dictionary<TKey, TValue>( count );
			for( int i = 0; i < count; i++ )
			{
				KeyValuePair<TKey, TValue> kvp = ReadWriteKvPair(
					new KeyValuePair<TKey, TValue>(), keyDeSerialization, valueDeSerialization, i );

				if( value.ContainsKey( kvp.Key ) )
				{
					throw new DeSerializeException(
						$"Attempt to add same key {kvp.Key} to Dictionary during deserialization! " );
				}

				value.Add( kvp.Key, kvp.Value );
			}

			Reader.ReadCollectionEnd( argumentName, value.GetType() );
		}
		else
		{
			value = null;
		}

		return value;
	}
}
