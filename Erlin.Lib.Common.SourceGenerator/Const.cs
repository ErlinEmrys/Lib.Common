namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Various constants
/// </summary>
public static class Const
{
	public const string STRING_NS = "System";
	public const string STRING_NAME = "String";

	public const string GENERATED_CODE_ATT_NS = "System.CodeDom.Compiler";
	public const string GENERATED_CODE_ATT_NAME = "GeneratedCodeAttribute";

	public const string DE_SERIALIZABLE_ATT_NS = "Erlin.Lib.Common.DeSerialization";
	public const string DE_SERIALIZABLE_ATT_NAME = "DeSerializableAttribute";
	public const string DE_SERIALIZABLE_METHOD_NAME = "DeSerialize";

	public const string I_DE_SERIALIZABLE_NS = DE_SERIALIZABLE_ATT_NS;
	public const string I_DE_SERIALIZABLE_NAME = "IDeSerializable";

	public const string I_DE_SERIALIZER_NS = DE_SERIALIZABLE_ATT_NS;
	public const string I_DE_SERIALIZER_NAME = "IDeSerializer";
}
