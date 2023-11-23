namespace Erlin.Lib.Common.Exceptions;

/// <summary>
///    Exception during attribute ctor
/// </summary>
[Serializable]
public class AttributeCreationException : Exception
{
	/// <summary>
	///    Ctor
	/// </summary>
	public AttributeCreationException()
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	public AttributeCreationException( string? message ) : base( message )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	/// <param name="innerException">Original thrown exception</param>
	public AttributeCreationException( string? message, Exception? innerException ) : base(
		message, innerException )
	{
	}
}
