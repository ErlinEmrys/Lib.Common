namespace Erlin.Lib.Common.Exceptions;

/// <summary>
///    Exception when go to not implemented and not expected case statement
/// </summary>
[Serializable]
public class CaseNotExpectedException : Exception
{
	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="value">Value of the case</param>
	public CaseNotExpectedException( object value ) : base( CaseNotExpectedException.CreateMessage( value ) )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="value">Value of the case</param>
	/// <param name="innerException">Original thrown exception</param>
	public CaseNotExpectedException( object value, Exception? innerException ) : base(
		CaseNotExpectedException.CreateMessage( value ), innerException )
	{
	}

	/// <summary>
	///    Create error message
	/// </summary>
	/// <param name="value">Value of the case</param>
	/// <returns>Error message</returns>
	private static string CreateMessage( object value )
	{
		return $"Case not expected or implemented for value: {value}";
	}
}
