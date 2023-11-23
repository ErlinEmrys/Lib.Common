using System.Runtime.CompilerServices;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    DeSerializer for both complex and primitive data
/// </summary>
public partial class DeSerializer : IDeSerializer
{
	private Stack<ushort> ShortRuntimeTypes { get; } = new();

	/// <summary>
	///    Reader or Writer of elementary values
	/// </summary>
	public IDeSerializeReader Reader { get; }

	/// <summary>
	///    Reader or Writer of elementary values
	/// </summary>
	public IDeSerializeWriter Writer { get; }

	/// <summary>
	///    Indicates that DeSerializer is reading
	/// </summary>
	public bool IsRead
	{
		get
		{
			if( Reader.ImplementsRead && Writer.ImplementsWrite )
			{
				throw new InvalidOperationException(
					"DeSerializer implements both reading and writing at the same time!" );
			}

			return Reader.ImplementsRead;
		}
	}

	/// <summary>
	///    Indicates that DeSerializer is writing
	/// </summary>
	public bool IsWrite
	{
		get { return !IsRead; }
	}

	/// <summary>
	///    Provider of runtime types
	/// </summary>
	public IDeSerializeTypeProvider TypeProvider { get; }

	/// <summary>
	///    Optional parameters of De/Serialization
	/// </summary>
	public Dictionary<string, string> Params { get; } = new();

	public DeSerializer(
		IDeSerializeTypeProvider typeProvider, IDeSerializeWriter writer, IDeSerializeReader reader )
	{
		TypeProvider = typeProvider;
		Writer = writer;
		Reader = reader;
	}

	/// <summary>
	///    Release all resources
	/// </summary>
	public void Dispose()
	{
		Writer.Dispose();
	}

	public T ReadWrite<T>( DeSerializeContext<T> context )
		where T : IDeSerializable
	{
		return ReadWrite( context.GetValue(), context.ArgumentName, context.ValueIndex );
	}

	public T ReadWrite<T>(
		T value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null,
		int? valueIndex = null )
		where T : IDeSerializable
	{
		return ReadWrite(
			value, c =>
			{
				T item = c.GetValue( () => ( T? )Activator.CreateInstance( c.ValueRuntimeType, this ) );
				if( c.DS.IsWrite )
				{
					item.DeSerialize( this );
				}

				return item;
			}, argumentName, valueIndex );
	}

	public T? ReadWriteN<T>( DeSerializeContext<T?> context )
		where T : IDeSerializable
	{
		return ReadWriteN( context.GetValue(), context.ArgumentName, context.ValueIndex );
	}

	public T? ReadWriteN<T>(
		T? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null,
		int? valueIndex = null )
		where T : IDeSerializable
	{
		return ReadWriteN(
			value, c =>
			{
				T item = c.GetValue( () => ( T? )Activator.CreateInstance( c.ValueRuntimeType, this ) );
				if( c.DS.IsWrite )
				{
					item.DeSerialize( this );
				}

				return item;
			}, argumentName, valueIndex );
	}

	public T ReadWrite<T>(
		T value, Func<DeSerializeContext<T>, T> objectDeSerialization,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null, int? valueIndex = null )
	{
		if( IsWrite && value is null )
		{
			throw new DeSerializeException( $"Writing NULL value on expected instance! ArgName: {argumentName}" );
		}

		T? read = ReadWriteN( value, objectDeSerialization, argumentName, valueIndex );
		if( read is null )
		{
			throw new DeSerializeException( "Reading NULL value on expected instance!" );
		}

		return read;
	}

	public T? ReadWriteN<T>(
		T? value, Func<DeSerializeContext<T>, T> objectDeSerialization,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null, int? valueIndex = null )
	{
		if( IsWrite )
		{
			if( value?.Equals( default( T? ) ) ?? true )
			{
				Writer.WriteObjectEmpty( argumentName );
			}
			else
			{
				Type valueType = value.GetType();
				DeSerializeType type = TypeProvider.EnsureType( valueType );
				Writer.WriteObjectStart( argumentName, type.ShortId );
				DeSerializeContext<T> context = new( this, value, valueIndex, valueType, argumentName );
				objectDeSerialization( context );
				Writer.WriteObjectEnd();
			}

			return value;
		}

		ushort shortTypeId = Reader.ReadObjectStart( argumentName, valueIndex );
		if( shortTypeId != 0 )
		{
			Type itemType = TypeProvider.FindRuntimeType( shortTypeId );
			DeSerializeContext<T> context = new( this, value, valueIndex, itemType, argumentName );
			T reading = objectDeSerialization( context );

			Reader.ReadObjectEnd( argumentName, itemType );

			return reading;
		}

		return default;
	}

	public ushort GetVersion<T>()
		where T : IDeSerializable
	{
		DeSerializableAttribute att = typeof( T ).GetOneCustomAttribute<DeSerializableAttribute>();
		return IsWrite ? att.Version : TypeProvider.GetVersion( att.Identifier, ShortRuntimeTypes.Peek() );
	}
}
