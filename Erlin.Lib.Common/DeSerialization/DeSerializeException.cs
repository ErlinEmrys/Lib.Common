namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Exception during DeSerialization
/// </summary>
[Serializable]
public class DeSerializeException : Exception
{
	/// <summary>
	///    Ctor
	/// </summary>
	public DeSerializeException()
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	public DeSerializeException( string? message ) : base( message )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="message">Custom error message</param>
	/// <param name="innerException">Original thrown exception</param>
	public DeSerializeException( string? message, Exception? innerException ) : base( message, innerException )
	{
	}
}
