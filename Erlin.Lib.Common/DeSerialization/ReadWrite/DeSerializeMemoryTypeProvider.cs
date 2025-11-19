using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

public class DeSerializeMemoryTypeProvider
(
	List< DeSerializeType > _table
)
	: DeSerializeBaseTypeProvider
{
	private int _typeTableIdGenerator;

	private static ConcurrentDictionary< Expression< Func< DeSerializeType, bool > >, Func< DeSerializeType, bool > > FindOnePredicates { get; } = new();

	/// <summary>
	///    Runtime type table
	/// </summary>
	private List< DeSerializeType > Table { get; } = _table;

	public DeSerializeMemoryTypeProvider() : this( [ ] )
	{
	}

	protected override DeSerializeType? FindById( ushort shortTypeId )
	{
		return Table.FirstOrDefault( rt => rt.ShortId == shortTypeId );
	}

	protected override DeSerializeType? FindByIdentifier( Guid typeIdentifier )
	{
		return Table.FirstOrDefault( rt => rt.Identifier == typeIdentifier );
	}

	protected override DeSerializeType? FindOne( Expression< Func< DeSerializeType, bool > > predicate )
	{
		return Table.FirstOrDefault( DeSerializeMemoryTypeProvider.FindOnePredicates.GetOrAdd( predicate, p => p.Compile() ) );
	}

	protected override void AddType( DeSerializeType type )
	{
		type.Id = ++_typeTableIdGenerator;
		Table.Add( type );
	}
}
