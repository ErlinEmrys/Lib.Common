namespace Erlin.Lib.Common.Exceptions;

/// <summary>
///    Unexpected result of an operation
/// </summary>
[Serializable]
public class UnexpectedResultException : Exception
{
	/// <summary>
	///    Ctor
	/// </summary>
	public UnexpectedResultException()
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	public UnexpectedResultException( string? message ) : base( message )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	/// <param name="innerException">Original thrown exception</param>
	public UnexpectedResultException( string? message, Exception? innerException ) : base(
		message, innerException )
	{
	}
}
