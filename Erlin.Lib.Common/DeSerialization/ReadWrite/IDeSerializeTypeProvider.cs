namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

public interface IDeSerializeTypeProvider
{
	ushort GetVersion( Guid typeIdentifier, ushort shortTypeId );

	DeSerializeType EnsureType( Type runtimeType );

	Type FindRuntimeType( ushort shortTypeId );
}
