namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Helper context for custom De/Serialization
/// </summary>
/// <typeparam name="T"></typeparam>
public class DeSerializeContext< T >
(
	IDeSerializer ds,
	T? value,
	int? valueIndex,
	Type valueRuntimeType,
	string? argumentName
)
{
	/// <summary>
	///    De/Serializer
	/// </summary>
	public IDeSerializer DS { get; } = ds;

	/// <summary>
	///    Value to read
	/// </summary>
	private T? Value { get; } = value;

	/// <summary>
	///    Index of current entity from parent collection
	/// </summary>
	public int? ValueIndex { get; } = valueIndex;

	/// <summary>
	///    Original argument name
	/// </summary>
	public string? ArgumentName { get; } = argumentName;

	/// <summary>
	///    Runtime type of value
	/// </summary>
	public Type ValueRuntimeType { get; } = valueRuntimeType;

	/// <summary>
	///    Returns current item or constructs new one
	/// </summary>
	/// <param name="valueCreation"></param>
	/// <returns></returns>
	public T GetValue( Func< T? >? valueCreation = null )
	{
		if( Value is not null )
		{
			return Value;
		}

		return valueCreation != null ? valueCreation() ?? default! : default!;
	}
}
