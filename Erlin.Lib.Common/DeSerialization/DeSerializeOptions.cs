namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Configurable options for DeSerialization
/// </summary>
public class DeSerializeOptions
{
	/// <summary>
	///    File extension for saved files
	/// </summary>
	public string FileExtension { get; set; } = DeSerializeConstants.FILE_EXTENSION_DB;

	/// <summary>
	///    File extension for temp file during serialization
	/// </summary>
	public string TempFileExtension { get; set; } = DeSerializeConstants.FILE_EXTENSION_TEMP;

	/// <summary>
	///    Whether the files should be saved in Binary (true) or JSON (false) format
	/// </summary>
	public bool IsBinaryFormat { get; set; }

	/// <summary>
	///    Whether the binary format file should use ZIP compression
	/// </summary>
	public bool BinaryZipCompress { get; set; } = true;

	/// <summary>
	///    Whether JSON file should be auto-formatted for better readability
	/// </summary>
	public bool JsonFormatted { get; set; } = true;

	/// <summary>
	///    Indentation for JSON auto-formatting
	/// </summary>
	public string JsonIndentation { get; set; } = "\t";
}
