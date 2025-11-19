// ReSharper disable ArrangeStaticMemberQualifier

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Cacheable type symbol for code generation
/// </summary>
public class DeSerializableInfo
(
	string name,
	string nameSpace,
	bool isBottomImplementation,
	bool isAbstract,
	bool implementDeSerializeCtor,
	bool implementParamlessCtor
)
	: IEquatable< DeSerializableInfo >
{
	/// <summary>
	///    Type name
	/// </summary>
	public string Name { get; init; } = name;

	/// <summary>
	///    Type namespace
	/// </summary>
	public string NameSpace { get; init; } = nameSpace;

	/// <summary>
	///    Whether is this type the first one that implements DeSerializable method in inheritance
	///    hierarchy
	/// </summary>
	public bool IsBottomImplementation { get; init; } = isBottomImplementation;

	/// <summary>
	///    Whether is this type abstract
	/// </summary>
	public bool IsAbstract { get; init; } = isAbstract;

	/// <summary>
	///    Whether is DeSerialize constructor implemented manually
	/// </summary>
	public bool ImplementDeSerializeCtor { get; init; } = implementDeSerializeCtor;

	/// <summary>
	///    Whether is parameterless constructor implemented manually
	/// </summary>
	public bool ImplementParamlessCtor { get; init; } = implementParamlessCtor;

	/// <summary>
	///    Equality comparer
	/// </summary>
	public bool Equals( DeSerializableInfo? other )
	{
		if( ReferenceEquals( null, other ) )
		{
			return false;
		}

		if( ReferenceEquals( this, other ) )
		{
			return true;
		}

		return ( Name == other.Name ) && ( NameSpace == other.NameSpace );
	}

	/// <summary>
	///    Equality comparer
	/// </summary>
	public override bool Equals( object? obj )
	{
		if( ReferenceEquals( null, obj ) )
		{
			return false;
		}

		if( ReferenceEquals( this, obj ) )
		{
			return true;
		}

		return ( obj.GetType() == GetType() ) && Equals( ( DeSerializableInfo )obj );
	}

	/// <summary>
	///    Returns the hash code for this object
	/// </summary>
	/// <returns></returns>
	public override int GetHashCode()
	{
		unchecked
		{
			int hashCode = Name.GetHashCode();
			hashCode = ( hashCode * 397 ) ^ NameSpace.GetHashCode();
			return hashCode;
		}
	}

	/// <summary>
	///    Equality comparer
	/// </summary>
	public static bool operator ==( DeSerializableInfo? left, DeSerializableInfo? right )
	{
		return Equals( left, right );
	}

	/// <summary>
	///    InEquality comparer
	/// </summary>
	public static bool operator !=( DeSerializableInfo? left, DeSerializableInfo? right )
	{
		return !Equals( left, right );
	}
}
