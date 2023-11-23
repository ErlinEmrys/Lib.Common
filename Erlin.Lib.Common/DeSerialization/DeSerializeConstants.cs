namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Constants for DeSerialization
/// </summary>
public static class DeSerializeConstants
{
	public const string FILE_EXTENSION_DB = ".bjd";
	public const string FILE_EXTENSION_TEMP = ".tmp";
	public const string FIELD_MAIN_VERSION = "#Version";
	public const string FIELD_MAIN_DATA = "#Data";
	public const string FIELD_OBJECT_TYPE_ID = "#ObjTypeId";

	public const string TYPE_TABLE_FIELD_TABLE = "#RuntimeTypes";
	public const string TYPE_TABLE_FIELD_PARENT_ID = "ParentId";
	public const string TYPE_TABLE_FIELD_VERSION = "Version";
	public const string TYPE_TABLE_FIELD_DATA = "Data";
	public const string TYPE_TABLE_FIELD_IDENTIFIER = "Identifier";
	public const string TYPE_TABLE_FIELD_TYPENAME = "TypeName";

	public const byte FILE_HEADER_BINARY = 42;// B char = Binary version of file
	public const byte FILE_HEADER_JSON = 123;// { char = JSON version of file
	public const byte FILE_CLOSURE_JSON = 125;// } char = JSON version of file

	public const byte FLAG_OBJECT_END = byte.MaxValue;
	public const byte FLAG_COLLECTION_END = byte.MaxValue;

	public const ushort TYPE_ID_CUSTOM_TYPE = ushort.MaxValue;
	public const ushort TYPE_ID_TYPE_TABLE = ushort.MaxValue - 1;
	public const ushort TYPE_ID_MAIN_DATA = ushort.MaxValue - 2;
	public const ushort TYPE_ID_KEY_VALUE_PAIR = ushort.MaxValue - 3;
	public const ushort TYPE_ID_OBJECT_NULL = 0;

	public const int FLAG_LENGTH_NULL = -1;
	public const int FLAG_STRING_EMPTY = 0;
}
