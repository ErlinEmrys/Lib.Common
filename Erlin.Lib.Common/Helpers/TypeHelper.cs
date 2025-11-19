using System.Collections;
using System.Linq.Expressions;

using Erlin.Lib.Common.DeSerialization;

namespace Erlin.Lib.Common;

/// <summary>
///    Helper class for runtime types
/// </summary>
public static class TypeHelper
{
	/// <summary>
	///    Bool runtime type
	/// </summary>
	public static Type TypeBool { get; } = typeof( bool );

	/// <summary>
	///    String runtime type
	/// </summary>
	public static Type TypeString { get; } = typeof( string );

	/// <summary>
	///    Decimal runtime type
	/// </summary>
	public static Type TypeDecimal { get; } = typeof( decimal );

	/// <summary>
	///    Double runtime type
	/// </summary>
	public static Type TypeDouble { get; } = typeof( double );

	/// <summary>
	///    Float runtime type
	/// </summary>
	public static Type TypeFloat { get; } = typeof( float );

	/// <summary>
	///    Timespan runtime type
	/// </summary>
	public static Type TypeTimespan { get; } = typeof( TimeSpan );

	/// <summary>
	///    Timespan runtime type
	/// </summary>
	public static Type TypeDateTime { get; } = typeof( DateTime );

	/// <summary>
	///    Nullable runtime type
	/// </summary>
	public static Type TypeNullable { get; } = typeof( Nullable<> );

	/// <summary>
	///    Guid runtime type
	/// </summary>
	public static Type TypeGuid { get; } = typeof( Guid );

	/// <summary>
	///    Int16 runtime type
	/// </summary>
	public static Type TypeInt16 { get; } = typeof( short );

	/// <summary>
	///    Int32 runtime type
	/// </summary>
	public static Type TypeInt32 { get; } = typeof( int );

	/// <summary>
	///    Int64 runtime type
	/// </summary>
	public static Type TypeInt64 { get; } = typeof( long );

	/// <summary>
	///    UInt16 runtime type
	/// </summary>
	public static Type TypeUInt16 { get; } = typeof( ushort );

	/// <summary>
	///    UInt32 runtime type
	/// </summary>
	public static Type TypeUInt32 { get; } = typeof( uint );

	/// <summary>
	///    UInt64 runtime type
	/// </summary>
	public static Type TypeUInt64 { get; } = typeof( ulong );

	/// <summary>
	///    Byte runtime type
	/// </summary>
	public static Type TypeByte { get; } = typeof( byte );

	/// <summary>
	///    SByte runtime type
	/// </summary>
	public static Type TypeSByte { get; } = typeof( sbyte );

	/// <summary>
	///    IList runtime type
	/// </summary>
	public static Type TypeIList { get; } = typeof( IList );

	/// <summary>
	///    IDeSerializable runtime type
	/// </summary>
	public static Type TypeIDeSerializable { get; } = typeof( IDeSerializable );

	/// <summary>
	///    Check if entered value's runtime type is null-able (value can be null)
	/// </summary>
	/// <param name="value">Checked object</param>
	/// <returns>True if value can be null</returns>
	public static bool IsNullable( object? value )
	{
		return ( value == null ) || TypeHelper.IsNullable( value.GetType() );
	}

	/// <summary>
	///    Check if entered runtime type is null-able (value can be null)
	/// </summary>
	/// <param name="type">Checked runtime type</param>
	/// <returns>True if value can be null</returns>
	public static bool IsNullable( Type type )
	{
		if( !type.IsValueType )
		{
			return true; // ref-type
		}

		return Nullable.GetUnderlyingType( type ) != null;
	}

	/// <summary>
	///    Check if entered runtime type is number with floating point
	/// </summary>
	/// <param name="type">Runtime type to check</param>
	/// <returns>True - runtime type is floating point number type</returns>
	public static bool IsFloatingNumber( Type type )
	{
		return ( type == TypeHelper.TypeDecimal )
			|| ( type == TypeHelper.TypeDouble )
			|| ( type == TypeHelper.TypeFloat );
	}

	/// <summary>
	///    Unfolds runtime type from nullable type (Extract non-nullable type)
	/// </summary>
	/// <param name="type"></param>
	/// <returns>Unfolded type from nullable type or original value</returns>
	public static Type UnfoldNullable( Type type )
	{
		Type? underlyingType = Nullable.GetUnderlyingType( type );
		return underlyingType ?? type;
	}

	/// <summary>
	///    Get name of the property or field from LINQ query (Example: GetPropName( ()=> IsReadOnly ); )
	/// </summary>
	/// <typeparam name="TProperty">Property type</typeparam>
	/// <param name="property">LINQ query</param>
	/// <returns>Name of the property in</returns>
	public static string GetPropertyName< TProperty >( Expression< Func< TProperty > > property )
	{
		LambdaExpression lambda = property;

		MemberExpression memberExpression;
		if( lambda.Body is UnaryExpression body )
		{
			memberExpression = ( MemberExpression )body.Operand;
		}
		else
		{
			memberExpression = ( MemberExpression )lambda.Body;
		}

		return memberExpression.Member.Name;
	}
}
