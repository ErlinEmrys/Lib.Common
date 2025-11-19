using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Erlin.Lib.Common.Collections;

/// <summary>
///    List of items that provides events when the collection changes
/// </summary>
public class ObservableList< T > : ObservableCollection< T >
{
	/// <summary>
	///    Adds the elements of the specified collection to the end of this List
	/// </summary>
	public void AddRange( IList< T > collection )
	{
		InsertRange( Count, collection );
	}

	/// <summary>
	///    Inserts the elements of a collection into this List at the
	///    specified index.
	/// </summary>
	public void InsertRange( int index, IList< T > collection )
	{
		ArgumentNullException.ThrowIfNull( collection );

		if( ( index < 0 ) || ( index > Count ) )
		{
			throw new ArgumentOutOfRangeException( nameof( index ) );
		}

		if( collection.Count == 0 )
		{
			return;
		}

		CheckReentrancy();

		//expand the following couple of lines when adding more constructors.
		List< T > target = ( List< T > )Items;
		target.InsertRange( index, collection );

		OnPropertyChanged( ObservableListEventArgsCache.CountPropertyChanged );
		OnPropertyChanged( ObservableListEventArgsCache.IndexerPropertyChanged );

		OnCollectionChanged( new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Add, collection, index ) );
	}
}

internal static class ObservableListEventArgsCache
{
	internal static PropertyChangedEventArgs CountPropertyChanged { get; } = new( "Count" );

	internal static PropertyChangedEventArgs IndexerPropertyChanged { get; } = new( "Item[]" );
}
