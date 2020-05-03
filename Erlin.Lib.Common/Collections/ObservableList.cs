using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Collections
{
    /// <summary>
    /// List that supports change events
    /// </summary>
    /// <typeparam name="T">Runtime type of items</typeparam>
    public class ObservableList<T> : List<T>
    {
#region Event(s)

        /// <summary>
        /// Fires when count of items changed in this list
        /// </summary>
        public event EventHandler<ListCountChangedEventArgs>? CountChanged;

#endregion

        //------------------------------------------------------------------

#region Subclasses

        /// <summary>
        /// EventArgs on count of items changed
        /// </summary>
        public class ListCountChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Number of elements that was in the list
            /// </summary>
            public int CountOld { get; set; }

            /// <summary>
            /// Number of elements in the list
            /// </summary>
            public int CountNew { get; set; }

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="countOld">Old count</param>
            /// <param name="countNew">New count</param>
            public ListCountChangedEventArgs(int countOld, int countNew)
            {
                CountOld = countOld;
                CountNew = countNew;
            }
        }

#endregion

        //---------------------------------------------------------------

#region Methods

        /// <summary>
        /// Adds an item
        /// </summary>
        public new void Add(T item)
        {
            int oldCount = Count;
            base.Add(item);

            RaiseCountChangedEvent(oldCount);
        }

        //------------------------------------------------------------------
        /// <summary>
        /// Adds a range
        /// </summary>
        public new void AddRange(IEnumerable<T> collection)
        {
            int oldCount = Count;
            base.AddRange(collection);

            RaiseCountChangedEvent(oldCount);
        }

        //------------------------------------------------------------------
        /// <summary>
        /// Clears the list.
        /// </summary>
        public new void Clear()
        {
            int oldCount = Count;
            base.Clear();

            RaiseCountChangedEvent(oldCount);
        }

        //------------------------------------------------------------------
        /// <summary>
        /// Removes the first matched item.
        /// </summary>
        public new void Remove(T item)
        {
            int oldCount = Count;
            base.Remove(item);

            RaiseCountChangedEvent(oldCount);
        }

        /// <summary>
        /// Raise the change of items count event
        /// </summary>
        /// <param name="oldCount">Old count of items</param>
        private void RaiseCountChangedEvent(int oldCount)
        {
            // Copy to a temporary variable to be thread-safe (MSDN).
            EventHandler<ListCountChangedEventArgs>? handler = CountChanged;
            handler?.Invoke(this, new ListCountChangedEventArgs(oldCount, Count));
        }

#endregion
    }
}