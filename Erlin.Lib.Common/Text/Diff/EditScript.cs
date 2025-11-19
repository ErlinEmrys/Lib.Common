using System.Collections.ObjectModel;

namespace Erlin.Lib.Common.Text.Diff;

/// <summary>
///    Collection of all edits needed to transform seqA to seqB
/// </summary>
public sealed class EditScript : ReadOnlyCollection< Edit >
{
	/// <summary>
	///    Total lenght of all edits
	/// </summary>
	public int TotalEditLength { get; private set; }

	/// <summary>
	///    Similarity index
	/// </summary>
	public double Similarity { get; }

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="similarity">Similarity index</param>
	internal EditScript( double similarity )
		: base( new List< Edit >() )
	{
		Similarity = similarity;
	}

	/// <summary>
	///    Add another edit part to collection
	/// </summary>
	/// <param name="edit">Edit part</param>
	internal void Add( Edit edit )
	{
		if( edit.EditType == TextEditType.Change )
		{
			TotalEditLength += 2 * edit.Length;
		}
		else
		{
			TotalEditLength += edit.Length;
		}

		Items.Add( edit );
	}
}
