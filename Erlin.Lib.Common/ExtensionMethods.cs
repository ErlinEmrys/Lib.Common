using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;

using Erlin.Lib.Common;
using Erlin.Lib.Common.Exceptions;

using Newtonsoft.Json;

namespace System;

/// <summary>
///    Helper class for common extension methods
/// </summary>
public static class ExtensionMethods
{
#region Type

	/// <summary>
	///    Returns one custom attribute
	/// </summary>
	/// <typeparam name="T">Runtime type of attribute to retrieve</typeparam>
	/// <param name="member">Runtime type or its member</param>
	/// <param name="inherit">True - return inherited attribute</param>
	/// <returns>Custom attribute object or NULL</returns>
	public static T GetOneCustomAttribute<T>( this MemberInfo member, bool inherit = true )
		where T : Attribute
	{
		T? att = member.GetOneCustomAttributeN<T>( inherit );
		if( att == null )
		{
			throw new InvalidOperationException( $"Attribute {typeof( T ).FullName} missing on member {member}" );
		}

		return att;
	}

	private static readonly ConcurrentDictionary<Tuple<MemberInfo, Type>, Attribute?> _customAttributeCache =
		new();

	/// <summary>
	///    Returns one custom attribute
	/// </summary>
	/// <typeparam name="T">Runtime type of attribute to retrieve</typeparam>
	/// <param name="member">Runtime type or its member</param>
	/// <param name="inherit">True - return inherited attribute</param>
	/// <returns>Custom attribute object or NULL</returns>
	public static T? GetOneCustomAttributeN<T>( this MemberInfo member, bool inherit = true )
		where T : Attribute
	{
		Type attributeType = typeof( T );
		Tuple<MemberInfo, Type> key = new( member, attributeType );
		return ( T? )_customAttributeCache.GetOrAdd(
			key,
			k =>
			{
				object[] attributes;
				try
				{
					attributes = k.Item1.GetCustomAttributes( k.Item2, inherit );
				}
				catch( Exception ex )
				{
					throw new InvalidProgramException(
						$"Could not get custom attributes on object type: {k.Item1} "
						+ "because of attribute ctor error!",
						ex );
				}

				switch( attributes.Length )
				{
					case > 1:
						throw new InvalidProgramException(
							$"Found more than one attribute: {k.Item2} on member: {k.Item1}" );

					case 0:
						return null;
				}

				if( attributes[ 0 ] is not T att )
				{
					throw new InvalidProgramException(
						$"Found weird attribute: {attributes[ 0 ]} "
						+ $"instead of {k.Item2} on member: {k.Item1}" );
				}

				return att;
			} );
	}

#endregion

#region Enums

	private static readonly Dictionary<Enum, NameDescriptionAttribute?> _enumDescriptorCache = new();

	/// <summary>
	///    Finds NameDescriptionAttribute associated with an Enum value
	/// </summary>
	private static NameDescriptionAttribute? FindEnumDescriptorAtt( Enum value )
	{
		if( !_enumDescriptorCache.TryGetValue( value, out NameDescriptionAttribute? att ) )
		{
			// get attributes
			FieldInfo? field = value.GetType().GetTypeInfo().GetField( value.ToString() );
			if( field == null )
			{
				throw new InvalidOperationException( "Field not found by refactoring!" );
			}

			// Description is in a hidden Attribute class called DisplayAttribute
			// Not to be confused with DisplayNameAttribute
			IEnumerable<NameDescriptionAttribute> attributes =
				field.GetCustomAttributes<NameDescriptionAttribute>( false );

			att = attributes.SingleOrDefault();
			_enumDescriptorCache[ value ] = att;
		}

		return att;
	}

	/// <summary>
	///    Returns associated name on enum field
	/// </summary>
	/// <param name="value">Enum field value</param>
	/// <returns>Associated name</returns>
	public static string Name( this Enum value )
	{
		NameDescriptionAttribute? att = ExtensionMethods.FindEnumDescriptorAtt( value );
		return att?.Name ?? value.ToString();
	}

	/// <summary>
	///    Returns associated description on enum field
	/// </summary>
	/// <param name="value">Enum field value</param>
	/// <returns>Associated description</returns>
	public static string Description( this Enum value )
	{
		NameDescriptionAttribute? att = ExtensionMethods.FindEnumDescriptorAtt( value );
		return att?.Description ?? att?.Name ?? value.ToString();
	}

#endregion

#region Exception

	/// <summary>
	///    Returns full descriptive JSON of any exception
	/// </summary>
	/// <param name="exception">Exception</param>
	/// <param name="indented">Whether the output JSON should be indented</param>
	/// <returns>Text representation of exception</returns>
	public static string ToJson( this Exception exception, bool indented = true )
	{
		JsonSerializer serializer = new();
		serializer.Converters.Add( new ExceptionJsonConverter() );
		serializer.Formatting = indented ? Formatting.Indented : Formatting.None;
		serializer.NullValueHandling = NullValueHandling.Ignore;

		StringBuilder builder = new();
		using StringWriter writer = new( builder );
		serializer.Serialize( writer, exception );
		builder.AppendLine();
		return builder.ToString();
	}

#endregion

#region String

	/// <summary>
	///    Returns deterministic hash code for the string
	///    According to:
	///    https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static int GetDeterministicHashCode( this string str )
	{
		unchecked
		{
			int hash1 = ( 5381 << 16 ) + 5381;
			int hash2 = hash1;

			for( int i = 0; i < str.Length; i += 2 )
			{
				hash1 = ( ( hash1 << 5 ) + hash1 ) ^ str[ i ];
				if( i == str.Length - 1 )
				{
					break;
				}

				hash2 = ( ( hash2 << 5 ) + hash2 ) ^ str[ i + 1 ];
			}

			return hash1 + ( hash2 * 1566083941 );
		}
	}

	/// <summary>
	///    Check if string is null or empty
	/// </summary>
	/// <param name="text">Value to check</param>
	/// <returns>True = value is empty or null</returns>
	public static bool IsEmpty( [NotNullWhen( false )]this string? text )
	{
		return string.IsNullOrEmpty( text );
	}

	/// <summary>
	///    Check if string is NOT null or empty
	/// </summary>
	/// <param name="text">Value to check</param>
	/// <returns>True = value contains characters</returns>
	public static bool IsNotEmpty( [NotNullWhen( true )]this string? text )
	{
		return !text.IsEmpty();
	}

	/// <summary>
	///    Makes comparison of two strings
	/// </summary>
	/// <param name="text">First text</param>
	/// <param name="compareTo">Second text</param>
	/// <returns>True - strings are equal</returns>
	public static bool EqualsTo( this string? text, string? compareTo )
	{
		return string.Equals( text, compareTo, StringComparison.Ordinal );
	}

	/// <summary>
	///    Makes comparison of two strings (ignoring case)
	/// </summary>
	/// <param name="text">First text</param>
	/// <param name="compareTo">Second text</param>
	/// <returns>True - strings are equal</returns>
	public static bool EqualsToIgnoreCase( this string? text, string? compareTo )
	{
		return string.Equals( text, compareTo, StringComparison.OrdinalIgnoreCase );
	}

	/// <summary>
	///    Remove diacritics from text
	/// </summary>
	/// <param name="text">Input text</param>
	/// <returns>Text without diacritics</returns>
	[return: NotNullIfNotNull( nameof( text ) )]
	public static string? RemoveDiacritics( this string? text )
	{
		if( text.IsEmpty() )
		{
			return text;
		}

		string normalizedString = text.Normalize( NormalizationForm.FormD );
		StringBuilder stringBuilder = new();

		foreach( char c in normalizedString )
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory( c );
			if( unicodeCategory != UnicodeCategory.NonSpacingMark )
			{
				stringBuilder.Append( c );
			}
		}

		return stringBuilder.ToString().Normalize( NormalizationForm.FormC );
	}

	/// <summary>
	///    Check if string contains at least one of a basic chars = A-z1-9
	/// </summary>
	/// <param name="text">Input text</param>
	/// <returns>True - string contains basic chars</returns>
	public static bool ContainsBasicChar( this string? text )
	{
		if( text.IsEmpty() )
		{
			return false;
		}

		foreach( char fChar in text )
		{
			if( char.IsLetterOrDigit( fChar ) )
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	///    Check if string contains only basic chars = A-z1-9
	/// </summary>
	/// <param name="text">Input text</param>
	/// <returns>True - string contains basic chars</returns>
	public static bool ContainsLetterNumberOnly( this string? text )
	{
		if( text.IsEmpty() )
		{
			return false;
		}

		foreach( char fChar in text )
		{
			if( !char.IsLetterOrDigit( fChar ) )
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	///    Truncate string to a maximum length
	/// </summary>
	/// <param name="text">Input text</param>
	/// <param name="maxLength">Maximum allowed length of a text</param>
	/// <returns>Truncated text</returns>
	[return: NotNullIfNotNull( nameof( text ) )]
	public static string? Truncate( this string? text, int maxLength )
	{
		if( text.IsEmpty() )
		{
			return text;
		}

		if( maxLength < 0 )
		{
			maxLength = 0;
		}

		return text.Length <= maxLength ? text : text[ ..maxLength ];
	}

#endregion

#region Collections

	/// <summary>
	///    Separate collections items to three ways (Only items in left collection, only items in right
	///    collection, items in both collections)
	/// </summary>
	/// <typeparam name="T">Runtime type of item</typeparam>
	/// <param name="left">Left collection of items to separate</param>
	/// <param name="right">Right collection of items to separate</param>
	/// <param name="equalityMethod">Method that check equality of items</param>
	/// <param name="onlyLeft">Method for perform items found only in left collection</param>
	/// <param name="onlyRight">Method for perform items found only in right collection</param>
	/// <param name="both">Method for perform items found in both collections</param>
	public static void Assort<T>(
		this IEnumerable<T>? left, IEnumerable<T>? right, Func<T, T, bool> equalityMethod,
		Action<T>? onlyLeft = null, Action<T>? onlyRight = null, Action<T, T>? both = null )
	{
		left ??= new List<T>();
		right ??= new List<T>();

		List<T> tempRight = right.ToList();
		foreach( T fLeft in left )
		{
			T? fRight = tempRight.FirstOrDefault( t => equalityMethod( fLeft, t ) );

			if( fRight is null )
			{
				onlyLeft?.Invoke( fLeft );
			}
			else
			{
				both?.Invoke( fLeft, fRight );
				tempRight.Remove( fRight );
			}
		}

		if( onlyRight is not null )
		{
			foreach( T fRight in tempRight )
			{
				onlyRight( fRight );
			}
		}
	}

	/// <summary>
	///    Remove range of items
	/// </summary>
	/// <typeparam name="T">Runtime of item type</typeparam>
	/// <param name="source">Collection to remove items</param>
	/// <param name="toRemove">Which items should be removed</param>
	public static void RemoveRange<T>( this IList<T> source, IEnumerable<T>? toRemove )
	{
		if( toRemove != null )
		{
			foreach( T fItem in toRemove )
			{
				source.Remove( fItem );
			}
		}
	}

	/// <summary>
	///    Withdraw all items from list by provided selector and returns them as separate list
	/// </summary>
	/// <typeparam name="TSource">Runtime type of list item</typeparam>
	/// <param name="source">Source list</param>
	/// <param name="selector">Item selector</param>
	/// <returns>Withdrew items as list</returns>
	public static List<TSource> Withdraw<TSource>( this IList<TSource> source, Func<TSource, bool> selector )
	{
		List<TSource> result = [];
		for( int i = 0; i < source.Count; )
		{
			TSource item = source[ i ];
			if( selector( item ) )
			{
				result.Add( item );
				source.RemoveAt( i );
			}
			else
			{
				i++;
			}
		}

		return result;
	}

	/// <summary>
	///    Easy convert enumerable
	/// </summary>
	/// <typeparam name="TSource">Source runtime type</typeparam>
	/// <typeparam name="TResult">Target runtime type</typeparam>
	/// <param name="source">Collection of source objects</param>
	/// <param name="converter">Converter function</param>
	/// <returns>Collection of target objects</returns>
	public static IEnumerable<TResult> Convert<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TResult> converter )
	{
		switch( source )
		{
			case IEnumerable<TResult> results:
				return results;

			default:
				return source.Select( converter );
		}
	}

	/// <summary>
	///    Select all items from collection with min value specified
	/// </summary>
	/// <typeparam name="TSource">Runtime type of items</typeparam>
	/// <typeparam name="TKey">Runtime type of comparable values</typeparam>
	/// <param name="source">Source collection</param>
	/// <param name="selector">Min value selector</param>
	/// <returns>Collection with min values</returns>
	public static IEnumerable<TSource> AllMinBy<TSource, TKey>(
		this IEnumerable<TSource> source, Func<TSource, TKey> selector )
		where TKey : IComparable
	{
		List<TSource> result = new();

		result.Capacity = 1;

		TKey? minKey = default;
		bool firstItem = true;
		foreach( TSource fItem in source )
		{
			TKey fKey = selector.Invoke( fItem );

			int comparison = firstItem ? -1 : fKey.CompareTo( minKey );
			switch( comparison )
			{
				case < 0:
					minKey = fKey;
					result.Clear();
					result.Add( fItem );
					firstItem = false;
					break;

				case 0:
					result.Add( fItem );
					break;
			}
		}

		return result;
	}

	/// <summary>
	///    Select all items from collection with max value specified
	/// </summary>
	/// <typeparam name="TSource">Runtime type of items</typeparam>
	/// <typeparam name="TKey">Runtime type of comparable values</typeparam>
	/// <param name="source">Source collection</param>
	/// <param name="selector">Max value selector</param>
	/// <returns>Collection with max values</returns>
	public static IEnumerable<TSource> AllMaxBy<TSource, TKey>(
		this IEnumerable<TSource> source, Func<TSource, TKey> selector )
		where TKey : IComparable
	{
		List<TSource> result = [];
		TKey? maxKey = default;
		bool firstItem = true;
		foreach( TSource fItem in source )
		{
			TKey fKey = selector.Invoke( fItem );

			int comparison = firstItem ? 1 : fKey.CompareTo( maxKey );
			switch( comparison )
			{
				case > 0:
					maxKey = fKey;
					result.Clear();
					result.Add( fItem );
					firstItem = false;
					break;

				case 0:
					result.Add( fItem );
					break;
			}
		}

		return result;
	}

	/// <summary>
	///    Clears concurrent queue
	/// </summary>
	/// <typeparam name="T">Runtime type of stored objects</typeparam>
	/// <param name="queue">Queue to clear</param>
	public static void Clear<T>( this ConcurrentQueue<T> queue )
	{
		while( !queue.IsEmpty )
		{
			queue.TryDequeue( out T? _ );
		}
	}

#endregion
}
