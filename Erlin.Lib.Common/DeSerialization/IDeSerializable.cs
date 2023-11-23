namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Serializable object
/// </summary>
public interface IDeSerializable
{
	/// <summary>
	///    De/Serialize object
	/// </summary>
	/// <param name="ds"></param>
	void DeSerialize( IDeSerializer ds );
}
