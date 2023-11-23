namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Attribute for all De/Serializable records
/// </summary>
[AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct, Inherited = false )]
public class DeSerializableAttribute : Attribute
{
	/// <summary>
	///    Unique identifier for record runtime type
	/// </summary>
	public Guid Identifier { get; }

	/// <summary>
	///    Current version of De/Serialization
	/// </summary>
	public ushort Version { get; }

	public DeSerializableAttribute( ushort version, string identifier )
	{
		Version = version;
		Identifier = new Guid( identifier );
	}
}
