namespace Erlin.Lib.Common.Collections;

/// <summary>
///    Dictionary for storing multiple values under same key
/// </summary>
/// <typeparam name="TKey">Runtime type of dictionary key</typeparam>
/// <typeparam name="TValue">Runtime type of dictionary value</typeparam>
#pragma warning disable CA1711
public class MultiDictionary<TKey, TValue>
#pragma warning restore CA1711
	where TKey : notnull
{
	private readonly Dictionary<TKey, List<TValue>> _innerDic = new();
	private readonly bool _onlyUniqueValues;

	/// <summary>
	///    All keys collection
	/// </summary>
	public ICollection<TKey> Keys
	{
		get { return _innerDic.Keys; }
	}

	/// <summary>
	///    All List values collection
	/// </summary>
	public ICollection<List<TValue>> Values
	{
		get { return _innerDic.Values; }
	}

	/// <summary>
	///    Key accessor - return all values stored under entered key
	/// </summary>
	/// <param name="key">Key</param>
	/// <returns>List of values</returns>
	public List<TValue> this[ TKey key ]
	{
		get
		{
			return _innerDic.TryGetValue( key, out List<TValue>? item ) ? item : [];
		}
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="onlyUniqueValues">If true, list under key can contains only distinct values</param>
	public MultiDictionary( bool onlyUniqueValues )
	{
		_onlyUniqueValues = onlyUniqueValues;
	}

	/// <summary>
	///    Add value object under entered key
	/// </summary>
	/// <param name="key">Key</param>
	/// <param name="value">Value</param>
	public void Add( TKey key, TValue value )
	{
		if( !_innerDic.TryGetValue( key, out List<TValue>? list ) )
		{
			list = new List<TValue>();
			_innerDic.Add( key, list );
		}

		if( !_onlyUniqueValues || !list.Contains( value ) )
		{
			list.Add( value );
		}
	}

	/// <summary>
	///    Check if this dictionary contains specific key
	/// </summary>
	/// <param name="key">Key</param>
	/// <returns>True - dictionary contains specified key</returns>
	public bool ContainsKey( TKey key )
	{
		return _innerDic.ContainsKey( key );
	}

	/// <summary>
	///    Remove entry by key if exist
	/// </summary>
	/// <param name="key">Key to remove</param>
	/// <returns>True - record existed and was removed</returns>
	public bool Remove( TKey key )
	{
		return _innerDic.Remove( key );
	}
}
