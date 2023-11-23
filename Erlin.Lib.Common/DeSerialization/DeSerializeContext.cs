namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Helper context for custom De/Serialization
/// </summary>
/// <typeparam name="T"></typeparam>
public class DeSerializeContext<T>
{
	/// <summary>
	///    De/Serializer
	/// </summary>
	public IDeSerializer DS { get; }

	/// <summary>
	///    Value to read
	/// </summary>
	private T? Value { get; }

	/// <summary>
	///    Index of current entity from parent collection
	/// </summary>
	public int? ValueIndex { get; }

	/// <summary>
	///    Original argument name
	/// </summary>
	public string? ArgumentName { get; }

	/// <summary>
	///    Runtime type of value
	/// </summary>
	public Type ValueRuntimeType { get; }

	public DeSerializeContext(
		IDeSerializer ds, T? value, int? valueIndex, Type valueRuntimeType,
		string? argumentName )
	{
		DS = ds;
		Value = value;
		ValueIndex = valueIndex;
		ArgumentName = argumentName;
		ValueRuntimeType = valueRuntimeType;
	}

	/// <summary>
	///    Returns current item or constructs new one
	/// </summary>
	/// <param name="valueCreation"></param>
	/// <returns></returns>
	public T GetValue( Func<T?>? valueCreation = null )
	{
		if( Value is not null )
		{
			return Value;
		}

		return valueCreation != null ? valueCreation() ?? default! : default!;
	}
}
