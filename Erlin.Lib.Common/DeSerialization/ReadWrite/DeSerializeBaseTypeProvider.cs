using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

public abstract class DeSerializeBaseTypeProvider : IDeSerializeTypeProvider
{
	private ConcurrentDictionary<Type, DeSerializeType> SerializeTypeCache { get; } = new();

	private ConcurrentDictionary<Tuple<Guid, ushort>, ushort> VersionTypeCache { get; } = new();

	private ConcurrentDictionary<ushort, Type> DeserializeTypeCache { get; } = new();

	protected abstract DeSerializeType? FindById( ushort shortTypeId );

	protected abstract DeSerializeType? FindOne( Expression<Func<DeSerializeType, bool>> predicate );

	protected abstract void AddType( DeSerializeType type );

	public ushort GetVersion( Guid typeIdentifier, ushort shortTypeId )
	{
		Tuple<Guid, ushort> key = new( typeIdentifier, shortTypeId );
		return VersionTypeCache.GetOrAdd(
			key, t =>
			{
				DeSerializeType? type = FindById( t.Item2 );
				if( type == null )
				{
					throw new DeSerializeException( $"Could not locate DeSerializeType by short ID: {t.Item2}" );
				}

				if( type.Identifier == t.Item1 )
				{
					return type.Version;
				}

				if( type.ParentId.HasValue )
				{
					return GetVersion( typeIdentifier, type.ParentId.Value );
				}

				throw new DeSerializeException(
					$"Could not get DeSerialize version by ID: {t.Item2} and guid: {t.Item1}" );
			} );
	}

	public DeSerializeType EnsureType( Type runtimeType )
	{
		return SerializeTypeCache.GetOrAdd(
			runtimeType, rt =>
			{
				ushort? parentId = null;
				if( ( rt.BaseType != null )
					&& DeSerializeTypeCache.CheckTypeIsDeSerializable( rt.BaseType ) )
				{
					DeSerializeType parentType = EnsureType( rt.BaseType );
					parentId = parentType.ShortId;
				}

				ushort version = DeSerializeTypeCache.GetVersion( rt );
				Guid identifier = DeSerializeTypeCache.GetIdentifier( rt );

				DeSerializeType? type = FindOne(
					t => ( t.Identifier == identifier ) && ( t.Version == version ) && ( t.ParentId == parentId ) );

				if( type == null )
				{
					type = new DeSerializeType();
					type.ParentId = parentId;
					type.OriginalRuntimeTypeName = rt.FullName
						?? throw new InvalidOperationException( "Unknown runtime type name!" );

					type.Identifier = identifier;
					type.Version = version;
					AddType( type );
				}

				return type;
			} );
	}

	public Type FindRuntimeType( ushort shortTypeId )
	{
		return DeserializeTypeCache.GetOrAdd(
			shortTypeId, id =>
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
