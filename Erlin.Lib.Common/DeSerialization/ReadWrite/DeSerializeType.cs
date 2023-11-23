namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    Runtime type record in serialized data
/// </summary>
public sealed class DeSerializeType
{
	/// <summary>
	///    Identifier of this runtime type in specific version
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	///    Short identifier of this runtime type in specific version
	/// </summary>
	public ushort ShortId
	{
		get { return ( ushort )Id; }
	}

	/// <summary>
	///    Short identifier of DeSerializable parent runtime type in specific version
	/// </summary>
	public ushort? ParentId { get; set; }

	/// <summary>
	///    Runtime type identifier
	/// </summary>
	public Guid Identifier { get; set; }

	/// <summary>
	///    DeSerializable version for this type
	/// </summary>
	public ushort Version { get; set; }

	/// <summary>
	///    Runtime type name that was used during serialization
	/// </summary>
	public string OriginalRuntimeTypeName { get; set; } = string.Empty;
}
