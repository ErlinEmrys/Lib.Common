using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

public abstract class DeSerializeBaseTypeProvider : IDeSerializeTypeProvider
{
	private ConcurrentDictionary< Type, DeSerializeType > SerializeTypeCache { get; } = new();

	private ConcurrentDictionary< Guid, ushort > VersionTypeCache { get; } = new();

	private ConcurrentDictionary< ushort, Type > DeserializeTypeCache { get; } = new();

	protected abstract DeSerializeType? FindById( ushort shortTypeId );

	protected abstract DeSerializeType? FindByIdentifier( Guid typeIdentifier );

	protected abstract DeSerializeType? FindOne( Expression< Func< DeSerializeType, bool > > predicate );

	protected abstract void AddType( DeSerializeType type );

	public ushort GetVersion( Guid typeIdentifier )
	{
		return VersionTypeCache.GetOrAdd( typeIdentifier, identifier =>
		{
			DeSerializeType? type = FindByIdentifier( identifier );
			if( type == null )
			{
				throw new DeSerializeException( $"Could not locate DeSerializeType by identifier: {identifier}" );
			}

			return type.Version;
		} );
	}

	public DeSerializeType EnsureType( Type runtimeType )
	{
		return SerializeTypeCache.GetOrAdd( runtimeType, rt =>
		{
			ushort? parentId = null;
			if( ( rt.BaseType != null ) && DeSerializeTypeCache.CheckTypeIsDeSerializable( rt.BaseType ) )
			{
				DeSerializeType parentType = EnsureType( rt.BaseType );
				parentId = parentType.ShortId;
			}

			ushort version = DeSerializeTypeCache.GetVersion( rt );
			Guid identifier = DeSerializeTypeCache.GetIdentifier( rt );

			DeSerializeType? type = FindOne( t =>
				( t.Identifier == identifier )
				&& ( t.Version == version )
				&& ( t.ParentId == parentId ) );

			if( type == null )
			{
				type = new DeSerializeType();
				type.ParentId = parentId;
				type.OriginalRuntimeTypeName = rt.FullName ?? throw new InvalidOperationException( "Unknown runtime type name!" );

				type.Identifier = identifier;
				type.Version = version;
				AddType( type );
			}

			return type;
		} );
	}

	public Type FindRuntimeType( ushort shortTypeId )
	{
		return DeserializeTypeCache.GetOrAdd( shortTypeId, id =>
		{
			DeSerializeType? type = FindById( id );
			if( type == null )
			{
				throw new DeSerializeException( $"Could not locate DeSerializeType by short ID: {id}" );
			}

			return DeSerializeTypeCache.GetRuntimeType( type.Identifier, type.OriginalRuntimeTypeName );
		} );
	}
}
