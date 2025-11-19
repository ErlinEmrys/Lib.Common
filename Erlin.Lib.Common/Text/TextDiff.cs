using Erlin.Lib.Common.Text.Diff;

namespace Erlin.Lib.Common.Text;

/// <summary>
///    This class uses the MyersDiff helper class to difference two
///    string lists.  It hashes each string in both lists and then
///    differences the resulting integer arrays.
/// </summary>
public sealed class TextDiff
{
	private readonly bool _supportChangeEditType;
	private readonly StringHasher _hasher;

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="hashType">Type of used hash algorithm</param>
	/// <param name="ignoreCase">If ignore case</param>
	/// <param name="ignoreWhiteSpace">If ignore whitespaces on beginning and end</param>
	/// <param name="leadingCharactersToIgnore">Prefix lenght to ignore</param>
	/// <param name="supportChangeEditType">If change edit type is supported</param>
	public TextDiff( TextDiffHashType hashType, bool ignoreCase, bool ignoreWhiteSpace, int leadingCharactersToIgnore = 0, bool supportChangeEditType = true )
	{
		_hasher = new StringHasher( hashType, ignoreCase, ignoreWhiteSpace, leadingCharactersToIgnore );
		_supportChangeEditType = supportChangeEditType;
	}

	/// <summary>
	///    Makes comparison of two collections of string lines
	/// </summary>
	/// <param name="listA">Lines sequence A</param>
	/// <param name="listB">Lines sequence B</param>
	/// <returns>Edit script to transform A to B</returns>
	public EditScript Execute( IList< string > listA, IList< string > listB )
	{
		int[] hashA = HashStringList( listA );
		int[] hashB = HashStringList( listB );

		MyersDiff< int > diff = new( hashA, hashB, _supportChangeEditType );
		EditScript result = diff.Execute();
		return result;
	}

	/// <summary>
	///    Returns hash to every line of text
	/// </summary>
	/// <param name="lines">Lines of text</param>
	/// <returns>Hashes of lines</returns>
	private int[] HashStringList( IList< string > lines )
	{
		int numLines = lines.Count;
		int[] result = new int[numLines];

		for( int i = 0; i < numLines; i++ )
		{
			result[ i ] = _hasher.GetHashCode( lines[ i ] );
		}

		return result;
	}
}
