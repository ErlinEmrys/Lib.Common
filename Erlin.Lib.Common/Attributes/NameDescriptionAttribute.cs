namespace Erlin.Lib.Common;

/// <summary>
///    Attribute for name and description on the enum field
/// </summary>
[AttributeUsage( AttributeTargets.Field )]
public class NameDescriptionAttribute : Attribute
{
	/// <summary>
	///    Associated name
	/// </summary>
	public string Name { get; }

	/// <summary>
	///    Associated description
	/// </summary>
	public string? Description { get; }

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="name">associated name</param>
	/// <param name="description">Associated description</param>
	public NameDescriptionAttribute( string name, string? description = null )
	{
		Name = name;
		Description = description;
	}
}
