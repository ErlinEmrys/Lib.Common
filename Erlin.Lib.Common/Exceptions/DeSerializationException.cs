namespace Erlin.Lib.Common.Exceptions;

/// <summary>
///    Exception during DeSerialization
/// </summary>
[Serializable]
public class DeSerializationException : Exception
{
	/// <summary>
	///    Ctor
	/// </summary>
	public DeSerializationException()
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	public DeSerializationException( string? message ) : base( message )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	/// <param name="innerException">Original thrown exception</param>
	public DeSerializationException( string? message, Exception? innerException ) : base(
		message, innerException )
	{
	}
}
