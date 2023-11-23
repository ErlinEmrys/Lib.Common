using System.Diagnostics.CodeAnalysis;

namespace Erlin.Lib.Common.Collections;

/// <summary>
///    Two way dictionary
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class DictionaryMap<TKey, TValue>
	where TKey : notnull
	where TValue : notnull
{
	private readonly Dictionary<TKey, TValue> _forwardDic = new();
	private readonly Dictionary<TValue, TKey> _backwardDic = new();

	/// <summary>
	///    Key to value translator
	/// </summary>
	public MapIndexer<TKey, TValue> ToValue { get; }

	/// <summary>
	///    Value to key translator
	/// </summary>
	public MapIndexer<TValue, TKey> ToKey { get; }

	/// <summary>
	///    Collection of all keys
	/// </summary>
	public ICollection<TKey> Keys
	{
		get { return _forwardDic.Keys; }
	}

	/// <summary>
	///    Collection of all values
	/// </summary>
	public ICollection<TValue> Values
	{
		get { return _forwardDic.Values; }
	}

	/// <summary>
	///    Ctor
	/// </summary>
	public DictionaryMap()
	{
		ToValue = new MapIndexer<TKey, TValue>( _forwardDic );
		ToKey = new MapIndexer<TValue, TKey>( _backwardDic );
	}

	/// <summary>
	///    Indexer helper
	/// </summary>
	/// <typeparam name="TK"></typeparam>
	/// <typeparam name="TV"></typeparam>
	public class MapIndexer<TK, TV>
		where TK : notnull
		where TV : notnull
	{
		private readonly Dictionary<TK, TV> _dictionary;

		public MapIndexer( Dictionary<TK, TV> dictionary )
		{
			_dictionary = dictionary;
		}

		/// <summary>
		///    Accessor
		/// </summary>
		/// <param name="key"></param>
		public TV this[ TK key ]
		{
			get { return _dictionary[ key ]; }
			set { _dictionary[ key ] = value; }
		}

		/// <summary>
		///    Gets the value associated with the specified key.
		/// </summary>
		public bool TryGetValue( TK key, [MaybeNullWhen( false )]out TV value )
		{
			return _dictionary.TryGetValue( key, out value );
		}
	}

	/// <summary>
	///    Add new key/value pair to the map
	/// </summary>
	/// <param name="left"></param>
	/// <param name="right"></param>
	public void Add( TKey left, TValue right )
	{
		_forwardDic.Add( left, right );
		_backwardDic.Add( right, left );
	}

	/// <summary>
	///    Check if map contains key
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public bool ContainsKey( TKey key )
	{
		return _forwardDic.ContainsKey( key );
	}

	/// <summary>
	///    Check if map contains value
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public bool ContainsValue( TValue value )
	{
		return _backwardDic.ContainsKey( value );
	}

	/// <summary>
	///    Remove key and associated value from map
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	public bool RemoveByKey( TKey key )
	{
		if( ContainsKey( key ) )
		{
			TValue value = ToValue[ key ];
			_forwardDic.Remove( key );
			_backwardDic.Remove( value );

			return true;
		}

		return false;
	}

	/// <summary>
	///    Remove value and associated key from map
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public bool RemoveByValue( TValue value )
	{
		if( ContainsValue( value ) )
		{
			TKey key = ToKey[ value ];
			_forwardDic.Remove( key );
			_backwardDic.Remove( value );

			return true;
		}

		return false;
	}
}
