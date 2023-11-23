namespace Erlin.Lib.Common.Text.Diff;

/// <summary>
///    Hash types for text diff
/// </summary>
public enum TextDiffHashType
{
	/// <summary>
	///    Unknown/Error
	/// </summary>
	Error = 0,

	/// <summary>
	///    By hash code
	/// </summary>
	HashCode = 1,

	/// <summary>
	///    Crc 32
	/// </summary>
	Crc32 = 2,

	/// <summary>
	///    Unique
	/// </summary>
	Unique = 3,
}
