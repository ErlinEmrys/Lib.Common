namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Attribute for all De/Serializable records
/// </summary>
[AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct, Inherited = false )]
public class DeSerializableAttribute
(
	ushort version,
	string identifier
) : Attribute
{
	/// <summary>
	///    Unique identifier for record runtime type
	/// </summary>
	public Guid Identifier { get; } = new( identifier );

	/// <summary>
	///    Current version of De/Serialization
	/// </summary>
	public ushort Version { get; } = version;
}
