using System.Globalization;

namespace Erlin.Lib.Common;

/// <summary>
///    Helper class to simple convert runtime types
/// </summary>
public static class UniversalConvert
{
	/// <summary>
	///    US number format
	/// </summary>
	public static NumberFormatInfo NumberFormatEnUs { get; } =
		new CultureInfo( "en-US", false ).NumberFormat;

	/// <summary>
	///    Czech number format
	/// </summary>
	public static NumberFormatInfo NumberFormatCsCz { get; } =
		new CultureInfo( "cs-CZ", false ).NumberFormat;

	/// <summary>
	///    Arabic Egypt number format
	/// </summary>
	public static NumberFormatInfo NumberFormatArEg { get; } =
		new CultureInfo( "ar-EG", false ).NumberFormat;

	/// <summary>
	///    Converts null value to DBNull value
	/// </summary>
	/// <param name="value">Original value</param>
	/// <returns>Original value or DBNull</returns>
	public static object? ConvertDbNullToNull( object? value )
	{
		return value != DBNull.Value ? value : null;
	}

	/// <summary>
	///    Converts DBNull to null value
	/// </summary>
	/// <param name="value">Original value</param>
	/// <returns>Original value or null</returns>
	public static object ConvertNullToDbNull( object? value )
	{
		return value ?? DBNull.Value;
	}

	/// <summary>
	///    Converts entered object to different runtime type
	/// </summary>
	/// <typeparam name="T">Runtime type for converting input object to</typeparam>
	/// <param name="input">Input object to convert</param>
	/// <returns>Converted value</returns>
	public static T Convert<T>( object? input )
	{
		Type targetType = typeof( T );
		object? converted = UniversalConvert.ConvertToType( input, targetType );
		if( converted == null )
		{
			throw new FormatException( $"Could not convert value: '{input}' to type: '{targetType}'" );
		}

		return ( T )converted;
	}

	/// <summary>
	///    Converts entered object to different runtime type
	/// </summary>
	/// <typeparam name="T">Runtime type for converting input object to</typeparam>
	/// <param name="input">Input object to convert</param>
	/// <returns>Converted value</returns>
	public static T? ConvertN<T>( object? input )
		where T : struct
	{
		Type targetType = typeof( T );
		object? converted = UniversalConvert.ConvertToType( input, targetType );
		if( ( converted == null ) && !TypeHelper.IsNullable( targetType ) )
		{
			return default;
		}

		return ( T? )converted;
	}

	/// <summary>
	///    Try converts entered object to entered type (returns null if fail)
	/// </summary>
	/// <param name="input">Input object to convert</param>
	/// <param name="targetType">Target type of conversion</param>
	/// <returns>Converted object or null</returns>
	public static object? ConvertToType( object? input, Type targetType )
	{
		//Argument null check
		ArgumentNullException.ThrowIfNull( targetType );

		//Unfold target nullable type
		targetType = TypeHelper.UnfoldNullable( targetType );

		//Unfold DBNull objects
		input = UniversalConvert.ConvertDbNullToNull( input );

		//Null check
		if( input == null )
		{
			return null;
		}

		string? inputText = input.ToString();
		bool inputIsString = input is string;

		//Empty check
		if( inputText.IsEmpty() )
		{
			return targetType == TypeHelper.TypeString ? inputText : null;
		}

		Type objectType = input.GetType();
		objectType = TypeHelper.UnfoldNullable( objectType );

		//Object is same type or derived - do nothing
		if( targetType.IsAssignableFrom( objectType ) )
		{
			return input;
		}

		//Parse bool
		if( targetType == TypeHelper.TypeBool )
		{
			string inputTextUpper = inputText.ToUpperInvariant();
			switch( inputTextUpper )
			{
				case "TRUE":
				case "1":
				case "A":
				case "Y":
					return true;

				case "FALSE":
				case "0":
				case "N":
					return false;

				default:
					return null;
			}
		}

		//Parse enum
		if( targetType.IsEnum )
		{
			object enumValue = Enum.Parse( targetType, inputText, true );
			return Enum.IsDefined( targetType, enumValue ) ? enumValue : null;
		}

		//Parse TimeSpan
		if( targetType == TypeHelper.TypeTimespan )
		{
			return UniversalConvert.TryParseTimeSpan( inputText );
		}

		//Floating-point number
		if( TypeHelper.IsFloatingNumber( targetType ) && inputIsString )
		{
			object? result = null;
			if( inputText.IsNotEmpty() )
			{
				result = UniversalConvert.StringToFloatingNumber( inputText, targetType );
			}

			return result;
		}

		//Parse Guid
		if( targetType == TypeHelper.TypeGuid )
		{
			if( inputIsString )
			{
				return new Guid( inputText );
			}

			if( input is byte[] inputByteArray )
			{
				return new Guid( inputByteArray );
			}
		}

		//Everything else
		return System.Convert.ChangeType( input, targetType, CultureInfo.InvariantCulture );
	}

	/// <summary>
	///    Converts text to selected floating-point number (returns null if fail)
	/// </summary>
	/// <param name="input">Input text to convert</param>
	/// <param name="targetType">Target floating-point number runtime type</param>
	/// <returns>Converted number or null</returns>
	private static object? StringToFloatingNumber( string input, Type targetType )
	{
		if( targetType == TypeHelper.TypeDecimal )
		{
			return UniversalConvert.TryParseDecimal( input );
		}

		if( targetType == TypeHelper.TypeDouble )
		{
			return UniversalConvert.TryParseDouble( input );
		}

		if( targetType == TypeHelper.TypeFloat )
		{
			return UniversalConvert.TryParseFloat( input );
		}

		throw new InvalidOperationException( $"Type '{targetType}' is not supported as floating number!" );
	}

	/// <summary>
	///    Picks number format info based on input string
	/// </summary>
	/// <param name="input">Input string</param>
	/// <returns>Selected number format</returns>
	private static NumberFormatInfo PickNumberFormatInfo( string input )
	{
		NumberFormatInfo numberFormatInfo = NumberFormatInfo.CurrentInfo;
		if( input.Contains( UniversalConvert.NumberFormatEnUs.NumberDecimalSeparator ) )
		{
			numberFormatInfo = UniversalConvert.NumberFormatEnUs;
		}
		else if( input.Contains( UniversalConvert.NumberFormatCsCz.NumberDecimalSeparator ) )
		{
			numberFormatInfo = UniversalConvert.NumberFormatCsCz;
		}
		else if( input.Contains( UniversalConvert.NumberFormatArEg.NumberDecimalSeparator ) )
		{
			numberFormatInfo = UniversalConvert.NumberFormatArEg;
		}

		return numberFormatInfo;
	}

#region TryParse methods

	/// <summary>
	///    Convert entered text to decimal, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static decimal? TryParseDecimal( string input, decimal? defaultValue = null )
	{
		if( input.IsEmpty() )
		{
			return null;
		}

		NumberFormatInfo numberFormatInfo = UniversalConvert.PickNumberFormatInfo( input );
		return decimal.TryParse(
			input, NumberStyles.Number | NumberStyles.AllowExponent, numberFormatInfo,
			out decimal result )
			? result
			: defaultValue;
	}

	/// <summary>
	///    Convert entered text to double, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static double? TryParseDouble( string input, double? defaultValue = null )
	{
		if( input.IsEmpty() )
		{
			return null;
		}

		NumberFormatInfo numberFormatInfo = UniversalConvert.PickNumberFormatInfo( input );
		return double.TryParse(
			input, NumberStyles.Number | NumberStyles.AllowExponent, numberFormatInfo,
			out double result )
			? result
			: defaultValue;
	}

	/// <summary>
	///    Convert entered text to float, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static float? TryParseFloat( string input, float? defaultValue = null )
	{
		if( input.IsEmpty() )
		{
			return null;
		}

		NumberFormatInfo numberFormatInfo = UniversalConvert.PickNumberFormatInfo( input );
		return float.TryParse(
			input, NumberStyles.Number | NumberStyles.AllowExponent, numberFormatInfo,
			out float result )
			? result
			: defaultValue;
	}

	/// <summary>
	///    Convert entered text to sbyte, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static sbyte? TryParseSByte( string input, sbyte? defaultValue = null )
	{
		return sbyte.TryParse( input, out sbyte result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to short, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static short? TryParseInt16( string input, short? defaultValue = null )
	{
		return short.TryParse( input, out short result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to int, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static int? TryParseInt32( string input, int? defaultValue = null )
	{
		return int.TryParse( input, out int result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to long, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static long? TryParseInt64( string input, long? defaultValue = null )
	{
		return long.TryParse( input, out long result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to byte, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static byte? TryParseByte( string input, byte? defaultValue = null )
	{
		return byte.TryParse( input, out byte result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to ushort, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static ushort? TryParseUInt16( string input, ushort? defaultValue = null )
	{
		return ushort.TryParse( input, out ushort result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to uint, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static uint? TryParseUInt32( string input, uint? defaultValue = null )
	{
		return uint.TryParse( input, out uint result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to ulong, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static ulong? TryParseUInt64( string input, ulong? defaultValue = null )
	{
		return ulong.TryParse( input, out ulong result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to TimeSpan, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static TimeSpan? TryParseTimeSpan( string input, TimeSpan? defaultValue = null )
	{
		return TimeSpan.TryParse( input, out TimeSpan result ) ? result : defaultValue;
	}

	/// <summary>
	///    Convert entered text to DateTime, returns default value if conversion fails
	/// </summary>
	/// <param name="input">Text to convert</param>
	/// <param name="defaultValue">Default value</param>
	/// <returns>Converted value or default value</returns>
	public static DateTime? TryParseDateTime( string input, DateTime? defaultValue = null )
	{
		return DateTime.TryParse( input, out DateTime result ) ? result : defaultValue;
	}

#endregion
}
